using System;

namespace MetaBrainz.MusicBrainz {

  /// <summary>Class providing access to the MusicBrainz API.</summary>
  public sealed class Query {

    #region Static Fields / Properties

    static Query() {
      // Mono's C# compiler does not like initializers on auto-properties, so set them up here instead.
      Query.DefaultPort      = -1;
      Query.DefaultUserAgent = null;
      Query.DefaultWebSite   = "musicbrainz.org";
    }

    /// <summary>The default port number to use for requests (-1 to not specify any explicit port).</summary>
    public static int    DefaultPort      { get; set; }

    /// <summary>The default user agent to use for requests.</summary>
    public static string DefaultUserAgent { get; set; }

    /// <summary>The default web site to use for requests.</summary>
    public static string DefaultWebSite   { get; set; }

    #endregion

    #region Constructors

    /// <summary>Creates a new instance of the <see cref="T:Query"/> class.</summary>
    /// <param name="userAgent">The user agent to use for all requests.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="userAgent"/> is null, and no default was set via <see cref="DefaultUserAgent"/>.</exception>
    public Query(string userAgent = null) {
      this.Port      =              Query.DefaultPort;
      this.UserAgent = userAgent ?? Query.DefaultUserAgent;
      this.WebSite   =              Query.DefaultWebSite;
      if (this.UserAgent == null)
        throw new ArgumentNullException(nameof(userAgent));
      // libmetabrainz does not validate/change the user agent in any way, so neither do we
    }

    #endregion

    #region Instance Fields / Properties

    /// <summary>The port number to use for requests (-1 to not specify any explicit port).</summary>
    public int Port { get; set; }

    /// <summary>The user agent to use for all requests.</summary>
    public string UserAgent { get; }

    /// <summary>The web site to use for requests.</summary>
    public string WebSite { get; set; }

    #endregion

  }

}
