using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model;
using MetaBrainz.MusicBrainz.Model.Lists;
using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz {

  /// <summary>Class providing access to the MusicBrainz API.</summary>
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
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

    /// <summary>The amount of seconds to leave between requests. Set to 0 (or a negative value) to send all requests as soon as they are made.</summary>
    /// <remarks>
    /// Note that this is a global delay, affecting all threads.
    /// When querying the official musicbrainz site, setting this below the default of one second may incur penalties (ranging from rate limiting to IP bans).
    /// </remarks>
    public static double DelayBetweenRequests {
      get { return Query._requestDelay; }
      set {
        Query._requestDelay = value;
      }
    }

    /// <summary>The URL included in the user agent for requests as part of this library's information.</summary>
    public const string UserAgentUrl = "https://github.com/Zastai/MusicBrainz";

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
      // libmusicbrainz does not validate/change the user agent in any way, so neither do we
      {
        var an = Assembly.GetExecutingAssembly().GetName();
        this._fullUserAgent = $"{this.UserAgent} {an.Name}/v{an.Version} ({Query.UserAgentUrl})";
      }
    }

    #endregion

    #region Public Methods

    #region Lookup

    /// <summary>Performs an MBID-based lookup for the specified entity type.</summary>
    /// <param name="entity">The type of entity to look up.</param>
    /// <param name="mbid">The MBID for the entity to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="type">
    /// The release type to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.ReleaseGroups"/> and/or <see cref="Include.Releases"/>.
    /// </param>
    /// <param name="status">The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <returns>The result of the lookup.</returns>
    /// <exception cref="QueryException">When the serb service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IMetadata Lookup(string entity, Guid mbid, Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null) => this.Lookup(entity, mbid.ToString("D"), inc, type, status);

    /// <summary>Performs a generic identifier-based lookup for the specified entity type.</summary>
    /// <param name="entity">The type of entity to look up.</param>
    /// <param name="id">The identifier for the entity to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="type">
    /// The release type to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.ReleaseGroups"/> and/or <see cref="Include.Releases"/>.
    /// </param>
    /// <param name="status">The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <returns>The result of the lookup.</returns>
    /// <exception cref="QueryException">When the serb service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IMetadata Lookup(string entity, string id, Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null) => this.PerformRequest(entity, id, Query.BuildExtraText(inc, type: type, status: status));

    /// <summary>Looks up the specified area.</summary>
    /// <param name="mbid">The MBID for the area to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested area.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IArea LookupArea(Guid mbid, Include inc = Include.None) => this.Lookup("area", mbid, inc).Area;

    /// <summary>Looks up the specified artist.</summary>
    /// <param name="mbid">The MBID for the artist to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="type">
    /// The release type to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.ReleaseGroups"/> and/or <see cref="Include.Releases"/>.
    /// </param>
    /// <param name="status">The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <returns>The requested artist.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IArtist LookupArtist(Guid mbid, Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null) => this.Lookup("artist", mbid, inc, type, status).Artist;

    /// <summary>Looks up the specified collection.</summary>
    /// <param name="mbid">The MBID for the collection to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested collection.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public ICollection LookupCollection(Guid mbid, Include inc = Include.None) => this.Lookup("collection", mbid, inc).Collection;

    /// <summary>Looks up the specified disc ID.</summary>
    /// <param name="discid">The disc ID to look up.</param>
    /// <param name="toc">
    ///   The TOC (table of contents) to use for a fuzzy lookup if <paramref name="discid"/> has no exact matches.
    ///   The array should contain the first track number, last track number and the address of the disc's lead-out (in sectors),
    ///   followed by the start address of each track (in sectors).
    /// </param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="allMedia">If true, all media types are considered for a fuzzy lookup; otherwise, only CDs are considered.</param>
    /// <param name="noStubs">If true, CD stubs are not returned.</param>
    /// <returns>The result of the disc ID lookup. This can be a single disc or CD stub, or a list of matching releases.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public DiscIdLookupResult LookupDiscId(string discid, int[] toc = null, Include inc = Include.None, bool allMedia = false, bool noStubs = false) => new DiscIdLookupResult(this.PerformRequest("discid", discid, Query.BuildExtraText(inc, allMedia: allMedia, noStubs: noStubs, toc: toc)));

    /// <summary>Looks up the specified event.</summary>
    /// <param name="mbid">The MBID for the event to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested event.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IEvent LookupEvent(Guid mbid, Include inc = Include.None) => this.Lookup("event", mbid, inc).Event;

    /// <summary>Looks up the specified instrument.</summary>
    /// <param name="mbid">The MBID for the instrument to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested instrument.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IInstrument LookupInstrument(Guid mbid, Include inc = Include.None) => this.Lookup("instrument", mbid, inc).Instrument;

    /// <summary>Looks up the recordings associated with the specified ISRC value.</summary>
    /// <param name="isrc">The ISRC to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The recordings associated with the requested ISRC.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IIsrc LookupIsrc(string isrc, Include inc = Include.None) => this.Lookup("isrc", isrc, inc).Isrc;

    /// <summary>Looks up the works associated with the specified ISWC.</summary>
    /// <param name="iswc">The ISWC to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The works associated with the requested ISWC.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IResourceList<IWork> LookupIswc(string iswc, Include inc = Include.None) => this.Lookup("iswc", iswc, inc).WorkList;

    /// <summary>Looks up the specified label.</summary>
    /// <param name="mbid">The MBID for the label to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="type">The release type to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <param name="status">The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <returns>The requested label.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public ILabel LookupLabel(Guid mbid, Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null) => this.Lookup("label", mbid, inc, type, status).Label;

    /// <summary>Looks up the specified place.</summary>
    /// <param name="mbid">The MBID for the place to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested place.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IPlace LookupPlace(Guid mbid, Include inc = Include.None) => this.Lookup("place", mbid, inc).Place;

    /// <summary>Looks up the specified recording.</summary>
    /// <param name="mbid">The MBID for the recording to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="type">The release type to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <param name="status">The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <returns>The requested recording.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IRecording LookupRecording(Guid mbid, Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null) => this.Lookup("recording", mbid, inc, type, status).Recording;

    /// <summary>Looks up the specified release.</summary>
    /// <param name="mbid">The MBID for the release to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested release.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IRelease LookupRelease(Guid mbid, Include inc = Include.None) => this.Lookup("release", mbid, inc).Release;

    /// <summary>Looks up the specified release group.</summary>
    /// <param name="mbid">The MBID for the release group to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="status">The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <returns>The requested release group.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IReleaseGroup LookupReleaseGroup(Guid mbid, Include inc = Include.None, ReleaseStatus? status = null) => this.Lookup("release-group", mbid, inc, status: status).ReleaseGroup;

    /// <summary>Looks up the specified series.</summary>
    /// <param name="mbid">The MBID for the series to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested series.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public ISeries LookupSeries(Guid mbid, Include inc = Include.None) => this.Lookup("series", mbid, inc).Series;

    /// <summary>Looks up the specified URL.</summary>
    /// <param name="mbid">The MBID for the URL to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested URL.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IUrl LookupUrl(Guid mbid, Include inc = Include.None) => this.Lookup("url", mbid, inc).Url;

    /// <summary>Looks up the specified work.</summary>
    /// <param name="mbid">The MBID for the work to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested work.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IWork LookupWork(Guid mbid, Include inc = Include.None) => this.Lookup("work", mbid, inc).Work;

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

    private static readonly ReaderWriterLockSlim RequestLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

    private static readonly XmlSerializer Serializer = new XmlSerializer(typeof(Metadata));

    private static DateTime _lastRequestTime;

    private static double _requestDelay = 1.0;

    private NetworkCredential _credential;

    private readonly string _fullUserAgent;

    private string _lastDigest;

    private static string BuildExtraText(Include inc, int[] toc = null, bool allMedia = false, bool noStubs = false, ReleaseType? type = null, ReleaseStatus? status = null) {
      var sb = new StringBuilder();
      if (inc != Include.None) {
        sb.Append((sb.Length == 0) ? '?' : '&').Append("inc");
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
      if (toc != null)
        sb.Append((sb.Length == 0) ? '?' : '&').Append("toc=").Append(string.Join("+", toc));
      if (allMedia)
        sb.Append((sb.Length == 0) ? '?' : '&').Append("media-format=all");
      if (noStubs)
        sb.Append((sb.Length == 0) ? '?' : '&').Append("cdstubs=no");
      if (type.HasValue) {
        sb.Append((sb.Length == 0) ? '?' : '&').Append("type=");
        switch (type.Value) {
          // Primary Types
          case ReleaseType.Album:       sb.Append("album");          break;
          case ReleaseType.Broadcast:   sb.Append("broadcast");      break;
          case ReleaseType.EP:          sb.Append("ep");             break;
          case ReleaseType.Other:       sb.Append("other");          break;
          case ReleaseType.Single:      sb.Append("single");         break;
          // Secondary Types
          case ReleaseType.Audiobook:   sb.Append("audiobook");      break;
          case ReleaseType.Compilation: sb.Append("compilation");    break;
          case ReleaseType.DJMix:       sb.Append("dj-mix");         break;
          case ReleaseType.Interview:   sb.Append("interview");      break;
          case ReleaseType.Live:        sb.Append("live");           break;
          case ReleaseType.MixTape:     sb.Append("mixtape/street"); break;
          case ReleaseType.Remix:       sb.Append("remix");          break;
          case ReleaseType.Soundtrack:  sb.Append("soundtrack");     break;
          case ReleaseType.SpokenWord:  sb.Append("spokenwork");     break;
        }
      }
      if (status.HasValue) {
        sb.Append((sb.Length == 0) ? '?' : '&').Append("status=");
        switch (status.Value) {
          case ReleaseStatus.Bootleg:       sb.Append("bootleg");        break;
          case ReleaseStatus.Official:      sb.Append("official");       break;
          case ReleaseStatus.Promotional:   sb.Append("promotion");      break; // Docs call it Promotional, UI & ws use "Promotion".
          case ReleaseStatus.PseudoRelease: sb.Append("pseudo-release"); break;
        }
      }
      return sb.ToString();
    }

    private Metadata PerformDirectRequest(string entity, string id, string extra) {
      var uri = new UriBuilder(this.UrlScheme, this.WebSite, this.Port, $"{Query.WebServiceRoot}/{entity}/{id}", extra).Uri;
      Debug.Print($"[{DateTime.UtcNow}] WEB SERVICE REQUEST: {uri}");
      var firstTry = true;
      retry:
      var req = WebRequest.Create(uri) as HttpWebRequest;
      if (req == null)
        throw new InvalidOperationException("Only HTTP-compatible URL schemes are supported.");
      req.Method    = "GET";
      req.Accept    = "application/xml";
      req.UserAgent = this._fullUserAgent;
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
          if (response.ContentType.StartsWith("application/xml"))
            throw new QueryException(we);
        }
        // If not handled in some way, just rethrow the WebException.
        throw;
      }
      // We got a response without any content (probably impossible).
      throw new QueryException("Query did not produce results.");
    }

    private Metadata PerformRequest(string entity, string id, string extra) {
      if (id == null)
        throw new ArgumentNullException(nameof(id));
      id = id.Trim();
      if (id.Length == 0)
        throw new ArgumentException("An entity identifier must not be blank or empty.", nameof(id));
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
