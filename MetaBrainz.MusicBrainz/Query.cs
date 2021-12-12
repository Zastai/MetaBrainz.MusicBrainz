using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz;

/// <summary>Class providing access to the MusicBrainz API.</summary>
[PublicAPI]
public sealed partial class Query {

  #region Constants

  /// <summary>
  /// The number of items returned by a browse or search request when no limit (or a limit smaller than 1) is specified.
  /// </summary>
  public const int DefaultPageSize = 25;

  /// <summary>The maximum number of items returned by a single browse or search request.</summary>
  public const int MaximumPageSize = 100;

  /// <summary>The root location of the web service.</summary>
  public const string WebServiceRoot = "/ws/2/";

  #endregion

  #region Static Fields / Properties

  private static int _defaultPort = -1;

  /// <summary>The default port number to use for requests (-1 to not specify any explicit port).</summary>
  public static int DefaultPort {
    get => Query._defaultPort;
    set {
      if (value is < -1 or > 65535) {
        throw new ArgumentOutOfRangeException(nameof(Query.DefaultPort), value,
                                              "The default port number must not be less than -1 or greater than 65535.");
      }
      Query._defaultPort = value;
    }
  }

  private static string _defaultServer = "musicbrainz.org";

  /// <summary>The default server to use for requests.</summary>
  public static string DefaultServer {
    get => Query._defaultServer;
    set {
      if (string.IsNullOrWhiteSpace(value)) {
        throw new ArgumentException("The default server name must not be blank.", nameof(Query.DefaultServer));
      }
      Query._defaultServer = value.Trim();
    }
  }

  private static string _defaultUrlScheme = "https";

  /// <summary>The default internet access protocol to use for requests.</summary>
  public static string DefaultUrlScheme {
    get => Query._defaultUrlScheme;
    set {
      if (string.IsNullOrWhiteSpace(value)) {
        throw new ArgumentException("The default URL scheme must not be blank.", nameof(Query.DefaultUrlScheme));
      }
      Query._defaultUrlScheme = value.Trim();
    }
  }

  /// <summary>The default user agent values to use for requests.</summary>
  public static IList<ProductInfoHeaderValue> DefaultUserAgent { get; } = new List<ProductInfoHeaderValue>();

  /// <summary>
  /// The amount of seconds to leave between requests. Set to 0 (or a negative value) to send all requests as soon as they are
  /// made.
  /// </summary>
  /// <remarks>
  /// Note that this is a global delay, affecting all threads. When querying the official MusicBrainz site, setting this below the
  /// default of one second may incur penalties (ranging from rate limiting to IP bans).
  /// </remarks>
  public static double DelayBetweenRequests { get; set; } = 1.0;

  #endregion

  #region Constructors

  /// <summary>
  /// Initializes a new MusicBrainz query client instance.<br/>
  /// An HTTP client will be created when needed and can be discarded again via the <see cref="Close()"/> method.
  /// </summary>
  public Query() {
    this._clientOwned = true;
  }

  /// <summary>Initializes a new MusicBrainz query client instance using a specific HTTP client.</summary>
  /// <param name="client">The HTTP client to use.</param>
  /// <param name="takeOwnership">
  /// Indicates whether this MusicBrainz query client should take ownership of <paramref name="client"/>.<br/>
  /// If this is <see langword="false"/>, it remains owned by the caller; this means <see cref="Close()"/> will throw an exception
  /// and <see cref="Dispose()"/> will release the reference to <paramref name="client"/> without disposing it.<br/>
  /// If this is <see langword="true"/>, then this object takes ownership and treat it just like an HTTP client it created itself;
  /// this means <see cref="Close()"/> will dispose of it (with further requests creating a new HTTP client) and
  /// <see cref="Dispose()"/> will dispose the HTTP client too. Note that in this case, any default request headers set on
  /// <paramref name="client"/> will <em>not</em> be saved and used for further clients.
  /// </param>
  public Query(HttpClient client, bool takeOwnership = false) {
    this._client = client;
    this._clientOwned = takeOwnership;
  }

  /// <summary>
  /// Initializes a new MusicBrainz query client instance.<br/>
  /// An HTTP client will be created when needed and can be discarded again via the <see cref="Close()"/> method.
  /// </summary>
  /// <param name="userAgent">The user agent values to use for all requests.</param>
  public Query(params ProductInfoHeaderValue[] userAgent) : this() {
    this._userAgent.AddRange(userAgent);
  }

  /// <summary>
  /// Initializes a new MusicBrainz query client instance.<br/>
  /// An HTTP client will be created when needed and can be discarded again via the <see cref="Close()"/> method.
  /// </summary>
  /// <param name="application">The application name to use in the user agent property for all requests.</param>
  /// <param name="version">The version number to use in the user agent property for all requests.</param>
  public Query(string application, Version? version) : this(application, version?.ToString()) {
  }

  /// <summary>
  /// Initializes a new MusicBrainz query client instance.<br/>
  /// An HTTP client will be created when needed and can be discarded again via the <see cref="Close()"/> method.
  /// </summary>
  /// <param name="application">The application name to use in the user agent property for all requests.</param>
  /// <param name="version">The version number to use in the user agent property for all requests.</param>
  /// <param name="contact">
  /// The contact address (typically HTTP[S] or MAILTO) to use in the user agent property for all requests.
  /// </param>
  public Query(string application, Version? version, Uri contact) : this(application, version?.ToString(), contact.ToString()) {
  }

  /// <summary>
  /// Initializes a new MusicBrainz query client instance.<br/>
  /// An HTTP client will be created when needed and can be discarded again via the <see cref="Close()"/> method.
  /// </summary>
  /// <param name="application">The application name to use in the user agent property for all requests.</param>
  /// <param name="version">The version number to use in the user agent property for all requests.</param>
  /// <param name="contact">
  /// The contact address (typically a URL or email address) to use in the user agent property for all requests.
  /// </param>
  public Query(string application, Version? version, string contact) : this(application, version?.ToString(), contact) {
  }

  /// <summary>
  /// Initializes a new MusicBrainz query client instance.<br/>
  /// An HTTP client will be created when needed and can be discarded again via the <see cref="Close()"/> method.
  /// </summary>
  /// <param name="application">The application name to use in the user agent property for all requests.</param>
  /// <param name="version">The version number to use in the user agent property for all requests.</param>
  public Query(string application, string? version) : this(new ProductInfoHeaderValue(application, version)) {
  }

  /// <summary>
  /// Initializes a new MusicBrainz query client instance.<br/>
  /// An HTTP client will be created when needed and can be discarded again via the <see cref="Close()"/> method.
  /// </summary>
  /// <param name="application">The application name to use in the user agent property for all requests.</param>
  /// <param name="version">The version number to use in the user agent property for all requests.</param>
  /// <param name="contact">
  /// The contact address (typically HTTP[S] or MAILTO) to use in the user agent property for all requests.
  /// </param>
  public Query(string application, string? version, Uri contact) : this(application, version, contact.ToString()) {
  }

  /// <summary>
  /// Initializes a new MusicBrainz query client instance.<br/>
  /// An HTTP client will be created when needed and can be discarded again via the <see cref="Close()"/> method.
  /// </summary>
  /// <param name="application">The application name to use in the user agent property for all requests.</param>
  /// <param name="version">The version number to use in the user agent property for all requests.</param>
  /// <param name="contact">
  /// The contact address (typically a URL or email address) to use in the user agent property for all requests.
  /// </param>
  public Query(string application, string? version, string contact)
    : this(new ProductInfoHeaderValue(application, version), new ProductInfoHeaderValue($"({contact})")) {
  }

  #endregion

  #region Instance Fields / Properties

  /// <summary>The base URI for all requests.</summary>
  public Uri BaseUri => new UriBuilder(this.UrlScheme, this.Server, this.Port, Query.WebServiceRoot).Uri;

  /// <summary>The OAuth2 bearer token to use for authenticated requests.</summary>
  public string? BearerToken { get; set; }

  private int _port = Query.DefaultPort;

  /// <summary>The port number to use for requests (-1 to not specify any explicit port).</summary>
  public int Port {
    get => this._port;
    set {
      if (value is < -1 or > 65535) {
        throw new ArgumentOutOfRangeException(nameof(Query.Port), value,
                                              "The port number must not be less than -1 or greater than 65535.");
      }
      this._port = value;
    }
  }

  private string _server = Query.DefaultServer;

  /// <summary>The web site to use for requests.</summary>
  public string Server {
    get => this._server;
    set {
      if (string.IsNullOrWhiteSpace(value)) {
        throw new ArgumentException("The server name must not be blank.", nameof(Query.Server));
      }
      this._server = value.Trim();
    }
  }

  private string _urlScheme = Query.DefaultUrlScheme;

  /// <summary>The internet access protocol to use for requests.</summary>
  public string UrlScheme {
    get => this._urlScheme;
    set {
      if (string.IsNullOrWhiteSpace(value)) {
        throw new ArgumentException("The URL scheme must not be blank.", nameof(Query.UrlScheme));
      }
      this._urlScheme = value.Trim();
    }
  }

  /// <summary>The user agent values to use for requests.</summary>
  /// <remarks>
  /// Note that changes to this list only take effect when a new HTTP client is created. the <see cref="Close()"/> method can be
  /// used to close the current client (if there is one) so that the next request creates a new client.
  /// </remarks>
  public IList<ProductInfoHeaderValue> UserAgent => this._userAgent;

  #endregion

}
