// This will not work until https://github.com/metabrainz/musicbrainz-server/pull/385 is merged.
//#define SUBMIT_ACCEPT_JSON
// This will cause the raw JSON response for lookups/browsed to be traced (debug builds only).
//#define TRACE_JSON_RESPONSE

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.XPath;

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
            if (encname == null || encname.Trim().Length == 0)
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
            if (encname == null || encname.Trim().Length == 0)
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

    private string ApplyDelay(Func<string> request) {
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

    #endregion

    #region Query String Processing

    [SuppressMessage("ReSharper", "CyclomaticComplexity")]
    private static string BuildExtraText(Include inc, int[] toc = null, bool allMedia = false, bool noStubs = false, Uri resource = null, ReleaseType? type = null, ReleaseStatus? status = null) {
      var sb = new StringBuilder();
      if (toc != null) {
        sb.Append((sb.Length == 0) ? '?' : '&').Append("toc=");
        for (var i = 0; i < toc.Length; ++i) {
          if (i > 0) sb.Append('+');
          sb.Append(toc[i]);
        }
      }
      if (resource != null)
        sb.Append((sb.Length == 0) ? '?' : '&').Append("resource=").Append(Uri.EscapeDataString(resource.ToString()));
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

    #endregion

    private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings {
      CheckAdditionalContent = true,
      MissingMemberHandling  = MissingMemberHandling.Error
    };

    private readonly string _fullUserAgent;

    private string _lastDigest;

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
              if (encname == null || encname.Trim().Length == 0)
                encname = "utf-8";
              var enc = Encoding.GetEncoding(encname);
              using (var sr = new StreamReader(stream, enc)) {
                var json = sr.ReadToEnd();
#if TRACE_JSON_RESPONSE
                Debug.Print($"[{DateTime.UtcNow}] => RESPONSE: <<\n{JsonConvert.DeserializeObject(json)}\n>>");
#endif
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
            var digest = HttpDigestHelper.GetDigest(response, null);
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
            var digest = HttpDigestHelper.GetDigest(response, null);
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

    private string PerformRequest(string entity, string id, string extra) => this.ApplyDelay(() => this.PerformDirectRequest(entity, id, extra));

    internal string PerformSubmission(ISubmission submission) => this.ApplyDelay(() => this.PerformDirectSubmission(submission));

    #endregion

  }

}
