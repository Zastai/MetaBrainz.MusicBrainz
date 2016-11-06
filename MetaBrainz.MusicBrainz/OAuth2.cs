using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz {

  /// <summary>Class providing convenient access to MusicBrainz' OAuth2 service.</summary>
  [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public class OAuth2 {

    #region Static Fields / Properties

    /// <summary>Sets up defaults for <see cref="OAuth2"/>'s static properties.</summary>
    static OAuth2() {
      // Mono's C# compiler does not like initializers on auto-properties, so set them up here instead.
      OAuth2.DefaultPort      = -1;
      OAuth2.DefaultUrlScheme = "https";
      OAuth2.DefaultWebSite   = "musicbrainz.org";
    }

    /// <summary>The endpoint used when requesting authorization.</summary>
    public const string AuthorizationEndPoint = "/oauth2/authorize";

    /// <summary>The default client ID to use for requests.</summary>
    /// <remarks>To register an application and obtain a client ID, go to <a href="https://musicbrainz.org/account/applications">your MusicBrainz account</a>.</remarks>
    public static string DefaultClientId { get; set; }

    /// <summary>The default port number to use for requests (-1 to not specify any explicit port).</summary>
    public static int DefaultPort { get; set; }

    /// <summary>The default internet access protocol to use for requests.</summary>
    public static string DefaultUrlScheme { get; set; }

    /// <summary>The default web site to use for requests.</summary>
    public static string DefaultWebSite { get; set; }

    /// <summary>The URI to use for out-of-band authorization.</summary>
    public static readonly Uri OutOfBandUri = new Uri("urn:ietf:wg:oauth:2.0:oob");

    /// <summary>The endpoint used when creating or refreshing a token.</summary>
    public const string TokenEndPoint = "/oauth2/token";

    #endregion

    #region Constructors

    /// <summary>Constructs a new <see cref="OAuth2"/> instance.</summary>
    public OAuth2() {
      this.ClientId  = OAuth2.DefaultClientId;
      this.Port      = OAuth2.DefaultPort;
      this.UrlScheme = OAuth2.DefaultUrlScheme;
      this.WebSite   = OAuth2.DefaultWebSite;
    }

    #endregion

    #region Public Methods

    /// <summary>Creates the URI to use to request an authorization code.</summary>
    /// <param name="redirectUri">The URI that should receive the authorization code; use <see cref="OutOfBandUri"/> for out-of-band requests.</param>
    /// <param name="scope">The authorization scopes that should be included in the authorization code.</param>
    /// <param name="state">An optional string that will be included in the response sent to <paramref name="redirectUri"/>.</param>
    /// <param name="offlineAccess">Requests offline use (a refresh token will be provided alongside the access token).</param>
    /// <param name="forcePrompt">If true, the user will be required to confirm authorization even if the requested scopes have already been granted.</param>
    /// <returns>The generated URI.</returns>
    public Uri CreateAuthorizationRequest(Uri redirectUri, AuthorizationScope scope, string state = null, bool offlineAccess = false, bool forcePrompt = false) {
      if (redirectUri == null)
        throw new ArgumentNullException(nameof(redirectUri));
      if (scope == AuthorizationScope.None)
        throw new ArgumentException("At least one authorization scope must be selected.", nameof(scope));
      if (string.IsNullOrWhiteSpace(this.WebSite))  throw new InvalidOperationException("No website has been set.");
      if (string.IsNullOrWhiteSpace(this.ClientId)) throw new InvalidOperationException("No client ID has been set.");
      var uri = new UriBuilder(this.UrlScheme, this.WebSite, this.Port, OAuth2.AuthorizationEndPoint);
      var queryparts = new List<string>(7) {
        "response_type=code",
        $"client_id={Uri.EscapeDataString(this.ClientId)}",
        $"redirect_uri={Uri.EscapeDataString(redirectUri.ToString())}",
        $"scope={string.Join("+", OAuth2.ScopeStrings(scope))}",
      };
      if (state != null)
        queryparts.Add($"state={Uri.EscapeDataString(state)}");
      if (offlineAccess)
        queryparts.Add("access_type=offline");
      if (forcePrompt)
        queryparts.Add("approval_prompt=force");
      uri.Query = string.Join("&", queryparts);
      return uri.Uri;
    }

    /// <summary>Exchanges an authorization code for a bearer token.</summary>
    /// <param name="code">The authorization code to be used. If the request succeeds, this code will be invalidated.</param>
    /// <param name="clientSecret">The client secret associated with <see cref="ClientId"/>.</param>
    /// <param name="redirectUri">The URI to redirect to (or <see cref="OutOfBandUri"/> for out-of-band requests); must match the request URI used to obtain <paramref name="code"/>.</param>
    /// <returns>The obtained bearer token.</returns>
    public AuthorizationToken GetBearerToken(string code, string clientSecret, Uri redirectUri) => this.TokenRequest("bearer", code, clientSecret, redirectUri, false);

    /// <summary>Refreshes a bearer token.</summary>
    /// <param name="refreshToken">The refresh token to use.</param>
    /// <param name="clientSecret">The client secret associated with <see cref="ClientId"/>.</param>
    /// <returns>The obtained bearer token.</returns>
    public AuthorizationToken RefreshBearerToken(string refreshToken, string clientSecret) => this.TokenRequest("bearer", refreshToken, clientSecret, null, true);

    #endregion

    #region Instance Fields / Properties

    /// <summary>The client ID to use for requests.</summary>
    /// <remarks>To register an application and obtain a client ID, go to <a href="https://musicbrainz.org/account/applications">your MusicBrainz account</a>.</remarks>
    public string ClientId { get; set; }

    /// <summary>The port number to use for requests (-1 to not specify any explicit port).</summary>
    public int Port { get; set; }

    /// <summary>The internet access protocol to use for requests.</summary>
    public string UrlScheme { get; set; }

    /// <summary>The web site to use for requests.</summary>
    public string WebSite { get; set; }

    #endregion

    #region Internals

    private static IEnumerable<string> ScopeStrings(AuthorizationScope scope) {
      if ((scope & AuthorizationScope.Collection)    != 0) yield return "collection";
      if ((scope & AuthorizationScope.EMail)         != 0) yield return "email";
      if ((scope & AuthorizationScope.Profile)       != 0) yield return "profile";
      if ((scope & AuthorizationScope.Rating)        != 0) yield return "rating";
      if ((scope & AuthorizationScope.SubmitBarcode) != 0) yield return "submit_barcode";
      if ((scope & AuthorizationScope.SubmitIsrc)    != 0) yield return "submit_isrc";
      if ((scope & AuthorizationScope.Tag)           != 0) yield return "tag";
    }

    private AuthorizationToken TokenRequest(string type, string codeOrToken, string clientSecret, Uri redirectUri, bool refresh) {
      if (type         == null) throw new ArgumentNullException(nameof(type));
      if (codeOrToken  == null) throw new ArgumentNullException(nameof(codeOrToken));
      if (clientSecret == null) throw new ArgumentNullException(nameof(clientSecret));
      if (redirectUri  == null && !refresh) throw new ArgumentNullException(nameof(redirectUri));
      if (string.IsNullOrWhiteSpace(this.WebSite))  throw new InvalidOperationException("No website has been set.");
      if (string.IsNullOrWhiteSpace(this.ClientId)) throw new InvalidOperationException("No client ID has been set.");
      var uri = new UriBuilder(this.UrlScheme, this.WebSite, this.Port, OAuth2.TokenEndPoint);
      Debug.Print($"[{DateTime.UtcNow}] OAUTH2 REQUEST: {uri.Uri}");
      var req = WebRequest.Create(uri.Uri) as HttpWebRequest;
      if (req == null)
        throw new InvalidOperationException("Only HTTP-compatible URL schemes are supported.");
      req.Method      = "POST";
      req.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
      {
        var an = Assembly.GetExecutingAssembly().GetName();
        req.UserAgent = $"{an.Name}/v{an.Version}";
      }
      {
        var queryparts = new List<string>(6) {
          $"client_id={Uri.EscapeDataString(this.ClientId)}",
          $"client_secret={Uri.EscapeDataString(clientSecret)}",
          $"token_type={Uri.EscapeDataString(type)}",
        };
        if (refresh) {
          queryparts.Add("grant_type=refresh_token");
          queryparts.Add($"refresh_token={Uri.EscapeDataString(codeOrToken)}");
        }
        else {
          queryparts.Add("grant_type=authorization_code");
          queryparts.Add($"code={Uri.EscapeDataString(codeOrToken)}");
          queryparts.Add($"redirect_uri={Uri.EscapeDataString(redirectUri.ToString())}");
        }
        using (var rs = req.GetRequestStream()) {
          var query = string.Join("&", queryparts);
          var bytes = Encoding.UTF8.GetBytes(query);
          rs.Write(bytes, 0, bytes.Length);
        }
      }
      using (var response = (HttpWebResponse) req.GetResponse()) {
        var stream = response.GetResponseStream();
        if (stream != null) {
          var encoding = Encoding.UTF8;
          if (!string.IsNullOrWhiteSpace(response.CharacterSet))
            encoding = Encoding.GetEncoding(response.CharacterSet);
          using (var sr = new StreamReader(stream, encoding)) {
            var at = JsonConvert.DeserializeObject<AuthorizationToken>(sr.ReadToEnd());
            if (at.TokenType != type)
              throw new InvalidOperationException($"Token request returned a token of the wrong type (\"{at.TokenType}\" != \"{type}\").");
            return at;
          }
        }
      }
      return null;
    }

    #endregion

  }

}
