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
using MetaBrainz.MusicBrainz.Json.Readers;
using MetaBrainz.MusicBrainz.Objects;

namespace MetaBrainz.MusicBrainz;

/// <summary>Class providing convenient access to MusicBrainz' OAuth2 service.</summary>
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

  private static int _defaultPort = -1;

  /// <summary>The default port number to use for requests (-1 to not specify any explicit port).</summary>
  public static int DefaultPort {
    get => OAuth2._defaultPort;
    set {
      if (value is < -1 or > 65535) {
        throw new ArgumentOutOfRangeException(nameof(OAuth2.DefaultPort), value,
                                              "The default port number must not be less than -1 or greater than 65535.");
      }
      OAuth2._defaultPort = value;
    }
  }

  private static string _defaultServer = "musicbrainz.org";

  /// <summary>The default server to use for requests.</summary>
  public static string DefaultServer {
    get => OAuth2._defaultServer;
    set {
      if (string.IsNullOrWhiteSpace(value)) {
        throw new ArgumentException("The default server name must not be blank.", nameof(OAuth2.DefaultServer));
      }
      OAuth2._defaultServer = value.Trim();
    }
  }

  private static string _defaultUrlScheme = "https";

  /// <summary>The default URL scheme (internet access protocol) to use for requests.</summary>
  public static string DefaultUrlScheme {
    get => OAuth2._defaultUrlScheme;
    set {
      if (string.IsNullOrWhiteSpace(value)) {
        throw new ArgumentException("The default URL scheme must not be blank.", nameof(OAuth2.DefaultUrlScheme));
      }
      OAuth2._defaultUrlScheme = value.Trim();
    }
  }

  /// <summary>The URI to use for out-of-band authorization.</summary>
  public static readonly Uri OutOfBandUri = new("urn:ietf:wg:oauth:2.0:oob");

  /// <summary>The endpoint used when creating or refreshing a token.</summary>
  public const string TokenEndPoint = "/oauth2/token";

  /// <summary>The content type for a token request body.</summary>
  public const string TokenRequestBodyType = "application/x-www-form-urlencoded";

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

  private int _port = OAuth2.DefaultPort;

  /// <summary>The port number to use for requests (-1 to not specify any explicit port).</summary>
  public int Port {
    get => this._port;
    set {
      if (value is < -1 or > 65535) {
        throw new ArgumentOutOfRangeException(nameof(OAuth2.Port), value,
                                              "The port number must not be less than -1 or greater than 65535.");
      }
      this._port = value;
    }
  }

  private string _server = OAuth2.DefaultServer;

  /// <summary>The web site to use for requests.</summary>
  public string Server {
    get => this._server;
    set {
      if (string.IsNullOrWhiteSpace(value)) {
        throw new ArgumentException("The server name must not be blank.", nameof(OAuth2.Server));
      }
      this._server = value.Trim();
    }
  }

  private string _urlScheme = OAuth2.DefaultUrlScheme;

  /// <summary>The URL scheme (internet access protocol) to use for requests.</summary>
  public string UrlScheme {
    get => this._urlScheme;
    set {
      if (string.IsNullOrWhiteSpace(value)) {
        throw new ArgumentException("The URL scheme must not be blank.", nameof(OAuth2.UrlScheme));
      }
      this._urlScheme = value.Trim();
    }
  }

  #endregion

  #region Public Methods

  /// <summary>Creates the URI to use to request an authorization code.</summary>
  /// <param name="redirectUri">
  /// The URI that should receive the authorization code; use <see cref="OutOfBandUri"/> for out-of-band requests.
  /// </param>
  /// <param name="scope">The authorization scopes that should be included in the authorization code.</param>
  /// <param name="state">An optional string that will be included in the response sent to <paramref name="redirectUri"/>.</param>
  /// <param name="offlineAccess">Requests offline use (a refresh token will be provided alongside the access token).</param>
  /// <param name="forcePrompt">
  /// If <see langword="true"/>, the user will be required to confirm authorization even if the requested scopes have already been
  /// granted.
  /// </param>
  /// <returns>The generated URI.</returns>
  public Uri CreateAuthorizationRequest(Uri redirectUri, AuthorizationScope scope, string? state = null, bool offlineAccess = false,
                                        bool forcePrompt = false) {
    if (scope == AuthorizationScope.None) {
      throw new ArgumentException("At least one authorization scope must be selected.", nameof(scope));
    }
    var query = new StringBuilder();
    query.Append("?response_type=code");
    query.Append("&client_id=").Append(Uri.EscapeDataString(this.ClientId));
    query.Append("&redirect_uri=").Append(Uri.EscapeDataString(redirectUri.ToString()));
    query.Append("&scope=").Append(string.Join("+", OAuth2.ScopeStrings(scope)));
    if (state is not null) {
      query.Append("&state=").Append(Uri.EscapeDataString(state));
    }
    if (offlineAccess) {
      query.Append("&access_type=offline");
    }
    if (forcePrompt) {
      query.Append("&approval_prompt=force");
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
  /// <returns>The obtained bearer token.</returns>
  public IAuthorizationToken GetBearerToken(string code, string clientSecret, Uri redirectUri)
    => AsyncUtils.ResultOf(this.GetBearerTokenAsync(code, clientSecret, redirectUri));

  /// <summary>Exchanges an authorization code for a bearer token.</summary>
  /// <param name="code">The authorization code to be used. If the request succeeds, this code will be invalidated.</param>
  /// <param name="clientSecret">The client secret associated with <see cref="ClientId"/>.</param>
  /// <param name="redirectUri">
  /// The URI to redirect to (or <see cref="OutOfBandUri"/> for out-of-band requests); must match the request URI used to obtain
  /// <paramref name="code"/>.
  /// </param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The obtained bearer token.</returns>
  public Task<IAuthorizationToken> GetBearerTokenAsync(string code, string clientSecret, Uri redirectUri,
                                                       CancellationToken cancellationToken = default)
    => this.RequestTokenAsync("bearer", code, clientSecret, redirectUri, cancellationToken);

  /// <summary>Refreshes a bearer token.</summary>
  /// <param name="refreshToken">The refresh token to use.</param>
  /// <param name="clientSecret">The client secret associated with <see cref="ClientId"/>.</param>
  /// <returns>The obtained bearer token.</returns>
  public IAuthorizationToken RefreshBearerToken(string refreshToken, string clientSecret)
    => AsyncUtils.ResultOf(this.RefreshBearerTokenAsync(refreshToken, clientSecret));

  /// <summary>Refreshes a bearer token.</summary>
  /// <param name="refreshToken">The refresh token to use.</param>
  /// <param name="clientSecret">The client secret associated with <see cref="ClientId"/>.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The obtained bearer token.</returns>
  public Task<IAuthorizationToken> RefreshBearerTokenAsync(string refreshToken, string clientSecret,
                                                           CancellationToken cancellationToken = default)
    => this.RefreshTokenAsync("bearer", refreshToken, clientSecret, cancellationToken);

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
#if NET6_0
      if (this._disposed) {
        throw new ObjectDisposedException(typeof(OAuth2).FullName);
      }
#else
      ObjectDisposedException.ThrowIf(this._disposed, typeof(OAuth2));
#endif
      if (this._client is null) {
        var client = this._clientCreation?.Invoke() ?? new HttpClient();
        this._clientConfiguration?.Invoke(client);
        this._client = client;
      }
      return this._client;
    }
  }

  /// <summary>
  /// Closes the underlying web service client in use by this OAuth2 client, if one has been created.<br/>
  /// The next web service request will create a new client.
  /// </summary>
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

  /// <summary>Finalizes this instance.</summary>
  ~OAuth2() {
    this.Dispose(false);
  }

  #endregion

  #region Internals

  private static readonly JsonSerializerOptions JsonReaderOptions =
    JsonUtils.CreateReaderOptions(AuthorizationTokenReader.Instance, AuthorizationErrorReader.Instance);

  private async Task<HttpResponseMessage> PerformRequestAsync(Uri uri, HttpMethod method, HttpContent? body,
                                                              CancellationToken cancellationToken) {
    Debug.Print($"[{DateTime.UtcNow}] WEB SERVICE REQUEST: {method.Method} {uri}");
    var client = this.Client;
    using var request = new HttpRequestMessage(method, uri);
    request.Content = body;
    request.Headers.Accept.Add(OAuth2.AcceptHeader);
    // Use whatever user agent the client has set, plus our own.
    foreach (var userAgent in client.DefaultRequestHeaders.UserAgent) {
      request.Headers.UserAgent.Add(userAgent);
    }
    request.Headers.UserAgent.Add(OAuth2.LibraryProductInfo);
    request.Headers.UserAgent.Add(OAuth2.LibraryComment);
    Debug.Print("[{0}] => HEADERS: {1}", DateTime.UtcNow, TextUtils.FormatMultiLine(request.Headers.ToString()));
    if (body is not null) {
      // FIXME: Should this include the actual body text too?
      Debug.Print("[{0}] => BODY ({1}): {2} bytes", DateTime.UtcNow, body.Headers.ContentType, body.Headers.ContentLength ?? 0);
    }
    var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
    Debug.Print("[{0}] WEB SERVICE RESPONSE: {1}/{2} '{3}' (v{4})", DateTime.UtcNow, (int) response.StatusCode, response.StatusCode,
                response.ReasonPhrase, response.Version);
    Debug.Print("[{0}] => HEADERS: {1}", DateTime.UtcNow, TextUtils.FormatMultiLine(response.Headers.ToString()));
    Debug.Print("[{0}] => CONTENT ({1}): {2} bytes", DateTime.UtcNow, response.Content.Headers.ContentType,
                response.Content.Headers.ContentLength ?? 0);
    try {
      return await response.EnsureSuccessfulAsync(cancellationToken);
    }
    catch (HttpError error) {
      if (!string.IsNullOrWhiteSpace(error.Content)) {
        AuthorizationError? ae;
        try {
          ae = JsonSerializer.Deserialize<AuthorizationError>(error.Content, OAuth2.JsonReaderOptions);
          if (ae is null) {
            throw new JsonException("Error response had null content.");
          }
          Debug.Print("[{0}] => ERROR '{1}' / '{2}'", DateTime.UtcNow, ae.Error, ae.Description);
          // FIXME: What is the best way to compose this value?
          if (ae.UnhandledProperties != null) {
            foreach (var prop in ae.UnhandledProperties) {
              Debug.Print("[{0}] => UNEXPECTED ERROR PROPERTY: {1} -> {2}", DateTime.UtcNow, prop.Key, prop.Value);
            }
          }
        }
        catch (Exception e) {
          Debug.Print("[{0}] => FAILED TO PARSE ERROR RESPONSE CONTENT AS JSON: {1}", DateTime.UtcNow, e.Message);
          ae = null;
        }
        if (ae != null) {
          throw new HttpError(error.Status, ae.Error, response.Version, ae.Description, error);
        }
      }
      throw;
    }
  }

  private async Task<AuthorizationToken> PostAsync(HttpContent content, CancellationToken cancellationToken) {
    var uri = new UriBuilder(this.UrlScheme, this.Server, this.Port, OAuth2.TokenEndPoint).Uri;
    var response = await this.PerformRequestAsync(uri, HttpMethod.Post, content, cancellationToken).ConfigureAwait(false);
    var jsonTask = JsonUtils.GetJsonContentAsync<AuthorizationToken>(response, OAuth2.JsonReaderOptions, cancellationToken);
    return await jsonTask.ConfigureAwait(false);
  }

  private async Task<IAuthorizationToken> PostAsync(string type, string body, CancellationToken cancellationToken) {
    var content = new StringContent(body, Encoding.UTF8, OAuth2.TokenRequestBodyType);
    var token = await this.PostAsync(content, cancellationToken).ConfigureAwait(false);
    if (token.TokenType != type) {
      throw new InvalidOperationException($"Token request returned a token of the wrong type ('{token.TokenType}' != '{type}').");
    }
    return token;
  }

  private Task<IAuthorizationToken> RefreshTokenAsync(string type, string codeOrToken, string clientSecret,
                                                      CancellationToken cancellationToken) {
    var body = new StringBuilder();
    body.Append("client_id=").Append(Uri.EscapeDataString(this.ClientId));
    body.Append("&client_secret=").Append(Uri.EscapeDataString(clientSecret));
    body.Append("&token_type=").Append(Uri.EscapeDataString(type));
    body.Append("&grant_type=refresh_token");
    body.Append("&refresh_token=").Append(Uri.EscapeDataString(codeOrToken));
    return this.PostAsync(type, body.ToString(), cancellationToken);
  }

  private Task<IAuthorizationToken> RequestTokenAsync(string type, string codeOrToken, string clientSecret, Uri redirectUri,
                                                      CancellationToken cancellationToken) {
    var body = new StringBuilder();
    body.Append("client_id=").Append(Uri.EscapeDataString(this.ClientId));
    body.Append("&client_secret=").Append(Uri.EscapeDataString(clientSecret));
    body.Append("&token_type=").Append(Uri.EscapeDataString(type));
    body.Append("&grant_type=authorization_code");
    body.Append("&code=").Append(Uri.EscapeDataString(codeOrToken));
    body.Append("&redirect_uri=").Append(Uri.EscapeDataString(redirectUri.ToString()));
    return this.PostAsync(type, body.ToString(), cancellationToken);
  }

  private static IEnumerable<string> ScopeStrings(AuthorizationScope scope) {
    if ((scope & AuthorizationScope.Collection) != 0) {
      yield return "collection";
    }
    if ((scope & AuthorizationScope.EMail) != 0) {
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
