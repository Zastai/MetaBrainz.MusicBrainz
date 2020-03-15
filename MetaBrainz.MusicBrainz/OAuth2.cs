using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces;
using MetaBrainz.MusicBrainz.Objects;

namespace MetaBrainz.MusicBrainz {

  /// <summary>Class providing convenient access to MusicBrainz' OAuth2 service.</summary>
  [PublicAPI]
  public class OAuth2 {

    #region Static Fields / Properties

    /// <summary>The endpoint used when requesting authorization.</summary>
    public const string AuthorizationEndPoint = "/oauth2/authorize";

    /// <summary>The default client ID to use for requests.</summary>
    /// <remarks>To register an application and obtain a client ID, go to <a href="https://musicbrainz.org/account/applications">your MusicBrainz account</a>.</remarks>
    public static string DefaultClientId { get; set; } = "";

    /// <summary>The default port number to use for requests (-1 to not specify any explicit port).</summary>
    public static int DefaultPort { get; set; } = -1;

    /// <summary>The default internet access protocol to use for requests.</summary>
    public static string DefaultUrlScheme { get; set; } = "https";

    /// <summary>The default web site to use for requests.</summary>
    public static string DefaultWebSite { get; set; } = "musicbrainz.org";

    /// <summary>The URI to use for out-of-band authorization.</summary>
    public static readonly Uri OutOfBandUri = new Uri("urn:ietf:wg:oauth:2.0:oob");

    /// <summary>The endpoint used when creating or refreshing a token.</summary>
    public const string TokenEndPoint = "/oauth2/token";

    #endregion

    #region Public Methods

    /// <summary>Creates the URI to use to request an authorization code.</summary>
    /// <param name="redirectUri">The URI that should receive the authorization code; use <see cref="OutOfBandUri"/> for out-of-band requests.</param>
    /// <param name="scope">The authorization scopes that should be included in the authorization code.</param>
    /// <param name="state">An optional string that will be included in the response sent to <paramref name="redirectUri"/>.</param>
    /// <param name="offlineAccess">Requests offline use (a refresh token will be provided alongside the access token).</param>
    /// <param name="forcePrompt">If true, the user will be required to confirm authorization even if the requested scopes have already been granted.</param>
    /// <returns>The generated URI.</returns>
    public Uri CreateAuthorizationRequest(Uri redirectUri, AuthorizationScope scope, string? state = null, bool offlineAccess = false, bool forcePrompt = false) {
      if (scope == AuthorizationScope.None)
        throw new ArgumentException("At least one authorization scope must be selected.", nameof(scope));
      var uri = this.BuildEndPointUri(OAuth2.AuthorizationEndPoint);
      var query = new StringBuilder();
      query.Append("response_type=code");
      query.Append("&client_id=").Append(Uri.EscapeDataString(this.ClientId));
      query.Append("&redirect_uri=").Append(Uri.EscapeDataString(redirectUri.ToString()));
      query.Append("&scope=").Append(string.Join("+", OAuth2.ScopeStrings(scope)));
      if (state != null)
        query.Append("&state=").Append(Uri.EscapeDataString(state));
      if (offlineAccess)
        query.Append("&access_type=offline");
      if (forcePrompt)
        query.Append("&approval_prompt=force");
      uri.Query = query.ToString();
      return uri.Uri;
    }

    /// <summary>Exchanges an authorization code for a bearer token.</summary>
    /// <param name="code">The authorization code to be used. If the request succeeds, this code will be invalidated.</param>
    /// <param name="clientSecret">The client secret associated with <see cref="ClientId"/>.</param>
    /// <param name="redirectUri">The URI to redirect to (or <see cref="OutOfBandUri"/> for out-of-band requests); must match the request URI used to obtain <paramref name="code"/>.</param>
    /// <returns>The obtained bearer token.</returns>
    public IAuthorizationToken GetBearerToken(string code, string clientSecret, Uri redirectUri) => this.RequestToken("bearer", code, clientSecret, redirectUri, false);

    /// <summary>Exchanges an authorization code for a bearer token.</summary>
    /// <param name="code">The authorization code to be used. If the request succeeds, this code will be invalidated.</param>
    /// <param name="clientSecret">The client secret associated with <see cref="ClientId"/>.</param>
    /// <param name="redirectUri">The URI to redirect to (or <see cref="OutOfBandUri"/> for out-of-band requests); must match the request URI used to obtain <paramref name="code"/>.</param>
    /// <returns>The obtained bearer token.</returns>
    public async Task<IAuthorizationToken> GetBearerTokenAsync(string code, string clientSecret, Uri redirectUri) => await this.RequestTokenAsync("bearer", code, clientSecret, redirectUri, false);

    /// <summary>Refreshes a bearer token.</summary>
    /// <param name="refreshToken">The refresh token to use.</param>
    /// <param name="clientSecret">The client secret associated with <see cref="ClientId"/>.</param>
    /// <returns>The obtained bearer token.</returns>
    public IAuthorizationToken RefreshBearerToken(string refreshToken, string clientSecret) => this.RequestToken("bearer", refreshToken, clientSecret, null, true);

    /// <summary>Refreshes a bearer token.</summary>
    /// <param name="refreshToken">The refresh token to use.</param>
    /// <param name="clientSecret">The client secret associated with <see cref="ClientId"/>.</param>
    /// <returns>The obtained bearer token.</returns>
    public async Task<IAuthorizationToken> RefreshBearerTokenAsync(string refreshToken, string clientSecret) => await this.RequestTokenAsync("bearer", refreshToken, clientSecret, null, true);

    #endregion

    #region Instance Fields / Properties

    /// <summary>The client ID to use for requests.</summary>
    /// <remarks>To register an application and obtain a client ID, go to <a href="https://musicbrainz.org/account/applications">your MusicBrainz account</a>.</remarks>
    public string ClientId { get; set; } = OAuth2.DefaultClientId;

    /// <summary>The port number to use for requests (-1 to not specify any explicit port).</summary>
    public int Port { get; set; } = OAuth2.DefaultPort;

    /// <summary>The internet access protocol to use for requests.</summary>
    public string UrlScheme { get; set; } = OAuth2.DefaultUrlScheme;

    /// <summary>The web site to use for requests.</summary>
    public string WebSite { get; set; } = OAuth2.DefaultWebSite;

    #endregion

    #region Internals

    private UriBuilder BuildEndPointUri(string endpoint) {
      if (string.IsNullOrWhiteSpace(this.UrlScheme))
        throw new InvalidOperationException("No URL scheme has been set.");
      if (string.IsNullOrWhiteSpace(this.WebSite))
        throw new InvalidOperationException("No website has been set.");
      if (string.IsNullOrWhiteSpace(this.ClientId))
        throw new InvalidOperationException("No client ID has been set.");
      return new UriBuilder(this.UrlScheme, this.WebSite, this.Port, endpoint);
    }

    private HttpWebRequest CreateTokenRequest() {
      var uri = this.BuildEndPointUri(OAuth2.TokenEndPoint);
      Debug.Print($"[{DateTime.UtcNow}] OAUTH2 REQUEST: {uri.Uri}");
      var req = WebRequest.Create(uri.Uri) as HttpWebRequest;
      if (req == null)
        throw new InvalidOperationException("Only HTTP-compatible URL schemes are supported.");
      req.Method      = "POST";
      req.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
      {
        var an = Assembly.GetExecutingAssembly().GetName();
        req.UserAgent = $"{an.Name}/{an.Version}";
      }
      return req;
    }

    private string CreateTokenRequestBody(string type, string codeOrToken, string clientSecret, Uri? redirectUri, bool refresh) {
      var body = new StringBuilder();
      body.Append("client_id=")     .Append(Uri.EscapeDataString(this.ClientId));
      body.Append("&client_secret=").Append(Uri.EscapeDataString(clientSecret));
      body.Append("&token_type=")   .Append(Uri.EscapeDataString(type));
      if (refresh) {
        body.Append("&grant_type=refresh_token");
        body.Append("&refresh_token=").Append(Uri.EscapeDataString(codeOrToken));
      }
      else {
        body.Append("&grant_type=authorization_code");
        body.Append("&code=")        .Append(Uri.EscapeDataString(codeOrToken));
        body.Append("&redirect_uri=").Append(Uri.EscapeDataString(redirectUri!.ToString()));
      }
      return body.ToString();
    }

    private AuthorizationToken ProcessResponse(HttpWebResponse response) {
      Debug.Print($"[{DateTime.UtcNow}] => RESPONSE ({response.ContentType}): {response.ContentLength} bytes");
      var stream = response.GetResponseStream();
      if (stream == null)
        throw new WebException("No data received.", WebExceptionStatus.ReceiveFailure);
      var encoding = Encoding.UTF8;
      if (response.CharacterSet != null && response.CharacterSet.Trim().Length > 0)
        encoding = Encoding.GetEncoding(response.CharacterSet);
      using var sr = new StreamReader(stream, encoding);
      var json = sr.ReadToEnd();
      Debug.Print($"[{DateTime.UtcNow}] => JSON: {json}");
      return JsonUtils.Deserialize<AuthorizationToken>(json);
    }

    private async Task<AuthorizationToken> ProcessResponseAsync(HttpWebResponse response) {
      Debug.Print($"[{DateTime.UtcNow}] => RESPONSE ({response.ContentType}): {response.ContentLength} bytes");
#if NETSTD_GE_2_1 || NETCORE_GE_3_0
      var stream = response.GetResponseStream();
      await using var _ = stream.ConfigureAwait(false);
#else
      using var stream = response.GetResponseStream();
#endif
      if (stream == null)
        throw new WebException("No data received.", WebExceptionStatus.ReceiveFailure);
      var characterSet = response.CharacterSet;
      if (characterSet == null || characterSet.Trim().Length == 0)
        characterSet = "utf-8";
#if NETSTD_GE_2_1 || NETCORE_GE_3_0
      // FIXME: Because this uses the stream directly, we can't show the JSON in debug builds.
      if (characterSet == "utf-8") // Directly use the stream
        return await JsonSerializer.DeserializeAsync<AuthorizationToken>(stream);
#endif
      var enc = Encoding.GetEncoding(characterSet);
      using var sr = new StreamReader(stream, enc);
      var json = await sr.ReadToEndAsync().ConfigureAwait(false);
      Debug.Print($"[{DateTime.UtcNow}] => JSON: {json}");
      return JsonUtils.Deserialize<AuthorizationToken>(json);
    }

    private IAuthorizationToken RequestToken(string type, string codeOrToken, string clientSecret, Uri? redirectUri, bool refresh) {
      var req = this.CreateTokenRequest();
      var body = this.CreateTokenRequestBody(type, codeOrToken, clientSecret, redirectUri, refresh);
      using var response = this.SendRequest(req, body);
      return this.ValidateToken(this.ProcessResponse(response), type);
    }

    private async Task<IAuthorizationToken> RequestTokenAsync(string type, string codeOrToken, string clientSecret, Uri? redirectUri, bool refresh) {
      var req = this.CreateTokenRequest();
      var body = this.CreateTokenRequestBody(type, codeOrToken, clientSecret, redirectUri, refresh);
      using var response = this.SendRequest(req, body);
      return this.ValidateToken(await this.ProcessResponseAsync(response), type);
    }

    private static IEnumerable<string> ScopeStrings(AuthorizationScope scope) {
      if ((scope & AuthorizationScope.Collection)    != 0) yield return "collection";
      if ((scope & AuthorizationScope.EMail)         != 0) yield return "email";
      if ((scope & AuthorizationScope.Profile)       != 0) yield return "profile";
      if ((scope & AuthorizationScope.Rating)        != 0) yield return "rating";
      if ((scope & AuthorizationScope.SubmitBarcode) != 0) yield return "submit_barcode";
      if ((scope & AuthorizationScope.SubmitIsrc)    != 0) yield return "submit_isrc";
      if ((scope & AuthorizationScope.Tag)           != 0) yield return "tag";
    }

    private HttpWebResponse SendRequest(HttpWebRequest req, string body) {
      using var rs = req.GetRequestStream();
      var bytes = Encoding.UTF8.GetBytes(body);
      rs.Write(bytes, 0, bytes.Length);
      return (HttpWebResponse) req.GetResponse();
    }

    private async Task<HttpWebResponse> SendRequestAsync(HttpWebRequest req, string body) {
      using var rs = req.GetRequestStream();
      var bytes = Encoding.UTF8.GetBytes(body);
      await rs.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
      return (HttpWebResponse) await req.GetResponseAsync().ConfigureAwait(false);
    }

    private IAuthorizationToken ValidateToken(AuthorizationToken token, string type) {
      if (token.TokenType != type)
        throw new InvalidOperationException($"Token request returned a token of the wrong type ('{token.TokenType}' != '{type}').");
      return token;
    }

    #endregion

  }

}
