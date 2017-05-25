using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml.XPath;

using MetaBrainz.MusicBrainz.Interfaces.Submissions;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  public sealed partial class Query {

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

    #region Delay Processing

    private static DateTime _lastRequestTime;

    private static double _requestDelay = 1.0;

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

    #region Compatibility Helpers

    #if NETFX_EQ_2_0 // Provide Func<T>

    /// <summary>A function taking no arguments.</summary>
    /// <typeparam name="TResult">The type for the function's result.</typeparam>
    /// <returns>The result of the function.</returns>
    private delegate TResult Func<out TResult>();

    #endif

    #if NETFX_GE_3_5 // Use ReaderWriterLockSlim

    private static readonly ReaderWriterLockSlim RequestLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

    private static void Lock() {
      Query.RequestLock.EnterWriteLock();
    }

    private static void Unlock() {
      Query.RequestLock.ExitWriteLock();
    }

    #else // Use ReaderWriterLock

    private static readonly ReaderWriterLock RequestLock = new ReaderWriterLock();

    private static void Lock()
    {
      Query.RequestLock.AcquireWriterLock(-1);
    }

    private static void Unlock()
    {
      Query.RequestLock.ReleaseWriterLock();
    }

    #endif

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

  }

}
