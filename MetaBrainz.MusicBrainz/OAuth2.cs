using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

using MetaBrainz.Common;
using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces;
using MetaBrainz.MusicBrainz.Json.OAuth2;
using MetaBrainz.MusicBrainz.Objects.OAuth2;

namespace MetaBrainz.MusicBrainz;

/// <summary>Class providing convenient access to the MusicBrainz OAuth2 service.</summary>
[PublicAPI]
public sealed class OAuth2 : IDisposable {

  #region Static Fields / Properties

  /// <summary>The endpoint used when requesting authorization.</summary>
  public const string AuthorizationEndPoint = "/oauth2/authorize";

  /// <summary>The default client ID to use for requests.</summary>
  /// <remarks>
  /// To register an application and obtain a client ID, go to
  /// <a href="https://musicbrainz.org/account/applications">your MusicBrainz account</a>.
  /// </remarks>
  public static string DefaultClientId { get; set; } = "";

  /// <summary>The default port number to use for requests (-1 to not specify any explicit port).</summary>
  public static int DefaultPort {
    get;
    set {
      if (value is < -1 or > 65535) {
        throw new ArgumentOutOfRangeException(nameof(OAuth2.DefaultPort), value,
                                              "The default port number must not be less than -1 or greater than 65535.");
      }
      field = value;
    }
  } = -1;

  /// <summary>The default server to use for requests.</summary>
  public static string DefaultServer {
    get;
    set {
      if (string.IsNullOrWhiteSpace(value)) {
        throw new ArgumentException("The default server name must not be blank.", nameof(OAuth2.DefaultServer));
      }
      field = value.Trim();
    }
  } = "musicbrainz.org";

  /// <summary>The default URL scheme (internet access protocol) to use for requests.</summary>
  /// <remarks>For the official MusicBrainz site, this <em>must</em> be <c>https</c>.</remarks>
  public static string DefaultUrlScheme {
    get;
    set {
      if (string.IsNullOrWhiteSpace(value)) {
        throw new ArgumentException("The default URL scheme must not be blank.", nameof(OAuth2.DefaultUrlScheme));
      }
      field = value.Trim();
    }
  } = "https";

  /// <summary>The URI to use for out-of-band authorization.</summary>
  public static readonly Uri OutOfBandUri = new("urn:ietf:wg:oauth:2.0:oob");

  /// <summary>The endpoint used when revoking a token.</summary>
  public const string RevokeEndPoint = "/oauth2/revoke";

  /// <summary>The endpoint used when creating or refreshing a token.</summary>
  public const string TokenEndPoint = "/oauth2/token";

  /// <summary>The content type for a token request body.</summary>
  public const string TokenRequestBodyType = "application/x-www-form-urlencoded";

  /// <summary>The trace source (named 'MetaBrainz.MusicBrainz.OAuth2') used by this class.</summary>
  public static readonly TraceSource TraceSource = new("MetaBrainz.MusicBrainz.OAuth2", SourceLevels.Off);

  /// <summary>The endpoint used when requesting user info.</summary>
  public const string UserInfoEndPoint = "/oauth2/userinfo";

  #endregion

  #region Constructors

  /// <summary>
  /// Initializes a new OAuth2 client instance.<br/>
  /// An HTTP client will be created when needed and can be discarded again via the <see cref="Close()"/> method.
  /// </summary>
  public OAuth2() {
    this._clientOwned = true;
  }

  /// <summary>Initializes a new OAuth2 client instance using a specific HTTP client.</summary>
  /// <param name="client">The HTTP client to use.</param>
  /// <param name="takeOwnership">
  /// Indicates whether this OAuth2 client should take ownership of <paramref name="client"/>.<br/>
  /// If this is <see langword="false"/>, it remains owned by the caller; this means <see cref="Close()"/> will throw an exception
  /// and <see cref="Dispose()"/> will release the reference to <paramref name="client"/> without disposing it.<br/>
  /// If this is <see langword="true"/>, then this object takes ownership and treat it just like an HTTP client it created itself;
  /// this means <see cref="Close()"/> will dispose of it (with further requests creating a new HTTP client) and
  /// <see cref="Dispose()"/> will dispose the HTTP client too.
  /// </param>
  public OAuth2(HttpClient client, bool takeOwnership = false) {
    this._client = client;
    this._clientOwned = takeOwnership;
  }

  #endregion

  #region Instance Fields / Properties

  /// <summary>The client ID to use for requests.</summary>
  /// <remarks>
  /// To register an application and obtain a client ID, go to
  /// <a href="https://musicbrainz.org/account/applications">your MusicBrainz account</a>.
  /// </remarks>
  public string ClientId { get; set; } = OAuth2.DefaultClientId;

  /// <summary>The port number to use for requests (-1 to not specify any explicit port).</summary>
  public int Port {
    get;
    set {
      if (value is < -1 or > 65535) {
        throw new ArgumentOutOfRangeException(nameof(OAuth2.Port), value,
                                              "The port number must not be less than -1 or greater than 65535.");
      }
      field = value;
    }
  } = OAuth2.DefaultPort;

  /// <summary>The website to use for requests.</summary>
  public string Server {
    get;
    set {
      if (string.IsNullOrWhiteSpace(value)) {
        throw new ArgumentException("The server name must not be blank.", nameof(OAuth2.Server));
      }
      field = value.Trim();
    }
  } = OAuth2.DefaultServer;

  /// <summary>The URL scheme (internet access protocol) to use for requests.</summary>
  /// <remarks>For the official MusicBrainz site, this <em>must</em> be <c>https</c>.</remarks>
  public string UrlScheme {
    get;
    set {
      if (string.IsNullOrWhiteSpace(value)) {
        throw new ArgumentException("The URL scheme must not be blank.", nameof(OAuth2.UrlScheme));
      }
      field = value.Trim();
    }
  } = OAuth2.DefaultUrlScheme;

  #endregion

  #region Public Methods

  /// <summary>Creates the URI to use to request an authorization code.</summary>
  /// <param name="redirectUri">
  /// The URI that should receive the authorization code; use <see cref="OutOfBandUri"/> for out-of-band requests.
  /// </param>
  /// <param name="scope">The authorization scopes that should be included in the authorization code.</param>
  /// <param name="state">
  /// Any string the application wants passed back after authorization; it will be included in the request sent to
  /// <paramref name="redirectUri"/>. For example, this can be a CSRF token from your application. This parameter is optional, but
  /// strongly recommended.
  /// </param>
  /// <param name="challenge">
  /// MusicBrainz supports the use of "Proof Key for Code Exchange" (PKCE) by clients. This is strongly recommended to avoid
  /// authorization code interception attacks.<br/>
  /// See <seealso href="https://tools.ietf.org/html/rfc7636#section-4.1">RFC 7636</seealso> for the process of generating a
  /// <c>code_verifier</c> and then a <c>code_challenge</c> (passed here) based on that.
  /// </param>
  /// <param name="challengeMethod">Either "S256" (recommended) or "plain" (the default if not specified).</param>
  /// <param name="offlineAccess">Requests offline use (a refresh token will be provided alongside the access token).</param>
  /// <param name="forcePrompt">
  /// If <see langword="true"/>, the user will be required to confirm authorization even if the requested scopes have already been
  /// granted.
  /// </param>
  /// <returns>The generated URI.</returns>
  public Uri CreateAuthorizationRequest(Uri redirectUri, AuthorizationScope scope, string? state = null, string? challenge = null,
                                        string? challengeMethod = null, bool offlineAccess = false, bool forcePrompt = false) {
    if (scope == AuthorizationScope.None) {
      throw new ArgumentException("At least one authorization scope must be selected.", nameof(scope));
    }
    var query = new StringBuilder();
    query.Append("?response_type=code");
    query.Append("&client_id=").Append(Uri.EscapeDataString(this.ClientId));
    query.Append("&redirect_uri=").Append(Uri.EscapeDataString(redirectUri.ToString()));
    query.Append("&scope=").Append(string.Join('+', OAuth2.ScopeStrings(scope)));
    if (state is not null) {
      query.Append("&state=").Append(Uri.EscapeDataString(state));
    }
    if (offlineAccess) {
      query.Append("&access_type=offline");
    }
    if (forcePrompt) {
      query.Append("&approval_prompt=force");
    }
    if (challenge is not null) {
      query.Append("&code_challenge=").Append(Uri.EscapeDataString(challenge));
      if (challengeMethod is not null) {
        query.Append("&code_challenge_method=").Append(Uri.EscapeDataString(challengeMethod));
      }
    }
    return new UriBuilder(this.UrlScheme, this.Server, this.Port, OAuth2.AuthorizationEndPoint, query.ToString()).Uri;
  }

  /// <summary>Exchanges an authorization code for a bearer token.</summary>
  /// <param name="code">The authorization code to be used. If the request succeeds, this code will be invalidated.</param>
  /// <param name="clientSecret">The client secret associated with <see cref="ClientId"/>.</param>
  /// <param name="redirectUri">
  /// The URI to redirect to (or <see cref="OutOfBandUri"/> for out-of-band requests); must match the request URI used to obtain
  /// <paramref name="code"/>.
  /// </param>
  /// <param name="verifier">
  /// If you're using PKCE, pass the <c>code_verifier</c> here. The request will be rejected if it doesn't agree with the challenge
  /// and challenge method used for the authorization request as generated via <see cref="CreateAuthorizationRequest"/>. The process
  /// is described in detail by <seealso href="https://tools.ietf.org/html/rfc7636#section-4.5">RFC 7636</seealso>.
  /// </param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The obtained bearer token.</returns>
  public Task<IAuthorizationToken> GetBearerTokenAsync(string code, string clientSecret, Uri redirectUri, string? verifier = null,
                                                       CancellationToken cancellationToken = default)
    => this.RequestTokenAsync("bearer", code, clientSecret, redirectUri, verifier, cancellationToken);

  /// <summary>Gets information about the user associated with an access token.</summary>
  /// <param name="token">The access token.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>Information about the user associated with the access token.</returns>
  /// <remarks>
  /// If the <see cref="AuthorizationScope.Profile"/> permission has not been granted, this request will fail. In addition, it will
  /// only include the user's email address if the <see cref="AuthorizationScope.Email"/> permission has been granted.
  /// </remarks>
  public Task<IUserInfo> GetUserInfoAsync(string token, CancellationToken cancellationToken = default)
    => this.GetAsync<IUserInfo, UserInfo>(OAuth2.UserInfoEndPoint, token, cancellationToken);

  /// <summary>Refreshes a bearer token.</summary>
  /// <param name="refreshToken">The refresh token to use.</param>
  /// <param name="clientSecret">The client secret associated with <see cref="ClientId"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The obtained bearer token.</returns>
  public Task<IAuthorizationToken> RefreshBearerTokenAsync(string refreshToken, string clientSecret,
                                                           CancellationToken cancellationToken = default)
    => this.RefreshTokenAsync("bearer", refreshToken, clientSecret, cancellationToken);

  /// <summary>Revokes a token.</summary>
  /// <param name="token">The token to revoke. This can be either an access token or a refresh token.</param>
  /// <param name="clientSecret">The client secret associated with <see cref="ClientId"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  public Task RevokeTokenAsync(string token, string clientSecret, CancellationToken cancellationToken = default)
    => this.PostRevocationRequestAsync(token, clientSecret, cancellationToken);

  #endregion

  #region HttpClient / IDisposable

  private static readonly MediaTypeWithQualityHeaderValue AcceptHeader = new("application/json");

  private static readonly ProductInfoHeaderValue LibraryComment = new("(https://github.com/Zastai/MetaBrainz.MusicBrainz)");

  private static readonly ProductInfoHeaderValue LibraryProductInfo = HttpUtils.CreateUserAgentHeader<OAuth2>();

  private HttpClient? _client;

  private Action<HttpClient>? _clientConfiguration;

  private Func<HttpClient>? _clientCreation;

  private readonly bool _clientOwned;

  private bool _disposed;

  private HttpClient Client {
    get {
      ObjectDisposedException.ThrowIf(this._disposed, typeof(OAuth2));
      if (this._client is null) {
        var client = this._clientCreation?.Invoke() ?? new HttpClient();
        this._clientConfiguration?.Invoke(client);
        this._client = client;
      }
      return this._client;
    }
  }

  /// <summary>Closes the underlying web service client in use by this OAuth2 client, if one has been created.</summary>
  /// <remarks>The next web service request will create a new client.</remarks>
  /// <exception cref="InvalidOperationException">When this instance is using an explicitly provided client instance.</exception>
  public void Close() {
    if (!this._clientOwned) {
      throw new InvalidOperationException("An explicitly provided client instance is in use.");
    }
    Interlocked.Exchange(ref this._client, null)?.Dispose();
  }

  /// <summary>Sets up code to run to configure a newly-created HTTP client.</summary>
  /// <param name="code">The configuration code for an HTTP client, or <see langword="null"/> to clear such code.</param>
  public void ConfigureClient(Action<HttpClient>? code) {
    this._clientConfiguration = code;
  }

  /// <summary>Sets up code to run to create an HTTP client.</summary>
  /// <param name="code">The creation code for an HTTP client, or <see langword="null"/> to clear such code.</param>
  /// <remarks>
  /// Any code set via <see cref="ConfigureClient(System.Action{System.Net.Http.HttpClient}?)"/> will be applied to the client
  /// returned by <paramref name="code"/>.
  /// </remarks>
  public void ConfigureClientCreation(Func<HttpClient>? code) {
    this._clientCreation = code;
  }

  /// <summary>Discards all resources held by this OAuth client, if any.</summary>
  /// <remarks>Further attempts at web service requests will cause <see cref="ObjectDisposedException"/> to be thrown.</remarks>
  public void Dispose() {
    this.Dispose(true);
    GC.SuppressFinalize(this);
  }

  private void Dispose(bool disposing) {
    if (!disposing) {
      // no unmanaged resources
      return;
    }
    try {
      if (this._clientOwned) {
        this.Close();
      }
      this._client = null;
    }
    finally {
      this._disposed = true;
    }
  }

  /// <summary>Finalizes this instance, releasing any and all resources.</summary>
  ~OAuth2() {
    this.Dispose(false);
  }

  #endregion

  #region Internals

  private static readonly JsonSerializerOptions JsonReaderOptions = JsonUtils.CreateReaderOptions(Converters.Readers);

  private async Task<TInterface> GetAsync<TInterface, TObject>(string endPoint, string? token,
                                                               CancellationToken cancellationToken) where TObject : TInterface {
    var response = await this.PerformRequestAsync(HttpMethod.Get, endPoint, null, token, cancellationToken).ConfigureAwait(false);
    var jsonTask = JsonUtils.GetJsonContentAsync<TObject>(response, OAuth2.JsonReaderOptions, cancellationToken);
    return await jsonTask.ConfigureAwait(false);
  }

  private async Task<HttpResponseMessage> PerformRequestAsync(HttpMethod method, string endPoint, HttpContent? body, string? token,
                                                              CancellationToken cancellationToken) {
    using var request = new HttpRequestMessage(method, new UriBuilder(this.UrlScheme, this.Server, this.Port, endPoint).Uri);
    var ts = OAuth2.TraceSource;
    ts.TraceEvent(TraceEventType.Verbose, 1, "WEB SERVICE REQUEST: {0} {1}", method.Method, request.RequestUri);
    var client = this.Client;
    {
      var headers = request.Headers;
      headers.Accept.Add(OAuth2.AcceptHeader);
      if (token is not null) {
        headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
      }
      // Use whatever user agent the client has set, plus our own.
      {
        var userAgent = headers.UserAgent;
        foreach (var ua in client.DefaultRequestHeaders.UserAgent) {
          userAgent.Add(ua);
        }
        userAgent.Add(OAuth2.LibraryProductInfo);
        userAgent.Add(OAuth2.LibraryComment);
      }
    }
    if (ts.Switch.ShouldTrace(TraceEventType.Verbose)) {
      ts.TraceEvent(TraceEventType.Verbose, 2, "HEADERS: {0}", TextUtils.FormatMultiLine(request.Headers.ToString()));
      if (body is not null) {
        var headers = body.Headers;
        ts.TraceEvent(TraceEventType.Verbose, 3, "BODY ({0}, {1} bytes): {2}", headers.ContentType, headers.ContentLength ?? 0,
                      TextUtils.FormatMultiLine(await body.ReadAsStringAsync(cancellationToken)));
      }
      else {
        ts.TraceEvent(TraceEventType.Verbose, 3, "NO BODY");
      }
    }
    request.Content = body;
    var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
    if (ts.Switch.ShouldTrace(TraceEventType.Verbose)) {
      ts.TraceEvent(TraceEventType.Verbose, 4, "WEB SERVICE RESPONSE: {0:D}/{0} '{1}' (v{2})", response.StatusCode,
                    response.ReasonPhrase, response.Version);
      ts.TraceEvent(TraceEventType.Verbose, 5, "HEADERS: {0}", TextUtils.FormatMultiLine(response.Headers.ToString()));
      var headers = response.Content.Headers;
      ts.TraceEvent(TraceEventType.Verbose, 6, "CONTENT ({0}): {1} bytes", headers.ContentType, headers.ContentLength ?? 0);
    }
    try {
      return await response.EnsureSuccessfulAsync(cancellationToken);
    }
    catch (HttpError error) {
      if (!string.IsNullOrWhiteSpace(error.Content) && error.Content.StartsWith('{')) {
        AuthorizationError? ae;
        try {
          ae = JsonSerializer.Deserialize<AuthorizationError>(error.Content, OAuth2.JsonReaderOptions);
          if (ae is null) {
            throw new JsonException("Error response had null content.");
          }
          ts.TraceEvent(TraceEventType.Verbose, 7, "ERROR '{0}' / '{1}'", ae.Error, ae.Description);
          if (ae.UnhandledProperties is not null) {
            foreach (var prop in ae.UnhandledProperties) {
              ts.TraceEvent(TraceEventType.Verbose, 8, "UNEXPECTED ERROR PROPERTY: {0} -> {1}", prop.Key, prop.Value);
            }
          }
        }
        catch (Exception e) {
          ts.TraceEvent(TraceEventType.Verbose, 9, "FAILED TO PARSE ERROR RESPONSE CONTENT AS JSON: {0}", e.Message);
          ae = null;
        }
        if (ae is not null) {
          throw new HttpError(error.Status, ae.Error, response.Version, ae.Description, error);
        }
      }
      throw;
    }
  }

  private async Task<T> PostAsync<T>(string endPoint, HttpContent? content, CancellationToken cancellationToken) {
    var request = this.PerformRequestAsync(HttpMethod.Post, endPoint, content, null, cancellationToken);
    var response = await request.ConfigureAwait(false);
    var jsonTask = JsonUtils.GetJsonContentAsync<T>(response, OAuth2.JsonReaderOptions, cancellationToken);
    return await jsonTask.ConfigureAwait(false);
  }

  private async Task PostRevocationRequestAsync(string token, string clientSecret, CancellationToken cancellationToken) {
    var body = new StringBuilder();
    body.Append("token=").Append(Uri.EscapeDataString(token));
    body.Append("&\nclient_id=").Append(Uri.EscapeDataString(this.ClientId));
    body.Append("&\nclient_secret=").Append(Uri.EscapeDataString(clientSecret));
    var content = new StringContent(body.ToString(), Encoding.UTF8, OAuth2.TokenRequestBodyType);
    await this.PerformRequestAsync(HttpMethod.Post, OAuth2.RevokeEndPoint, content, null, cancellationToken).ConfigureAwait(false);
  }

  private async Task<IAuthorizationToken> PostTokenRequestAsync(string type, string body, CancellationToken cancellationToken) {
    var content = new StringContent(body, Encoding.UTF8, OAuth2.TokenRequestBodyType);
    var token = await this.PostAsync<AuthorizationToken>(OAuth2.TokenEndPoint, content, cancellationToken).ConfigureAwait(false);
    if (token.TokenType != type) {
      throw new InvalidOperationException($"Token request returned a token of the wrong type ('{token.TokenType}' != '{type}').");
    }
    return token;
  }

  private Task<IAuthorizationToken> RefreshTokenAsync(string type, string codeOrToken, string clientSecret,
                                                      CancellationToken cancellationToken) {
    var body = new StringBuilder();
    body.Append("client_id=").Append(Uri.EscapeDataString(this.ClientId));
    body.Append("&\nclient_secret=").Append(Uri.EscapeDataString(clientSecret));
    body.Append("&\ntoken_type=").Append(Uri.EscapeDataString(type));
    body.Append("&\ngrant_type=refresh_token");
    body.Append("&\nrefresh_token=").Append(Uri.EscapeDataString(codeOrToken));
    return this.PostTokenRequestAsync(type, body.ToString(), cancellationToken);
  }

  private Task<IAuthorizationToken> RequestTokenAsync(string type, string codeOrToken, string clientSecret, Uri redirectUri,
                                                      string? verifier, CancellationToken cancellationToken) {
    var body = new StringBuilder();
    body.Append("client_id=").Append(Uri.EscapeDataString(this.ClientId));
    body.Append("&\nclient_secret=").Append(Uri.EscapeDataString(clientSecret));
    body.Append("&\ntoken_type=").Append(Uri.EscapeDataString(type));
    body.Append("&\ngrant_type=authorization_code");
    body.Append("&\ncode=").Append(Uri.EscapeDataString(codeOrToken));
    body.Append("&\nredirect_uri=").Append(Uri.EscapeDataString(redirectUri.ToString()));
    if (verifier is not null) {
      body.Append("&\ncode_verifier=").Append(Uri.EscapeDataString(verifier));
    }
    return this.PostTokenRequestAsync(type, body.ToString(), cancellationToken);
  }

  private static IEnumerable<string> ScopeStrings(AuthorizationScope scope) {
    if ((scope & AuthorizationScope.Collection) != 0) {
      yield return "collection";
    }
    if ((scope & AuthorizationScope.Email) != 0) {
      yield return "email";
    }
    if ((scope & AuthorizationScope.Profile) != 0) {
      yield return "profile";
    }
    if ((scope & AuthorizationScope.Rating) != 0) {
      yield return "rating";
    }
    if ((scope & AuthorizationScope.SubmitBarcode) != 0) {
      yield return "submit_barcode";
    }
    if ((scope & AuthorizationScope.SubmitIsrc) != 0) {
      yield return "submit_isrc";
    }
    if ((scope & AuthorizationScope.Tag) != 0) {
      yield return "tag";
    }
  }

  #endregion

}
