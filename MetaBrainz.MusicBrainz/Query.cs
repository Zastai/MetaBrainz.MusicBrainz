using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml.XPath;

#if NETFX_GE_4_5 // HttpWebRequest only has GetResponseAsync in v4.5 and up
using System.Threading.Tasks;
#endif

using MetaBrainz.MusicBrainz.Submissions;

using Newtonsoft.Json;

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
    /// <param name="application">The applciation name to use in the user agent property for all requests.</param>
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
    /// <param name="application">The applciation name to use in the user agent property for all requests.</param>
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

    #region Submissions

    /// <summary>Creates a submission request for setting a barcode on one or more releases.</summary>
    /// <param name="client">
    ///   The ID of the client software submitting data.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    ///   It will be included in the edit(s) registered by the MusicBrainz server for this submission.
    /// </param>
    /// <returns>A new barcode submission request.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    public BarcodeSubmission SubmitBarcodes(string client) {
      if (client == null) throw new ArgumentNullException(nameof(client));
      if (client.Trim().Length == 0) throw new ArgumentException("The client ID must not be blank.", nameof(client));
      return new BarcodeSubmission(this, client);
    }

    /// <summary>Creates a submission request for adding one or more ISRCs to one or more recordings.</summary>
    /// <param name="client">
    ///   The ID of the client software submitting data.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.<br/>
    ///   It will be included in the edit(s) registered by the MusicBrainz server for this submission.
    /// </param>
    /// <returns>A new ISRC submission request.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    public IsrcSubmission SubmitIsrcs(string client) {
      if (client == null) throw new ArgumentNullException(nameof(client));
      if (client.Trim().Length == 0) throw new ArgumentException("The client ID must not be blank.", nameof(client));
      return new IsrcSubmission(this, client);
    }

    /// <summary>Creates a submission request for rating one or more entities.</summary>
    /// <param name="client">
    ///   The ID of the client software submitting data.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.
    /// </param>
    /// <returns>A new rating submission request.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    public RatingSubmission SubmitRatings(string client) {
      if (client == null) throw new ArgumentNullException(nameof(client));
      if (client.Trim().Length == 0) throw new ArgumentException("The client ID must not be blank.", nameof(client));
      return new RatingSubmission(this, client);
    }

    /// <summary>Creates a submission request for modifying tags on one or more entities.</summary>
    /// <param name="client">
    ///   The ID of the client software submitting data.<br/>
    ///   This has to be the application's name and version number. The recommended format is &quot;<code>application-version</code>&quot;, where <code>version</code> does not contain a dash.
    /// </param>
    /// <returns>A new tag submission request.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="client"/> is null.</exception>
    /// <exception cref="ArgumentException">When <paramref name="client"/> is blank.</exception>
    public TagSubmission SubmitTags(string client) {
      if (client == null) throw new ArgumentNullException(nameof(client));
      if (client.Trim().Length == 0) throw new ArgumentException("The client ID must not be blank.", nameof(client));
      return new TagSubmission(this, client);
    }

    #endregion

    #region Internals

    private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings {
      CheckAdditionalContent = true,
      MissingMemberHandling  = MissingMemberHandling.Error
    };

    private const string JsonContentType = "application/json";

    private readonly string _fullUserAgent;

    private string _lastDigest;

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
            Debug.Print($"[{DateTime.UtcNow}] => ERROR ({response.ContentType}): \"{sb}\"");
            return sb?.ToString();
          }
          if (response.ContentType.StartsWith("application/json")) {
            var encname = response.CharacterSet;
            if (encname == null || encname.Trim().Length == 0)
              encname = "utf-8";
            var enc = Encoding.GetEncoding(encname);
            using (var sr = new StreamReader(stream, enc)) {
              var moe = JsonConvert.DeserializeObject<MessageOrError>(sr.ReadToEnd());
              Debug.Print($"[{DateTime.UtcNow}] => ERROR ({response.ContentType}): \"{moe?.error}\"");
              return moe?.error;
            }
          }
          Debug.Print($"[{DateTime.UtcNow}] => UNHANDLED ERROR ({response.ContentType})");
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
          if (response.ContentType.StartsWith("application/json")) {
            var encname = response.CharacterSet;
            if (encname == null || encname.Trim().Length == 0)
              encname = "utf-8";
            var enc = Encoding.GetEncoding(encname);
            using (var sr = new StreamReader(stream, enc)) {
              var moe = JsonConvert.DeserializeObject<MessageOrError>(sr.ReadToEnd());
              Debug.Print($"[{DateTime.UtcNow}] => RESPONSE ({response.ContentType}): \"{moe?.message}\"");
              return moe?.message;
            }
          }
          Debug.Print($"[{DateTime.UtcNow}] => UNHANDLED RESPONSE ({response.ContentType})");
        }
      }
      catch { /* keep calm and fall through */ }
      return null;
    }

    #endregion

    #region Locking

    #if NETFX_LT_3_5

    private static readonly ReaderWriterLock RequestLock = new ReaderWriterLock();

    private static void Lock() {
      Query.RequestLock.AcquireWriterLock(-1);
    }

    private static void Unlock() {
      Query.RequestLock.ReleaseWriterLock();
    }

    #else

    private static readonly ReaderWriterLockSlim RequestLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

    private static void Lock() {
      Query.RequestLock.EnterWriteLock();
    }

    private static void Unlock() {
      Query.RequestLock.ExitWriteLock();
    }

    #endif

    #endregion

    #region Delay Processing

    private static DateTime _lastRequestTime;

    private static double _requestDelay = 1.0;

    #if NETFX_LT_3_5 // No Func<T>

    /// <summary>A function taking no arguments.</summary>
    /// <typeparam name="TResult">The type for the function's result.</typeparam>
    /// <returns>The result of the function.</returns>
    private delegate TResult Func<out TResult>();

    #endif

    private static HttpWebResponse ApplyDelay(Func<HttpWebResponse> request) {
      if (Query._requestDelay <= 0.0)
        return request();
      while (true) {
        Query.Lock();
        try {
          if ((DateTime.UtcNow - Query._lastRequestTime).TotalSeconds >= Query._requestDelay) {
            try {
              return request();
            }
            finally {
              Query._lastRequestTime = DateTime.UtcNow;
            }
          }
        }
        finally {
          Query.Unlock();
        }
        Thread.Sleep((int) (500 * Query._requestDelay));
      }
    }

    #if NETFX_GE_4_5

    private static async Task<HttpWebResponse> ApplyDelayAsync(Func<Task<HttpWebResponse>> request) {
      if (Query._requestDelay <= 0.0)
        return await request().ConfigureAwait(false);
      Task<HttpWebResponse> task = null;
      while (task == null) {
        Query.Lock();
        try {
          if ((DateTime.UtcNow - Query._lastRequestTime).TotalSeconds >= Query._requestDelay) {
            try {
              task = request();
            }
            finally {
              Query._lastRequestTime = DateTime.UtcNow;
            }
          }
        }
        finally {
          Query.Unlock();
        }
        await Task.Delay((int) (500 * Query._requestDelay)).ConfigureAwait(false);
      }
      return await task.ConfigureAwait(false);
    }

    #endif

    #endregion

    #region Query String Processing

    private static void AddIncludeText(StringBuilder sb, Include inc) {
      if (sb == null) throw new ArgumentNullException(nameof(sb));
      if (inc == Include.None)
        return;
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

    private static void AddReleaseFilter(StringBuilder sb, ReleaseType? type, ReleaseStatus? status) {
      if (sb == null) throw new ArgumentNullException(nameof(sb));
      if (type.HasValue) {
        sb.Append((sb.Length == 0) ? '?' : '&').Append("type=");
        var letter = '=';
        // Primary Types
        if ((type.Value & ReleaseType.Album)       != 0) { sb.Append(letter).Append("album");          letter = '|'; }
        if ((type.Value & ReleaseType.Broadcast)   != 0) { sb.Append(letter).Append("broadcast");      letter = '|'; }
        if ((type.Value & ReleaseType.EP)          != 0) { sb.Append(letter).Append("ep");             letter = '|'; }
        if ((type.Value & ReleaseType.Other)       != 0) { sb.Append(letter).Append("other");          letter = '|'; }
        if ((type.Value & ReleaseType.Single)      != 0) { sb.Append(letter).Append("single");         letter = '|'; }
        // Secondary Types
        if ((type.Value & ReleaseType.Audiobook)   != 0) { sb.Append(letter).Append("audiobook");      letter = '|'; }
        if ((type.Value & ReleaseType.Compilation) != 0) { sb.Append(letter).Append("compilation");    letter = '|'; }
        if ((type.Value & ReleaseType.DJMix)       != 0) { sb.Append(letter).Append("dj-mix");         letter = '|'; }
        if ((type.Value & ReleaseType.Interview)   != 0) { sb.Append(letter).Append("interview");      letter = '|'; }
        if ((type.Value & ReleaseType.Live)        != 0) { sb.Append(letter).Append("live");           letter = '|'; }
        if ((type.Value & ReleaseType.MixTape)     != 0) { sb.Append(letter).Append("mixtape/street"); letter = '|'; }
        if ((type.Value & ReleaseType.Remix)       != 0) { sb.Append(letter).Append("remix");          letter = '|'; }
        if ((type.Value & ReleaseType.Soundtrack)  != 0) { sb.Append(letter).Append("soundtrack");     letter = '|'; }
        if ((type.Value & ReleaseType.SpokenWord)  != 0) { sb.Append(letter).Append("spokenword");     letter = '|'; }
      }
      if (status.HasValue) {
        sb.Append((sb.Length == 0) ? '?' : '&').Append("status=");
        var letter = '=';
        if ((status.Value & ReleaseStatus.Bootleg)       != 0) { sb.Append(letter).Append("bootleg");        letter = '|'; }
        if ((status.Value & ReleaseStatus.Official)      != 0) { sb.Append(letter).Append("official");       letter = '|'; }
        if ((status.Value & ReleaseStatus.Promotion)     != 0) { sb.Append(letter).Append("promotion");      letter = '|'; }
        if ((status.Value & ReleaseStatus.PseudoRelease) != 0) { sb.Append(letter).Append("pseudo-release"); letter = '|'; }
      }
    }

    private static string BuildExtraText(Include inc) {
      var sb = new StringBuilder();
      Query.AddIncludeText(sb, inc);
      return sb.ToString();
    }

    private static string BuildExtraText(Include inc, Uri resource) {
      var sb = new StringBuilder();
      if (resource != null)
        sb.Append("?resource=").Append(Uri.EscapeDataString(resource.ToString()));
      Query.AddIncludeText(sb, inc);
      return sb.ToString();
    }

    private static string BuildExtraText(Include inc, ReleaseStatus? status, ReleaseType? type = null) {
      var sb = new StringBuilder();
      Query.AddIncludeText(sb, inc);
      Query.AddReleaseFilter(sb, type, status);
      return sb.ToString();
    }

    private static string BuildExtraText(Include inc, int[] toc, bool allMediaFormats, bool noStubs) {
      var sb = new StringBuilder();
      if (toc != null) {
        sb.Append((sb.Length == 0) ? '?' : '&').Append("toc=");
        for (var i = 0; i < toc.Length; ++i) {
          if (i > 0) sb.Append('+');
          sb.Append(toc[i]);
        }
      }
      if (allMediaFormats) sb.Append((sb.Length == 0) ? '?' : '&').Append("media-format=all");
      if (noStubs)         sb.Append((sb.Length == 0) ? '?' : '&').Append("cdstubs=no");
      Query.AddIncludeText(sb, inc);
      return sb.ToString();
    }

    private static string BuildExtraText(Include inc, string query, ReleaseType? type = null, ReleaseStatus? status = null) {
      if (query == null) throw new ArgumentNullException(nameof(query));
      if (query.Trim().Length == 0) throw new ArgumentException("A browse or search query must not be blank.", nameof(query));
      var sb = new StringBuilder();
      sb.Append('?').Append(query);
      Query.AddIncludeText(sb, inc);
      Query.AddReleaseFilter(sb, type, status);
      return sb.ToString();
    }

    #endregion

    #region Synchronous Requests

    private HttpWebResponse PerformRequest(Uri uri, string method, string accept, string contentType = null, string body = null) {
      Debug.Print($"[{DateTime.UtcNow}] WEB SERVICE REQUEST: {method} {uri}");
      var firstTry = true;
    retry:
      var req = WebRequest.Create(uri) as HttpWebRequest;
      if (req == null)
        throw new InvalidOperationException("Only HTTP-compatible URL schemes are supported.");
      req.Method      = method;
      req.Accept      = accept;
      req.UserAgent   = this._fullUserAgent;
      if (contentType != null)
        req.ContentType = contentType;
      if (body != null) {
        Debug.Print($"[{DateTime.UtcNow}] => BODY ({contentType}): {body}");
        using (var rs = req.GetRequestStream()) {
          using (var sw = new StreamWriter(rs, Encoding.UTF8))
            sw.Write(body);
        }
      }
      if (this.BearerToken != null)
        req.Headers.Add("Authorization", $"Bearer {this.BearerToken}");
      else if (this._lastDigest != null)
        req.Headers.Add("Authorization", this._lastDigest);
      try {
        return (HttpWebResponse) req.GetResponse();
      }
      catch (WebException we) when (we.Response is HttpWebResponse) {
        var response = (HttpWebResponse) we.Response;
        if (firstTry && response.StatusCode == HttpStatusCode.Unauthorized) {
          firstTry = false; // only retry authentication once
          var digest = HttpDigestHelper.GetDigest(response, null);
          if (digest != null && this._lastDigest != digest) {
            // Before .NET 4.5, (Http)WebResponse used an explicit implementation of IDisposable, requiring this cast.
            ((IDisposable) response).Dispose();
            this._lastDigest = digest;
            goto retry;
          }
        }
        var msg = Query.ExtractError(response);
        if (msg != null)
          throw new QueryException(msg, we);
        throw;
      }
    }

    internal string PerformRequest(string entity, string id, string extra) {
      var uri = new UriBuilder(this.UrlScheme, this.WebSite, this.Port, $"{Query.WebServiceRoot}/{entity}/{id}", extra).Uri;
      using (var response = Query.ApplyDelay(() => this.PerformRequest(uri, "GET", Query.JsonContentType))) {
        if (!response.ContentType.StartsWith(Query.JsonContentType)) // FIXME: Should validate a little more than that, really
          throw new QueryException($"Invalid response received: bad content type ({response.ContentType}).");
        using (var stream = response.GetResponseStream()) {
          if (stream == null)
            return string.Empty;
          var encname = response.CharacterSet;
          if (encname == null || encname.Trim().Length == 0)
            encname = "utf-8";
          var enc = Encoding.GetEncoding(encname);
          using (var sr = new StreamReader(stream, enc)) {
            var json = sr.ReadToEnd();
            Debug.Print($"[{DateTime.UtcNow}] => RESPONSE ({response.ContentType}): <<\n{JsonConvert.DeserializeObject(json)}\n>>");
            return json;
          }
        }
      }
    }

    internal string PerformSubmission(ISubmission submission) {
      var uri = new UriBuilder(this.UrlScheme, this.WebSite, this.Port, $"{Query.WebServiceRoot}/{submission.Entity}/", $"?client={submission.Client}").Uri;
      using (var response = Query.ApplyDelay(() => this.PerformRequest(uri, submission.Method, Query.JsonContentType, submission.ContentType, submission.RequestBody)))
        return Query.ExtractMessage(response);
    }

    #endregion

    #region Asynchronous Requests

    #if NETFX_GE_4_5

    private async Task<HttpWebResponse> PerformRequestAsync(Uri uri, string method, string accept, string contentType = null, string body = null) {
      Debug.Print($"[{DateTime.UtcNow}] WEB SERVICE REQUEST: {method} {uri}");
      var firstTry = true;
    retry:
      var req = WebRequest.Create(uri) as HttpWebRequest;
      if (req == null)
        throw new InvalidOperationException("Only HTTP-compatible URL schemes are supported.");
      req.Method      = method;
      req.Accept      = accept;
      req.UserAgent   = this._fullUserAgent;
      if (contentType != null)
        req.ContentType = contentType;
      if (body != null) {
        Debug.Print($"[{DateTime.UtcNow}] => BODY ({contentType}): {body}");
        using (var rs = await req.GetRequestStreamAsync().ConfigureAwait(false)) {
          using (var sw = new StreamWriter(rs, Encoding.UTF8))
            sw.Write(body);
        }
      }
      if (this.BearerToken != null)
        req.Headers.Add("Authorization", $"Bearer {this.BearerToken}");
      else if (this._lastDigest != null)
        req.Headers.Add("Authorization", this._lastDigest);
      try {
        return (HttpWebResponse) await req.GetResponseAsync().ConfigureAwait(false);
      }
      catch (WebException we) when (we.Response is HttpWebResponse) {
        var response = (HttpWebResponse) we.Response;
        if (firstTry && response.StatusCode == HttpStatusCode.Unauthorized) {
          firstTry = false; // only retry authentication once
          var digest = HttpDigestHelper.GetDigest(response, null);
          if (digest != null && this._lastDigest != digest) {
            // Before .NET 4.5, (Http)WebResponse used an explicit implementation of IDisposable, requiring this cast.
            ((IDisposable) response).Dispose();
            this._lastDigest = digest;
            goto retry;
          }
        }
        var msg = Query.ExtractError(response);
        if (msg != null)
          throw new QueryException(msg, we);
        throw;
      }
    }

    internal async Task<string> PerformRequestAsync(string entity, string id, string extra) {
      var uri = new UriBuilder(this.UrlScheme, this.WebSite, this.Port, $"{Query.WebServiceRoot}/{entity}/{id}", extra).Uri;
      var task = Query.ApplyDelayAsync(() => this.PerformRequestAsync(uri, "GET", Query.JsonContentType));
      using (var response = await task.ConfigureAwait(false)) {
        if (!response.ContentType.StartsWith(Query.JsonContentType)) // FIXME: Should validate a little more than that, really
          throw new QueryException($"Invalid response received: bad content type ({response.ContentType}).");
        using (var stream = response.GetResponseStream()) {
          if (stream == null)
            return string.Empty;
          var encname = response.CharacterSet;
          if (encname == null || encname.Trim().Length == 0)
            encname = "utf-8";
          var enc = Encoding.GetEncoding(encname);
          using (var sr = new StreamReader(stream, enc)) {
            var json = sr.ReadToEnd();
            Debug.Print($"[{DateTime.UtcNow}] => RESPONSE ({response.ContentType}): <<\n{JsonConvert.DeserializeObject(json)}\n>>");
            return json;
          }
        }
      }
    }

    internal async Task<string> PerformSubmissionAsync(ISubmission submission) {
      var uri = new UriBuilder(this.UrlScheme, this.WebSite, this.Port, $"{Query.WebServiceRoot}/{submission.Entity}/", $"?client={submission.Client}").Uri;
      var task = Query.ApplyDelayAsync(() => this.PerformRequestAsync(uri, submission.Method, Query.JsonContentType, submission.ContentType, submission.RequestBody));
      using (var response = await task.ConfigureAwait(false))
        return Query.ExtractMessage(response);
    }

    #endif

    #endregion

    #endregion

  }

}
