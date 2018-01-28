using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace MetaBrainz.MusicBrainz {

  /// <summary>Class providing access to the MusicBrainz API.</summary>
  [SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public sealed partial class Query {

    #region Constants

    /// <summary>The number of items returned by a browse or search request when no limit (or a limit smaller than 1) is specified.</summary>
    public const int DefaultPageSize = 25;

    /// <summary>The maximum number of items returned by a single browse or search request.</summary>
    public const int MaximumPageSize = 100;

    /// <summary>The URL included in the user agent for requests as part of this library's information.</summary>
    public const string UserAgentUrl = "https://github.com/Zastai/MusicBrainz";

    /// <summary>The root location of the web service.</summary>
    public const string WebServiceRoot = "/ws/2";

    #endregion

    #region Static Fields / Properties

    /// <summary>The default port number to use for requests (-1 to not specify any explicit port).</summary>
    public static int DefaultPort { get; set; } = -1;

    /// <summary>The default internet access protocol to use for requests.</summary>
    public static string DefaultUrlScheme { get; set; } = "https";

    /// <summary>The default user agent to use for requests.</summary>
    public static string DefaultUserAgent { get; set; } = null;

    /// <summary>The default web site to use for requests.</summary>
    public static string DefaultWebSite { get; set; } = "musicbrainz.org";

    /// <summary>The amount of seconds to leave between requests. Set to 0 (or a negative value) to send all requests as soon as they are made.</summary>
    /// <remarks>
    ///   Note that this is a global delay, affecting all threads.
    ///   When querying the official MusicBrainz site, setting this below the default of one second may incur penalties (ranging from rate limiting to IP bans).
    /// </remarks>
    public static double DelayBetweenRequests { get; set; } = 1.0;

    #endregion

    #region Constructors

    /// <summary>Creates a new instance of the <see cref="T:Query"/> class.</summary>
    /// <param name="userAgent">The user agent to use for all requests.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="userAgent"/> is null, and no default was set via <see cref="DefaultUserAgent"/>.</exception>
    /// <exception cref="ArgumentException">When the user agent (whether from <paramref name="userAgent"/> or <see cref="DefaultUserAgent"/>) is blank.</exception>
    public Query(string userAgent = null) {
      // libmusicbrainz does not validate/change the user agent in any way, so neither do we
      this.UserAgent = userAgent ?? Query.DefaultUserAgent;
      if (this.UserAgent == null) throw new ArgumentNullException(nameof(userAgent));
      if (this.UserAgent.Trim().Length == 0) throw new ArgumentException("The user agent must not be blank.", nameof(userAgent));
      // Simple Defaults
      this.Port      = Query.DefaultPort;
      this.UrlScheme = Query.DefaultUrlScheme;
      this.WebSite   = Query.DefaultWebSite;
      { // Set full user agent, including this library's information
        var an = Assembly.GetExecutingAssembly().GetName();
        this._fullUserAgent = $"{this.UserAgent} {an.Name}/{an.Version} ({Query.UserAgentUrl})";
      }
    }

    /// <summary>Creates a new instance of the <see cref="T:Query"/> class.</summary>
    /// <param name="application">The application name to use in the user agent property for all requests.</param>
    /// <param name="version">The version number to use in the user agent property for all requests.</param>
    /// <param name="contact">The contact address (typically HTTP or MAILTO) to use in the user agent property for all requests.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="application"/>, <paramref name="version"/> and/or <paramref name="contact"/> are null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="application"/> is blank.</exception>
    public Query(string application, Version version, Uri contact) {
      if (application == null) throw new ArgumentNullException(nameof(application));
      if (version     == null) throw new ArgumentNullException(nameof(version));
      if (contact     == null) throw new ArgumentNullException(nameof(contact));
      if (application.Trim().Length == 0) throw new ArgumentException("The application name must not be blank.", nameof(application));
      this.UserAgent = $"{application}/{version} ({contact})";
      // Simple Defaults
      this.Port      = Query.DefaultPort;
      this.UrlScheme = Query.DefaultUrlScheme;
      this.WebSite   = Query.DefaultWebSite;
      { // Set full user agent, including this library's information
        var an = Assembly.GetExecutingAssembly().GetName();
        this._fullUserAgent = $"{this.UserAgent} {an.Name}/{an.Version} ({Query.UserAgentUrl})";
      }
    }

    /// <summary>Creates a new instance of the <see cref="T:Query"/> class.</summary>
    /// <param name="application">The application name to use in the user agent property for all requests.</param>
    /// <param name="version">The version number to use in the user agent property for all requests.</param>
    /// <param name="contact">The contact address (typically a URL or email address) to use in the user agent property for all requests.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="application"/>, <paramref name="version"/> and/or <paramref name="contact"/> are null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="application"/>, <paramref name="version"/> and/or <paramref name="contact"/> are blank.</exception>
    public Query(string application, string version, string contact) {
      if (application == null) throw new ArgumentNullException(nameof(application));
      if (version     == null) throw new ArgumentNullException(nameof(version));
      if (contact     == null) throw new ArgumentNullException(nameof(contact));
      if (application.Trim().Length == 0) throw new ArgumentException("The application name must not be blank.", nameof(application));
      if (version    .Trim().Length == 0) throw new ArgumentException("The version number must not be blank.",   nameof(version));
      if (contact    .Trim().Length == 0) throw new ArgumentException("The contact address must not be blank.",  nameof(contact));
      this.UserAgent = $"{application}/{version} ({contact})";
      // Simple Defaults
      this.Port      = Query.DefaultPort;
      this.UrlScheme = Query.DefaultUrlScheme;
      this.WebSite   = Query.DefaultWebSite;
      { // Set full user agent, including this library's information
        var an = Assembly.GetExecutingAssembly().GetName();
        this._fullUserAgent = $"{this.UserAgent} {an.Name}/{an.Version} ({Query.UserAgentUrl})";
      }
    }

    #endregion

    #region Instance Fields / Properties

    /// <summary>The OAuth2 bearer token to use for authenticated requests.</summary>
    public string BearerToken { get; set; }

    /// <summary>The port number to use for requests (-1 to not specify any explicit port).</summary>
    public int Port { get; set; }

    /// <summary>The internet access protocol to use for requests.</summary>
    public string UrlScheme { get; set; }

    /// <summary>The user agent to use for requests.</summary>
    public string UserAgent { get; }

    /// <summary>The web site to use for requests.</summary>
    public string WebSite { get; set; }

    /// <summary>The base URI for all requests.</summary>
    public Uri BaseUri => new UriBuilder(this.UrlScheme, this.WebSite, this.Port, Query.WebServiceRoot).Uri;

    #endregion

  }

}
