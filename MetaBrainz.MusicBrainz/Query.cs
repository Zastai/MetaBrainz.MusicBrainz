using System;

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

  /// <summary>The URL included in the user agent for requests as part of this library's information.</summary>
  public const string UserAgentUrl = "https://github.com/Zastai/MusicBrainz";

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

  /// <summary>The default user agent to use for requests.</summary>
  public static string? DefaultUserAgent { get; set; }

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

  /// <summary>Creates a new instance of the <see cref="Query"/> class.</summary>
  /// <param name="userAgent">The user agent to use for all requests.</param>
  /// <exception cref="ArgumentNullException">
  /// When <paramref name="userAgent"/> is <see langword="null"/>, and no default was set via <see cref="DefaultUserAgent"/>.
  /// </exception>
  /// <exception cref="ArgumentException">
  /// When the user agent (whether from <paramref name="userAgent"/> or <see cref="DefaultUserAgent"/>) is blank.
  /// </exception>
  public Query(string? userAgent = null) {
    // libmusicbrainz does not validate/change the user agent in any way, so neither do we
    this.UserAgent = userAgent ?? Query.DefaultUserAgent ?? throw new ArgumentNullException(nameof(userAgent));
    if (string.IsNullOrWhiteSpace(this.UserAgent)) {
      throw new ArgumentException("The user agent must not be blank.", nameof(userAgent));
    }
    { // Set full user agent, including this library's information
      var an = typeof(Query).Assembly.GetName();
      this._fullUserAgent = $"{this.UserAgent} {an.Name}/{an.Version} ({Query.UserAgentUrl})";
    }
  }

  /// <summary>Creates a new instance of the <see cref="Query"/> class.</summary>
  /// <param name="application">The application name to use in the user agent property for all requests.</param>
  /// <param name="version">The version number to use in the user agent property for all requests.</param>
  /// <param name="contact">
  /// The contact address (typically HTTP or MAILTO) to use in the user agent property for all requests.
  /// </param>
  /// <exception cref="ArgumentException">When <paramref name="application"/> is blank.</exception>
  public Query(string application, Version version, Uri contact)
    : this(application, version.ToString(), contact.ToString()) {
  }

  /// <summary>Creates a new instance of the <see cref="Query"/> class.</summary>
  /// <param name="application">The application name to use in the user agent property for all requests.</param>
  /// <param name="version">The version number to use in the user agent property for all requests.</param>
  /// <param name="contact">
  /// The contact address (typically a URL or email address) to use in the user agent property for all requests.
  /// </param>
  /// <exception cref="ArgumentNullException">
  /// When <paramref name="application"/>, <paramref name="version"/> and/or <paramref name="contact"/> are <see langword="null"/>.
  /// </exception>
  /// <exception cref="ArgumentException">
  /// When <paramref name="application"/>, <paramref name="version"/> and/or <paramref name="contact"/> are blank.
  /// </exception>
  public Query(string application, string version, string contact) {
    if (string.IsNullOrWhiteSpace(application)) {
      throw new ArgumentException("The application name must not be blank.", nameof(application));
    }
    if (string.IsNullOrWhiteSpace(version)) {
      throw new ArgumentException("The version number must not be blank.", nameof(version));
    }
    if (string.IsNullOrWhiteSpace(contact)) {
      throw new ArgumentException("The contact address must not be blank.", nameof(contact));
    }
    this.UserAgent = $"{application}/{version} ({contact})";
    { // Set full user agent, including this library's information
      var an = typeof(Query).Assembly.GetName();
      this._fullUserAgent = $"{this.UserAgent} {an.Name}/{an.Version} ({Query.UserAgentUrl})";
    }
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

  /// <summary>The user agent to use for requests.</summary>
  public string UserAgent { get; }

  #endregion

}
