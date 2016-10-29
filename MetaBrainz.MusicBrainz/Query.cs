// This will not work until https://github.com/metabrainz/musicbrainz-server/pull/385 is merged.
//#define SUBMIT_ACCEPT_JSON

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.XPath;

using MetaBrainz.MusicBrainz.Entities;
using MetaBrainz.MusicBrainz.Entities.Objects;
using MetaBrainz.MusicBrainz.Submissions;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz {

  /// <summary>Class providing access to the MusicBrainz API.</summary>
  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
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
    /// <exception cref="ArgumentException">When the user agent (whether from <paramref name="userAgent"/> or <see cref="DefaultUserAgent"/>) is blank.</exception>
    public Query(string userAgent = null) {
      // libmusicbrainz does not validate/change the user agent in any way, so neither do we
      this.UserAgent = userAgent ?? Query.DefaultUserAgent;
      if (this.UserAgent == null) throw new ArgumentNullException(nameof(userAgent));
      if (string.IsNullOrWhiteSpace(userAgent)) throw new ArgumentException("The user agent must not be blank.", nameof(userAgent));
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
    /// <param name="application">The applciation name to use in the user agent property for all requests.</param>
    /// <param name="version">The version number to use in the user agent property for all requests.</param>
    /// <param name="contact">The contact address (typically HTTP or MAILTO) to use in the user agent property for all requests.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="application"/>, <paramref name="version"/> and/or <paramref name="contact"/> are null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="application"/> is blank.</exception>
    public Query(string application, Version version, Uri contact) {
      if (application == null) throw new ArgumentNullException(nameof(application));
      if (version     == null) throw new ArgumentNullException(nameof(version));
      if (contact     == null) throw new ArgumentNullException(nameof(contact));
      if (string.IsNullOrWhiteSpace(application)) throw new ArgumentException("The application name must not be blank.", nameof(application));
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
    /// <param name="application">The applciation name to use in the user agent property for all requests.</param>
    /// <param name="version">The version number to use in the user agent property for all requests.</param>
    /// <param name="contact">The contact address (typically a URL or email address) to use in the user agent property for all requests.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="application"/>, <paramref name="version"/> and/or <paramref name="contact"/> are null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="application"/>, <paramref name="version"/> and/or <paramref name="contact"/> are blank.</exception>
    public Query(string application, string version, string contact) {
      if (application == null) throw new ArgumentNullException(nameof(application));
      if (version     == null) throw new ArgumentNullException(nameof(version));
      if (contact     == null) throw new ArgumentNullException(nameof(contact));
      if (string.IsNullOrWhiteSpace(application)) throw new ArgumentException("The application name must not be blank.", nameof(application));
      if (string.IsNullOrWhiteSpace(version    )) throw new ArgumentException("The version number must not be blank.",   nameof(version));
      if (string.IsNullOrWhiteSpace(contact    )) throw new ArgumentException("The contact address must not be blank.",  nameof(contact));
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

    #region Public Methods

    #region Lookup

    /// <summary>Looks up the specified area.</summary>
    /// <param name="mbid">The MBID for the area to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested area.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IArea LookupArea(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("area", mbid.ToString("D"), Query.BuildExtraText(inc));
      return new Area(JsonConvert.DeserializeObject<Area.JSON>(json, Query.SerializerSettings));
    }

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
    public IArtist LookupArtist(Guid mbid, Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null) {
      var json = this.PerformRequest("artist", mbid.ToString("D"), Query.BuildExtraText(inc, type: type, status: status));
      return new Artist(JsonConvert.DeserializeObject<Artist.JSON>(json, Query.SerializerSettings));
    }

    /// <summary>Looks up the specified collection.</summary>
    /// <param name="mbid">The MBID for the collection to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested collection.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public ICollection LookupCollection(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("collection", mbid.ToString("D"), Query.BuildExtraText(inc));
      return new Collection(JsonConvert.DeserializeObject<Collection.JSON>(json, Query.SerializerSettings));
    }

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
    public DiscIdLookupResult LookupDiscId(string discid, int[] toc = null, Include inc = Include.None, bool allMedia = false, bool noStubs = false) {
      return new DiscIdLookupResult(this.PerformRequest("discid", discid, Query.BuildExtraText(inc, allMedia: allMedia, noStubs: noStubs, toc: toc)), Query.SerializerSettings);
    }

    /// <summary>Looks up the specified event.</summary>
    /// <param name="mbid">The MBID for the event to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested event.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IEvent LookupEvent(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("event", mbid.ToString("D"), Query.BuildExtraText(inc));
      return new Event(JsonConvert.DeserializeObject<Event.JSON>(json, Query.SerializerSettings));
    }

    /// <summary>Looks up the specified instrument.</summary>
    /// <param name="mbid">The MBID for the instrument to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested instrument.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IInstrument LookupInstrument(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("instrument", mbid.ToString("D"), Query.BuildExtraText(inc));
      return new Instrument(JsonConvert.DeserializeObject<Instrument.JSON>(json, Query.SerializerSettings));
    }

    /// <summary>Looks up the recordings associated with the specified ISRC value.</summary>
    /// <param name="isrc">The ISRC to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The recordings associated with the requested ISRC.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IIsrc LookupIsrc(string isrc, Include inc = Include.None) {
      var json = this.PerformRequest("isrc", isrc, Query.BuildExtraText(inc));
      return new Isrc(JsonConvert.DeserializeObject<Isrc.JSON>(json, Query.SerializerSettings));
    }

    /// <summary>Looks up the works associated with the specified ISWC.</summary>
    /// <param name="iswc">The ISWC to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The works associated with the requested ISWC.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IEnumerable<IWork> LookupIswc(string iswc, Include inc = Include.None) {
      var json = this.PerformRequest("iswc", iswc, Query.BuildExtraText(inc));
      var jobj = JsonConvert.DeserializeObject<BrowseWorksResult>(json);
      if (jobj?.works == null)
        return null;
      var arr = new IWork[jobj.work_count];
      for (var i = 0; i < jobj.work_count; ++i)
        arr[i] = new Work(jobj.works[i]);
      return arr;
    }

    /// <summary>Looks up the specified label.</summary>
    /// <param name="mbid">The MBID for the label to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="type">The release type to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <param name="status">The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <returns>The requested label.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public ILabel LookupLabel(Guid mbid, Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null) {
      var json = this.PerformRequest("label", mbid.ToString("D"), Query.BuildExtraText(inc, type: type, status: status));
      return new Label(JsonConvert.DeserializeObject<Label.JSON>(json, Query.SerializerSettings));
    }

    /// <summary>Looks up the specified place.</summary>
    /// <param name="mbid">The MBID for the place to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested place.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IPlace LookupPlace(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("place", mbid.ToString("D"), Query.BuildExtraText(inc));
      return new Place(JsonConvert.DeserializeObject<Place.JSON>(json, Query.SerializerSettings));
    }

    /// <summary>Looks up the specified recording.</summary>
    /// <param name="mbid">The MBID for the recording to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="type">The release type to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <param name="status">The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <returns>The requested recording.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IRecording LookupRecording(Guid mbid, Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null) {
      var json = this.PerformRequest("recording", mbid.ToString("D"), Query.BuildExtraText(inc, type: type, status: status));
      return new Recording(JsonConvert.DeserializeObject<Recording.JSON>(json, Query.SerializerSettings));
    }

    /// <summary>Looks up the specified release.</summary>
    /// <param name="mbid">The MBID for the release to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested release.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IRelease LookupRelease(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("release", mbid.ToString("D"), Query.BuildExtraText(inc));
      return new Release(JsonConvert.DeserializeObject<Release.JSON>(json, Query.SerializerSettings));
    }

    /// <summary>Looks up the specified release group.</summary>
    /// <param name="mbid">The MBID for the release group to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="status">The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <returns>The requested release group.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IReleaseGroup LookupReleaseGroup(Guid mbid, Include inc = Include.None, ReleaseStatus? status = null) {
      var json = this.PerformRequest("release-group", mbid.ToString("D"), Query.BuildExtraText(inc, status: status));
      return new ReleaseGroup(JsonConvert.DeserializeObject<ReleaseGroup.JSON>(json, Query.SerializerSettings));
    }

    /// <summary>Looks up the specified series.</summary>
    /// <param name="mbid">The MBID for the series to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested series.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public ISeries LookupSeries(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("series", mbid.ToString("D"), Query.BuildExtraText(inc));
      return new Series(JsonConvert.DeserializeObject<Series.JSON>(json, Query.SerializerSettings));
    }

    /// <summary>Looks up the specified URL.</summary>
    /// <param name="mbid">The MBID for the URL to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested URL.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IUrl LookupUrl(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("url", mbid.ToString("D"), Query.BuildExtraText(inc));
      return new Url(JsonConvert.DeserializeObject<Url.JSON>(json, Query.SerializerSettings));
    }

    /// <summary>Looks up the specified work.</summary>
    /// <param name="mbid">The MBID for the work to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested work.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IWork LookupWork(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("work", mbid.ToString("D"), Query.BuildExtraText(inc));
      return new Work(JsonConvert.DeserializeObject<Work.JSON>(json, Query.SerializerSettings));
    }

    #endregion

    #region Browse

    #pragma warning disable 169
    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private sealed class BrowseWorksResult {
      [JsonProperty] public Work.JSON[] works;
      [JsonProperty("work-count")] public int work_count;
      [JsonProperty("work-offset")] public int work_offset;
    }

    #pragma warning restore 169
    #pragma warning restore 649

    #endregion

    #region Collection Management

    #region Adding Items

    /// <summary>Adds the specified items to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="entityType">The entity type for the collection identified by <paramref name="collection"/>.</param>
    /// <param name="items">The MBIDs of the items to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, CollectionEntityType entityType, params Guid[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, entityType).Add(items));

    /// <summary>Adds the specified areas to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The areas to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params IArea[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, CollectionEntityType.Area).Add(items));

    /// <summary>Adds the specified artists to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The artists to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params IArtist[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, CollectionEntityType.Artist).Add(items));

    /// <summary>Adds the specified events to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The events to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params IEvent[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, CollectionEntityType.Event).Add(items));

    /// <summary>Adds the specified instruments to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The instruments to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params IInstrument[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, CollectionEntityType.Instrument).Add(items));

    /// <summary>Adds the specified labels to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The labels to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params ILabel[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, CollectionEntityType.Label).Add(items));

    /// <summary>Adds the specified places to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The places to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params IPlace[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, CollectionEntityType.Place).Add(items));

    /// <summary>Adds the specified recordings to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The recordings to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params IRecording[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, CollectionEntityType.Recording).Add(items));

    /// <summary>Adds the specified releases to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The releases to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params IRelease[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, CollectionEntityType.Release).Add(items));

    /// <summary>Adds the specified release groups to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The release groups to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params IReleaseGroup[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, CollectionEntityType.ReleaseGroup).Add( items));

    /// <summary>Adds the specified series to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The series to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params ISeries[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, CollectionEntityType.Series).Add(items));

    /// <summary>Adds the specified works to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The works to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, Guid collection, params IWork[] items) => this.PerformSubmission(new ModifyCollection("PUT", client, collection, CollectionEntityType.Work).Add(items));

    /// <summary>Adds the specified items to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The MBIDs of the items to add to <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> and/or <paramref name="collection"/> are null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, ICollection collection, params Guid[] items) {
      if (collection == null) throw new ArgumentNullException(nameof(collection));
      return this.PerformSubmission(new ModifyCollection("PUT", client, collection.MbId, collection.EntityType).Add(items));
    }

    /// <summary>Adds the specified items to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The collection to add <paramref name="items"/> to.</param>
    /// <param name="items">The items to add to <paramref name="collection"/>. They should be of the appropriate type for the collection.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> and/or <paramref name="collection"/> are null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string AddToCollection(string client, ICollection collection, params IMbEntity[] items) {
      if (collection == null) throw new ArgumentNullException(nameof(collection));
      return this.PerformSubmission(new ModifyCollection("PUT", client, collection.MbId, collection.EntityType).Add(items));
    }

    #endregion

    #region Removing Items

    /// <summary>Removes the specified items to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="entityType">The entity type for the collection identified by <paramref name="collection"/>.</param>
    /// <param name="items">The MBIDs of the items to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, CollectionEntityType entityType, params Guid[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, entityType).Add(items));

    /// <summary>Removes the specified areas to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The areas to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params IArea[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, CollectionEntityType.Area).Add(items));

    /// <summary>Removes the specified artists to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The artists to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params IArtist[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, CollectionEntityType.Artist).Add(items));

    /// <summary>Removes the specified events to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The events to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params IEvent[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, CollectionEntityType.Event).Add(items));

    /// <summary>Removes the specified instruments to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The instruments to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params IInstrument[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, CollectionEntityType.Instrument).Add(items));

    /// <summary>Removes the specified labels to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The labels to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params ILabel[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, CollectionEntityType.Label).Add(items));

    /// <summary>Removes the specified places to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The places to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params IPlace[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, CollectionEntityType.Place).Add(items));

    /// <summary>Removes the specified recordings to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The recordings to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params IRecording[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, CollectionEntityType.Recording).Add(items));

    /// <summary>Removes the specified releases to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The releases to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params IRelease[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, CollectionEntityType.Release).Add(items));

    /// <summary>Removes the specified release groups to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The release groups to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params IReleaseGroup[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, CollectionEntityType.ReleaseGroup).Add( items));

    /// <summary>Removes the specified series to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The series to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params ISeries[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, CollectionEntityType.Series).Add(items));

    /// <summary>Removes the specified works to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The MBID of the collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The works to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, Guid collection, params IWork[] items) => this.PerformSubmission(new ModifyCollection("DELETE", client, collection, CollectionEntityType.Work).Add(items));

    /// <summary>Removes the specified items to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The MBIDs of the items to remove from <paramref name="collection"/>.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> and/or <paramref name="collection"/> are null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, ICollection collection, params Guid[] items) {
      if (collection == null) throw new ArgumentNullException(nameof(collection));
      return this.PerformSubmission(new ModifyCollection("DELETE", client, collection.MbId, collection.EntityType).Add(items));
    }

    /// <summary>Removes the specified items to the specified collection.</summary>
    /// <param name="client">
    ///   The ID of the client software making this request.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    /// </param>
    /// <param name="collection">The collection to remove <paramref name="items"/> from.</param>
    /// <param name="items">The items to remove from <paramref name="collection"/>. They should be of the appropriate type for the collection.</param>
    /// <returns>A message describing the result (usually "OK").</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> and/or <paramref name="collection"/> are null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    /// <exception cref="QueryException">When the MusicBrainz web service reports an error.</exception>
    /// <exception cref="WebException">When the MusicBrainz web service could not be contacted.</exception>
    public string RemoveFromCollection(string client, ICollection collection, params IMbEntity[] items) {
      if (collection == null) throw new ArgumentNullException(nameof(collection));
      return this.PerformSubmission(new ModifyCollection("DELETE", client, collection.MbId, collection.EntityType).Add(items));
    }

    #endregion

    #endregion

    #region Submissions

    /// <summary>Creates a submission request for setting a barcode on one or more releases.</summary>
    /// <param name="client">
    ///   The ID of the client software submitting data.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    ///   It will be included in the edit(s) registered by the MusicBrainz server for this submission.
    /// </param>
    /// <returns>A new barcode submission request (to be executed via a call to <see cref="BarcodeSubmission.Submit()"/>).</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    public BarcodeSubmission SubmitBarcodes(string client) {
      if (client == null) throw new ArgumentNullException(nameof(client));
      if (string.IsNullOrWhiteSpace(client)) throw new ArgumentException("The client ID must not be blank.", nameof(client));
      return new BarcodeSubmission(this, client);
    }

    /// <summary>Creates a submission request for adding one or more ISRCs to one or more recordings.</summary>
    /// <param name="client">
    ///   The ID of the client software submitting data.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    ///   It will be included in the edit(s) registered by the MusicBrainz server for this submission.
    /// </param>
    /// <returns>A new ISRC submission request (to be executed via a call to <see cref="IsrcSubmission.Submit()"/>).</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    public IsrcSubmission SubmitIsrcs(string client) {
      if (client == null) throw new ArgumentNullException(nameof(client));
      if (string.IsNullOrWhiteSpace(client)) throw new ArgumentException("The client ID must not be blank.", nameof(client));
      return new IsrcSubmission(this, client);
    }

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

    #region Message / Error Handling

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private sealed class MessageOrError {
      [JsonProperty] public string error;
      [JsonProperty] public string message;
    }

    #pragma warning restore 649

    private static string ExtractError(HttpWebResponse response) {
      if (response == null || response.ContentLength == 0)
        return null;
      try {
        using (var stream = response.GetResponseStream()) {
          if (stream == null)
            return null;
          if (response.ContentType.StartsWith("application/xml")) {
            StringBuilder sb = null;
            var xpath = new XPathDocument(stream).CreateNavigator().Select("/error/text");
            while (xpath.MoveNext()) {
              if (sb == null)
                sb = new StringBuilder();
              else
                sb.AppendLine();
              sb.Append(xpath.Current.InnerXml);
            }
            Debug.Print($"[{DateTime.UtcNow}] => ERROR (XML): \"{sb}\"");
            return sb?.ToString();
          }
          if (response.ContentType.StartsWith("application/json")) {
            var encname = response.CharacterSet;
            if (string.IsNullOrWhiteSpace(encname))
              encname = "utf-8";
            var enc = Encoding.GetEncoding(encname);
            using (var sr = new StreamReader(stream, enc)) {
              var moe = JsonConvert.DeserializeObject<MessageOrError>(sr.ReadToEnd());
              Debug.Print($"[{DateTime.UtcNow}] => ERROR (JSON): \"{moe?.error}\"");
              return moe?.error;
            }
          }
        }
      }
      catch { /* keep calm and fall through */ }
      return null;
    }

    private static string ExtractMessage(HttpWebResponse response) {
      if (response == null)
        return null;
      if (response.ContentLength == 0)
        return null;
      try {
        using (var stream = response.GetResponseStream()) {
          if (stream == null)
            return null;
          if (response.ContentType.StartsWith("application/xml")) {
            StringBuilder sb = null;
            // Unlike an error, this is a <metadata> document with a namespace, requiring more effort to get the text out.
            var nav = new XPathDocument(stream).CreateNavigator();
            XmlNamespaceManager ns = null;
            if (nav.NameTable != null) {
              ns = new XmlNamespaceManager(nav.NameTable);
              ns.AddNamespace("mb", "http://musicbrainz.org/ns/mmd-2.0#");
            }
            var xpath = nav.Select("/mb:metadata/mb:message/mb:text", ns);
            while (xpath.MoveNext()) {
              if (sb == null)
                sb = new StringBuilder();
              else
                sb.AppendLine();
              sb.Append(xpath.Current.InnerXml);
            }
            Debug.Print($"[{DateTime.UtcNow}] => RESPONSE (XML): \"{sb}\"");
            return sb?.ToString();
          }
          if (response.ContentType.StartsWith("application/json")) {
            var encname = response.CharacterSet;
            if (string.IsNullOrWhiteSpace(encname))
              encname = "utf-8";
            var enc = Encoding.GetEncoding(encname);
            using (var sr = new StreamReader(stream, enc)) {
              var moe = JsonConvert.DeserializeObject<MessageOrError>(sr.ReadToEnd());
              Debug.Print($"[{DateTime.UtcNow}] => RESPONSE (JSON): \"{moe?.message}\"");
              return moe?.message;
            }
          }
        }
      }
      catch { /* keep calm and fall through */ }
      return null;
    }

    #endregion

    private static readonly ReaderWriterLockSlim RequestLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

    private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings {
      CheckAdditionalContent = true,
      MissingMemberHandling  = MissingMemberHandling.Error
    };

    private static DateTime _lastRequestTime;

    private static double _requestDelay = 1.0;

    private NetworkCredential _credential;

    private readonly string _fullUserAgent;

    private string _lastDigest;

    [SuppressMessage("ReSharper", "CyclomaticComplexity")]
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
        sb.Append((sb.Length == 0) ? '?' : '&').Append("type");
        var letter = '=';
        // Primary Types
        if ((type & ReleaseType.Album)       != 0) { sb.Append(letter).Append("album");          letter = '|'; }
        if ((type & ReleaseType.Broadcast)   != 0) { sb.Append(letter).Append("broadcast");      letter = '|'; }
        if ((type & ReleaseType.EP)          != 0) { sb.Append(letter).Append("ep");             letter = '|'; }
        if ((type & ReleaseType.Other)       != 0) { sb.Append(letter).Append("other");          letter = '|'; }
        if ((type & ReleaseType.Single)      != 0) { sb.Append(letter).Append("single");         letter = '|'; }
        // Secondary Types
        if ((type & ReleaseType.Audiobook)   != 0) { sb.Append(letter).Append("audiobook");      letter = '|'; }
        if ((type & ReleaseType.Compilation) != 0) { sb.Append(letter).Append("compilation");    letter = '|'; }
        if ((type & ReleaseType.DJMix)       != 0) { sb.Append(letter).Append("dj-mix");         letter = '|'; }
        if ((type & ReleaseType.Interview)   != 0) { sb.Append(letter).Append("interview");      letter = '|'; }
        if ((type & ReleaseType.Live)        != 0) { sb.Append(letter).Append("live");           letter = '|'; }
        if ((type & ReleaseType.MixTape)     != 0) { sb.Append(letter).Append("mixtape/street"); letter = '|'; }
        if ((type & ReleaseType.Remix)       != 0) { sb.Append(letter).Append("remix");          letter = '|'; }
        if ((type & ReleaseType.Soundtrack)  != 0) { sb.Append(letter).Append("soundtrack");     letter = '|'; }
        if ((type & ReleaseType.SpokenWord)  != 0) { sb.Append(letter).Append("spokenword");     letter = '|'; }
      }
      if (status.HasValue) {
        sb.Append((sb.Length == 0) ? '?' : '&').Append("status");
        var letter = '=';
        if ((status & ReleaseStatus.Bootleg)       != 0) { sb.Append(letter).Append("bootleg");        letter = '|'; }
        if ((status & ReleaseStatus.Official)      != 0) { sb.Append(letter).Append("official");       letter = '|'; }
        if ((status & ReleaseStatus.Promotion)     != 0) { sb.Append(letter).Append("promotion");      letter = '|'; }
        if ((status & ReleaseStatus.PseudoRelease) != 0) { sb.Append(letter).Append("pseudo-release"); letter = '|'; }
      }
      return sb.ToString();
    }

    private string PerformDirectRequest(string entity, string id, string extra) {
      var uri = new UriBuilder(this.UrlScheme, this.WebSite, this.Port, $"{Query.WebServiceRoot}/{entity}/{id}", extra).Uri;
      Debug.Print($"[{DateTime.UtcNow}] WEB SERVICE REQUEST: GET {uri}");
      var firstTry = true;
    retry:
      var req = WebRequest.Create(uri) as HttpWebRequest;
      if (req == null)
        throw new InvalidOperationException("Only HTTP-compatible URL schemes are supported.");
      req.Method    = "GET";
      req.Accept    = "application/json";
      req.UserAgent = this._fullUserAgent;
      if (this.BearerToken != null)
        req.Headers.Add("Authorization", $"Bearer {this.BearerToken}");
      else if (this._lastDigest != null)
        req.Headers.Add("Authorization", this._lastDigest);
      try {
        using (var response = (HttpWebResponse) req.GetResponse()) {
          using (var stream = response.GetResponseStream()) {
            if (stream != null) {
              var encname = response.CharacterSet;
              if (string.IsNullOrWhiteSpace(encname))
                encname = "utf-8";
              var enc = Encoding.GetEncoding(encname);
              using (var sr = new StreamReader(stream, enc)) {
                var json = sr.ReadToEnd();
                //Console.WriteLine($"<<{JsonConvert.SerializeObject(JsonConvert.DeserializeObject(json), Formatting.Indented)}>>");
                return json;
              }
            }
          }
        }
      }
      catch (WebException we) when (we.Response is HttpWebResponse) {
        using (var response = (HttpWebResponse) we.Response) {
          if (firstTry && response.StatusCode == HttpStatusCode.Unauthorized) {
            firstTry = false; // only retry authentication once
            var digest = HttpDigestHelper.GetDigest(response, this.Credential);
            if (digest != null && this._lastDigest != digest) {
              this._lastDigest = digest;
              goto retry;
            }
          }
          var msg = Query.ExtractError(response);
          if (msg != null)
            throw new QueryException(msg, we);
        }
        throw;
      }
      // We got a response without any content (probably impossible).
      throw new QueryException("Query did not produce results.");
    }

    private string PerformDirectSubmission(ISubmission submission) {
      var uri = new UriBuilder(this.UrlScheme, this.WebSite, this.Port, $"{Query.WebServiceRoot}/{submission.Entity}/", $"?client={submission.Client}").Uri;
      Debug.Print($"[{DateTime.UtcNow}] WEB SERVICE SUBMISSION: {submission.Method} {uri}");
      var firstTry = true;
    retry:
      var req = WebRequest.Create(uri) as HttpWebRequest;
      if (req == null)
        throw new InvalidOperationException("Only HTTP-compatible URL schemes are supported.");
      req.Method      = submission.Method;
#if SUBMIT_ACCEPT_JSON
      req.Accept      = "application/json";
#else
      req.Accept      = "application/xml";
#endif
      req.ContentType = "application/xml; charset=utf-8";
      if (this.BearerToken != null)
        req.Headers.Add("Authorization", $"Bearer {this.BearerToken}");
      else if (this._lastDigest != null)
        req.Headers.Add("Authorization", this._lastDigest);
      var body = submission.RequestBody;
      if (body != null) {
        Debug.Print($"[{DateTime.UtcNow}] => BODY: {body}");
        using (var rs = req.GetRequestStream()) {
          using (var sw = new StreamWriter(rs, Encoding.UTF8))
            sw.Write(body);
        }
      }
      try {
        using (var response = (HttpWebResponse) req.GetResponse())
          return Query.ExtractMessage(response);
      }
      catch (WebException we) when (we.Response is HttpWebResponse) {
        using (var response = (HttpWebResponse) we.Response) {
          if (firstTry && response.StatusCode == HttpStatusCode.Unauthorized) {
            firstTry = false; // only retry authentication once
            var digest = HttpDigestHelper.GetDigest(response, this.Credential);
            if (digest != null && this._lastDigest != digest) {
              this._lastDigest = digest;
              goto retry;
            }
          }
          var msg = Query.ExtractError(response);
          if (msg != null)
            throw new QueryException(msg, we);
        }
        throw;
      }
    }

    private string PerformRequest(string entity, string id, string extra) {
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

    internal string PerformSubmission(ISubmission submission) {
      if (Query._requestDelay <= 0.0)
        return this.PerformDirectSubmission(submission);
      while (true) {
        Query.RequestLock.EnterWriteLock();
        try {
          if ((DateTime.UtcNow - Query._lastRequestTime).TotalSeconds >= Query._requestDelay) {
            try {
              return this.PerformDirectSubmission(submission);
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
