using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model;
using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz {

  /// <summary>Class providing access to the MusicBrainz API.</summary>
  public sealed class Query {

    #region Static Fields / Properties

    static Query() {
      // Mono's C# compiler does not like initializers on auto-properties, so set them up here instead.
      Query.DefaultPort      = -1;
      Query.DefaultUrlScheme = "https";
      Query.DefaultUserAgent = null;
      Query.DefaultWebSite   = "musicbrainz.org";
    }

    /// <summary>The default port number to use for requests (-1 to not specify any explicit port).</summary>
    public static int DefaultPort { get; set; }

    /// <summary>The default internet access protocol to use for requests.</summary>
    public static string DefaultUrlScheme { get; set; }

    /// <summary>The default user agent to use for requests.</summary>
    public static string DefaultUserAgent { get; set; }

    /// <summary>The default web site to use for requests.</summary>
    public static string DefaultWebSite { get; set; }

    /// <summary>
    ///   The amount of seconds to leave between requests. Set to 0 (or a negative value) to send all requests as soon as they are made.
    /// </summary>
    /// <remarks>
    ///   Note that this is a global delay, affecting all threads.
    ///   When querying the official musicbrainz site, setting this below the default of one second may incur penalties (ranging from rate limiting to IP bans).
    /// </remarks>
    public static double DelayBetweenRequests {
      get { return Query._requestDelay; }
      set {
        Query._requestDelay = value;
      }
    }

    /// <summary>The root location of the web service.</summary>
    public const string WebServiceRoot = "/ws/2";

    #endregion

    #region Constructors

    /// <summary>Creates a new instance of the <see cref="T:Query"/> class.</summary>
    /// <param name="userAgent">The user agent to use for all requests.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="userAgent"/> is null, and no default was set via <see cref="DefaultUserAgent"/>.</exception>
    public Query(string userAgent = null) {
      this.Port      =              Query.DefaultPort;
      this.UrlScheme =              Query.DefaultUrlScheme;
      this.UserAgent = userAgent ?? Query.DefaultUserAgent;
      this.WebSite   =              Query.DefaultWebSite;
      if (this.UserAgent == null)
        throw new ArgumentNullException(nameof(userAgent));
      // libmetabrainz does not validate/change the user agent in any way, so neither do we
    }

    #endregion

    #region Public Methods

    #region Lookup

    /// <summary>Looks up the specified area.</summary>
    /// <param name="mbid">The MBID for the area to look up.</param>
    /// <param name="inc">Additional information to include in the response.</param>
    /// <returns>The requested area.</returns>
    /// <exception cref="QueryException">When the serb service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public Area LookupArea(Guid mbid, Include inc = Include.None) => this.PerformRequest("area", mbid, Query.BuildExtraText(inc)).Area;

    public Artist LookupArtist(Guid mbid, Include inc = Include.None) => this.PerformRequest("artist", mbid, Query.BuildExtraText(inc)).Artist;

    public Collection LookupCollection(Guid mbid, Include inc = Include.None) => this.PerformRequest("collection", mbid, Query.BuildExtraText(inc)).Collection;

    public DiscIdLookupResult LookupDiscId(string discid, Include inc = Include.None) => new DiscIdLookupResult(this.PerformRequest("discid", discid, Query.BuildExtraText(inc)));

    public Event LookupEvent(Guid mbid, Include inc = Include.None) => this.PerformRequest("event", mbid, Query.BuildExtraText(inc)).Event;

    public Instrument LookupInstrument(Guid mbid, Include inc = Include.None) => this.PerformRequest("instrument", mbid, Query.BuildExtraText(inc)).Instrument;

    public Isrc LookupIsrc(string isrc, Include inc = Include.None) => this.PerformRequest("isrc", isrc, Query.BuildExtraText(inc)).Isrc;

    public WorkList LookupIswc(string iswc, Include inc = Include.None) => this.PerformRequest("iswc", iswc, Query.BuildExtraText(inc)).WorkList;

    public Label LookupLabel(Guid mbid, Include inc = Include.None) => this.PerformRequest("label", mbid, Query.BuildExtraText(inc)).Label;

    public Place LookupPlace(Guid mbid, Include inc = Include.None) => this.PerformRequest("place", mbid, Query.BuildExtraText(inc)).Place;

    public Recording LookupRecording(Guid mbid, Include inc = Include.None) => this.PerformRequest("recording", mbid, Query.BuildExtraText(inc)).Recording;

    public Release LookupRelease(Guid mbid, Include inc = Include.None) => this.PerformRequest("release", mbid, Query.BuildExtraText(inc)).Release;

    public ReleaseGroup LookupReleaseGroup(Guid mbid, Include inc = Include.None) => this.PerformRequest("release-group", mbid, Query.BuildExtraText(inc)).ReleaseGroup;

    public Series LookupSeries(Guid mbid, Include inc = Include.None) => this.PerformRequest("series", mbid, Query.BuildExtraText(inc)).Series;

    public Url LookupUrl(Guid mbid, Include inc = Include.None) => this.PerformRequest("url", mbid, Query.BuildExtraText(inc)).Url;

    public Work LookupWork(Guid mbid, Include inc = Include.None) => this.PerformRequest("work", mbid, Query.BuildExtraText(inc)).Work;

    #endregion

    #endregion

    #region Instance Fields / Properties

    /// <summary>The OAuth2 bearer token to use for authenticated requests; takes precedence over <see cref="Credential"/>.</summary>
    public string BearerToken { get; set; }

    /// <summary>The credential to use for authenticated requests; not used if <see cref="BearerToken"/> is also set.</summary>
    /// <remarks>The user name is <em>case sensitive</em> (unlike the logon on the MusicBrainz website).</remarks>
    public NetworkCredential Credential {
      get { return this._credential; }
      set {
        this._credential = value;
        this._lastDigest = null;
      }
    }

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

    #region Internals

    private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(Metadata));

    private static readonly ReaderWriterLockSlim RequestLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

    private static DateTime _lastRequestTime;

    private static double _requestDelay = 1.0;

    private NetworkCredential _credential;

    private string _lastDigest;

    private static string BuildExtraText(Include inc) {
      var sb = new StringBuilder();
      if (inc != Include.None) {
        sb.Append((sb.Length == 0) ? '?' : '&');
        sb.Append("inc");
        var letter = '=';
        // Linked Entities
        if ((inc & Include.Artists)       != 0) { sb.Append(letter).Append("artists");        letter = '+'; }
        if ((inc & Include.Collections)   != 0) { sb.Append(letter).Append("collections");    letter = '+'; }
        if ((inc & Include.Labels)        != 0) { sb.Append(letter).Append("labels");         letter = '+'; }
        if ((inc & Include.Recordings)    != 0) { sb.Append(letter).Append("recordings");     letter = '+'; }
        if ((inc & Include.ReleaseGroups) != 0) { sb.Append(letter).Append("release-groups"); letter = '+'; }
        if ((inc & Include.Releases)      != 0) { sb.Append(letter).Append("releases");       letter = '+'; }
        if ((inc & Include.Works)         != 0) { sb.Append(letter).Append("works");          letter = '+'; }
        // Special Cases
        if ((inc & Include.ArtistCredits)   != 0) { sb.Append(letter).Append("artist-credits");   letter = '+'; }
        if ((inc & Include.DiscIds)         != 0) { sb.Append(letter).Append("discids");          letter = '+'; }
        if ((inc & Include.Isrcs)           != 0) { sb.Append(letter).Append("isrcs");            letter = '+'; }
        if ((inc & Include.Media)           != 0) { sb.Append(letter).Append("media");            letter = '+'; }
        if ((inc & Include.UserCollections) != 0) { sb.Append(letter).Append("user-collections"); letter = '+'; }
        if ((inc & Include.VariousArtists)  != 0) { sb.Append(letter).Append("various-artists");  letter = '+'; }
        // Optional Info
        if ((inc & Include.Aliases)     != 0) { sb.Append(letter).Append("aliases");      letter = '+'; }
        if ((inc & Include.Annotation)  != 0) { sb.Append(letter).Append("annotation");   letter = '+'; }
        if ((inc & Include.Ratings)     != 0) { sb.Append(letter).Append("ratings");      letter = '+'; }
        if ((inc & Include.Tags)        != 0) { sb.Append(letter).Append("tags");         letter = '+'; }
        if ((inc & Include.UserRatings) != 0) { sb.Append(letter).Append("user-ratings"); letter = '+'; }
        if ((inc & Include.UserTags)    != 0) { sb.Append(letter).Append("user-tags");    letter = '+'; }
        // Relationships
        if ((inc & Include.AreaRelationships)           != 0) { sb.Append(letter).Append("area-rels");            letter = '+'; }
        if ((inc & Include.ArtistRelationships)         != 0) { sb.Append(letter).Append("artist-rels");          letter = '+'; }
        if ((inc & Include.EventRelationships)          != 0) { sb.Append(letter).Append("event-rels");           letter = '+'; }
        if ((inc & Include.InstrumentRelationships)     != 0) { sb.Append(letter).Append("instrument-rels");      letter = '+'; }
        if ((inc & Include.LabelRelationships)          != 0) { sb.Append(letter).Append("label-rels");           letter = '+'; }
        if ((inc & Include.PlaceRelationships)          != 0) { sb.Append(letter).Append("place-rels");           letter = '+'; }
        if ((inc & Include.RecordingLevelRelationships) != 0) { sb.Append(letter).Append("recording-level-rels"); letter = '+'; }
        if ((inc & Include.RecordingRelationships)      != 0) { sb.Append(letter).Append("recording-rels");       letter = '+'; }
        if ((inc & Include.ReleaseGroupRelationships)   != 0) { sb.Append(letter).Append("release-group-rels");   letter = '+'; }
        if ((inc & Include.ReleaseRelationships)        != 0) { sb.Append(letter).Append("release-rels");         letter = '+'; }
        if ((inc & Include.SeriesRelationships)         != 0) { sb.Append(letter).Append("series-rels");          letter = '+'; }
        if ((inc & Include.UrlRelationships)            != 0) { sb.Append(letter).Append("url-rels");             letter = '+'; }
        if ((inc & Include.WorkLevelRelationships)      != 0) { sb.Append(letter).Append("work-level-rels");      letter = '+'; }
        if ((inc & Include.WorkRelationships)           != 0) { sb.Append(letter).Append("work-rels");            letter = '+'; }
      }
      return sb.ToString();
    }

    private Metadata PerformDirectRequest(string entity,string id, string extra) {
      var uri = new UriBuilder(this.UrlScheme, this.WebSite, this.Port, $"{Query.WebServiceRoot}/{entity}/{id}", extra).Uri;
      Debug.Print($"[{DateTime.UtcNow}] WEB SERVICE REQUEST: {uri}");
      var firstTry = true;
    retry:
      var req = WebRequest.Create(uri) as HttpWebRequest;
      if (req == null)
        throw new InvalidOperationException("Only HTTP-compatible URL schemes are supported.");
      req.Method = "GET";
      req.Accept = "application/xml";
      {
        var an = Assembly.GetExecutingAssembly().GetName();
        req.UserAgent = $"{this.UserAgent} {an.Name}/v{an.Version}";
      }
      if (this.BearerToken != null)
        req.Headers.Add("Authorization", $"Bearer {this.BearerToken}");
      else if (this._lastDigest != null)
        req.Headers.Add("Authorization", this._lastDigest);
      try {
        using (var response = (HttpWebResponse) req.GetResponse()) {
          using (var stream = response.GetResponseStream()) {
            if (stream != null)
              return (Metadata) Query.Serializer.Deserialize(stream);
          }
        }
      }
      catch (WebException we) {
        var response = we.Response as HttpWebResponse;
        if (response != null) {
          if (firstTry && response.StatusCode == HttpStatusCode.Unauthorized) {
            firstTry = false; // only retry authentication once
            var digest = HttpDigestHelper.GetDigest(response, this.Credential);
            if (digest != null && this._lastDigest != digest) {
              this._lastDigest = digest;
              goto retry;
            }
          }
          // FIXME: Is there a better way to be sure it's an MB WS error response?
          if (response.ContentLength > 0 && response.ContentType.StartsWith("application/xml"))
            throw new QueryException(we);
        }
        // If not handled in some way, just rethrow the WebException.
        throw;
      }
      // We got a response without any content (probably impossible).
      throw new QueryException("Query did not produce results.");
    }

    private Metadata PerformRequest(string entity, Guid id, string extra) => this.PerformRequest(entity, id.ToString("D"), extra);

    private Metadata PerformRequest(string entity, string id, string extra) {
      if (Query._requestDelay <= 0.0)
        return this.PerformDirectRequest(entity, id, extra);
      while (true) {
        Query.RequestLock.EnterWriteLock();
        try {
          if ((DateTime.UtcNow - Query._lastRequestTime).TotalSeconds >= Query._requestDelay) {
            try {
              return this.PerformDirectRequest(entity, id, extra);
            }
            finally {
              Query._lastRequestTime = DateTime.UtcNow;
            }
          }
        }
        finally {
          Query.RequestLock.ExitWriteLock();
        }
        Thread.Sleep((int) (500 * Query._requestDelay));
      }
    }

    #endregion

  }

}
