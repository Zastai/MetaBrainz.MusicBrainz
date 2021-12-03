using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.XPath;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Submissions;
using MetaBrainz.MusicBrainz.Json;
using MetaBrainz.MusicBrainz.Objects;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query : IDisposable {

  #region JSON Options

  private static readonly JsonSerializerOptions JsonReaderOptions = JsonUtils.CreateReaderOptions(Converters.Readers);

  internal static T Deserialize<T>(string json) {
    var obj = JsonUtils.Deserialize<T>(json, Query.JsonReaderOptions);
    return obj ?? throw new JsonException("A null object was received.");
  }

  #endregion

  #region Message / Error Handling

  private static void MaybeMapException(WebException we) {
    var response = we.Response;
    if (response is null || response.ContentLength == 0) {
      return;
    }
    try {
      using var stream = response.GetResponseStream();
#if !NET
      if (stream == null) {
        return;
      }
#endif
      if (response.ContentType.StartsWith("application/xml")) {
        Debug.Print($"[{DateTime.UtcNow}] => RESPONSE ({response.ContentType}): <{response.ContentLength} byte(s)>");
        StringBuilder? sb = null;
        var xpath = new XPathDocument(stream).CreateNavigator().Select("/error/text");
        while (xpath.MoveNext()) {
          if (sb is null) {
            sb = new StringBuilder();
          }
          else {
            sb.AppendLine();
          }
          sb.Append(xpath.Current?.InnerXml);
        }
        if (sb is null) {
          Debug.Print($"[{DateTime.UtcNow}] => NO ERROR TEXT FOUND");
          return;
        }
        Debug.Print($"[{DateTime.UtcNow}] => ERROR: \"{sb}\"");
        throw new QueryException(sb.ToString(), we);
      }
      if (response.ContentType.StartsWith("application/json")) {
        var characterSet = "utf-8";
        if (response is HttpWebResponse httpResponse) {
          characterSet = httpResponse.CharacterSet;
          if (string.IsNullOrWhiteSpace(characterSet)) {
            characterSet = "utf-8";
          }
        }
        var enc = Encoding.GetEncoding(characterSet);
        using var sr = new StreamReader(stream, enc);
        var json = sr.ReadToEnd();
        Debug.Print($"[{DateTime.UtcNow}] => RESPONSE ({response.ContentType}): \"{JsonUtils.Prettify(json)}\"");
        var moe = Query.Deserialize<MessageOrError>(json);
        if (moe.Error is null) {
          Debug.Print($"[{DateTime.UtcNow}] => NO ERROR TEXT FOUND");
          return;
        }
        Debug.Print($"[{DateTime.UtcNow}] => ERROR: \"{moe.Error}\" ({moe.Help})");
        throw new QueryException(moe.Error, we) { HelpLink = moe.Help };
      }
      Debug.Print($"[{DateTime.UtcNow}] => UNHANDLED ERROR RESPONSE ({response.ContentType}): <{response.ContentLength} byte(s)>");
    }
    catch (QueryException) { throw; }
    catch (Exception e) {
      Debug.Print($"[{DateTime.UtcNow}] => FAILED TO PROCESS ERROR RESPONSE: [{e.GetType()}] {e.Message}");
      /* keep calm and fall through */
    }
  }

  private static async Task MaybeMapExceptionAsync(WebException we) {
    var response = we.Response;
    if (response is null || response.ContentLength == 0) {
      return;
    }
    try {
#if NET || NETSTANDARD2_1_OR_GREATER
      var stream = response.GetResponseStream();
      await using var _ = stream.ConfigureAwait(false);
#else
      using var stream = response.GetResponseStream();
#endif
#if !NET
      if (stream is null) {
        return;
      }
#endif
      if (response.ContentType.StartsWith("application/xml")) {
        Debug.Print($"[{DateTime.UtcNow}] => RESPONSE ({response.ContentType}): <{response.ContentLength} byte(s)>");
        StringBuilder? sb = null;
        var xpath = new XPathDocument(stream).CreateNavigator().Select("/error/text");
        while (xpath.MoveNext()) {
          if (sb is null) {
            sb = new StringBuilder();
          }
          else {
            sb.AppendLine();
          }
          sb.Append(xpath.Current?.InnerXml);
        }
        if (sb is null) {
          Debug.Print($"[{DateTime.UtcNow}] => NO ERROR TEXT FOUND");
          return;
        }
        // FIXME: Does this return a help string too?
        Debug.Print($"[{DateTime.UtcNow}] => ERROR: \"{sb}\"");
        throw new QueryException(sb.ToString(), we);
      }
      if (response.ContentType.StartsWith("application/json")) {
        var characterSet = "utf-8";
        if (response is HttpWebResponse httpResponse) {
          characterSet = httpResponse.CharacterSet;
          if (string.IsNullOrWhiteSpace(characterSet)) {
            characterSet = "utf-8";
          }
        }
        var enc = Encoding.GetEncoding(characterSet);
        using var sr = new StreamReader(stream, enc, false, 1024, true);
        var json = await sr.ReadToEndAsync().ConfigureAwait(false);
        Debug.Print($"[{DateTime.UtcNow}] => RESPONSE ({response.ContentType}): \"{JsonUtils.Prettify(json)}\"");
        var moe = Query.Deserialize<MessageOrError>(json);
        if (moe.Error is null) {
          Debug.Print($"[{DateTime.UtcNow}] => NO ERROR TEXT FOUND");
          return;
        }
        Debug.Print($"[{DateTime.UtcNow}] => ERROR: \"{moe.Error}\" ({moe.Help})");
        throw new QueryException(moe.Error, we) { HelpLink = moe.Help };
      }
      Debug.Print($"[{DateTime.UtcNow}] => UNHANDLED ERROR RESPONSE ({response.ContentType}): <{response.ContentLength} byte(s)>");
    }
    catch (QueryException) { throw; }
    catch (Exception e) {
      Debug.Print($"[{DateTime.UtcNow}] => FAILED TO PROCESS ERROR RESPONSE: [{e.GetType()}] {e.Message}");
      // keep calm and fall through
    }
  }

  private static string? ExtractMessage(string response) {
    try {
      Debug.Print($"[{DateTime.UtcNow}] => RESPONSE (application/json): \"{JsonUtils.Prettify(response)}\"");
      var moe = Query.Deserialize<MessageOrError>(response);
      Debug.Print($"[{DateTime.UtcNow}] => MESSAGE: \"{moe.Message}\"");
      return moe.Message;
    }
    catch {
      // keep calm and fall through
    }
    return null;
  }

  #endregion

  #region Delay Processing

  private static readonly SemaphoreSlim RequestLock = new(1);

  private static DateTime _lastRequestTime;

  private static T ApplyDelay<T>(Func<T> request) {
    if (Query.DelayBetweenRequests <= 0.0) {
      return request();
    }
    while (true) {
      Query.RequestLock.Wait();
      try {
        if ((DateTime.UtcNow - Query._lastRequestTime).TotalSeconds >= Query.DelayBetweenRequests) {
          Query._lastRequestTime = DateTime.UtcNow;
          return request();
        }
      }
      finally {
        Query.RequestLock.Release();
      }
      Thread.Sleep((int) (500 * Query.DelayBetweenRequests));
    }
  }

  private static async Task<T> ApplyDelayAsync<T>(Func<Task<T>> request) {
    if (Query.DelayBetweenRequests <= 0.0) {
      return await request().ConfigureAwait(false);
    }
    while (true) {
      await Query.RequestLock.WaitAsync();
      try {
        if ((DateTime.UtcNow - Query._lastRequestTime).TotalSeconds >= Query.DelayBetweenRequests) {
          Query._lastRequestTime = DateTime.UtcNow;
          return await request().ConfigureAwait(false);
        }
      }
      finally {
        Query.RequestLock.Release();
      }
      await Task.Delay((int) (500 * Query.DelayBetweenRequests)).ConfigureAwait(false);
    }
  }

  #endregion

  #region Query String Processing

  private static void AddIncludeText(StringBuilder sb, Include inc) {
    if (inc == Include.None) {
      return;
    }
    sb.Append((sb.Length == 0) ? '?' : '&').Append("inc");
    var letter = '=';
    // Linked Entities
    if ((inc & Include.Artists) != 0) {
      sb.Append(letter).Append("artists");
      letter = '+';
    }
    if ((inc & Include.Collections) != 0) {
      sb.Append(letter).Append("collections");
      letter = '+';
    }
    if ((inc & Include.Labels) != 0) {
      sb.Append(letter).Append("labels");
      letter = '+';
    }
    if ((inc & Include.Recordings) != 0) {
      sb.Append(letter).Append("recordings");
      letter = '+';
    }
    if ((inc & Include.ReleaseGroups) != 0) {
      sb.Append(letter).Append("release-groups");
      letter = '+';
    }
    if ((inc & Include.Releases) != 0) {
      sb.Append(letter).Append("releases");
      letter = '+';
    }
    if ((inc & Include.Works) != 0) {
      sb.Append(letter).Append("works");
      letter = '+';
    }
    // Special Cases
    if ((inc & Include.ArtistCredits) != 0) {
      sb.Append(letter).Append("artist-credits");
      letter = '+';
    }
    if ((inc & Include.DiscIds) != 0) {
      sb.Append(letter).Append("discids");
      letter = '+';
    }
    if ((inc & Include.Isrcs) != 0) {
      sb.Append(letter).Append("isrcs");
      letter = '+';
    }
    if ((inc & Include.Media) != 0) {
      sb.Append(letter).Append("media");
      letter = '+';
    }
    if ((inc & Include.UserCollections) != 0) {
      sb.Append(letter).Append("user-collections");
      letter = '+';
    }
    if ((inc & Include.VariousArtists) != 0) {
      sb.Append(letter).Append("various-artists");
      letter = '+';
    }
    // Optional Info
    if ((inc & Include.Aliases) != 0) {
      sb.Append(letter).Append("aliases");
      letter = '+';
    }
    if ((inc & Include.Annotation) != 0) {
      sb.Append(letter).Append("annotation");
      letter = '+';
    }
    if ((inc & Include.Genres) != 0) {
      sb.Append(letter).Append("genres");
      letter = '+';
    }
    if ((inc & Include.Ratings) != 0) {
      sb.Append(letter).Append("ratings");
      letter = '+';
    }
    if ((inc & Include.Tags) != 0) {
      sb.Append(letter).Append("tags");
      letter = '+';
    }
    if ((inc & Include.UserGenres) != 0) {
      sb.Append(letter).Append("user-genres");
      letter = '+';
    }
    if ((inc & Include.UserRatings) != 0) {
      sb.Append(letter).Append("user-ratings");
      letter = '+';
    }
    if ((inc & Include.UserTags) != 0) {
      sb.Append(letter).Append("user-tags");
      letter = '+';
    }
    // Relationships
    if ((inc & Include.AreaRelationships) != 0) {
      sb.Append(letter).Append("area-rels");
      letter = '+';
    }
    if ((inc & Include.ArtistRelationships) != 0) {
      sb.Append(letter).Append("artist-rels");
      letter = '+';
    }
    if ((inc & Include.EventRelationships) != 0) {
      sb.Append(letter).Append("event-rels");
      letter = '+';
    }
    if ((inc & Include.InstrumentRelationships) != 0) {
      sb.Append(letter).Append("instrument-rels");
      letter = '+';
    }
    if ((inc & Include.LabelRelationships) != 0) {
      sb.Append(letter).Append("label-rels");
      letter = '+';
    }
    if ((inc & Include.PlaceRelationships) != 0) {
      sb.Append(letter).Append("place-rels");
      letter = '+';
    }
    if ((inc & Include.RecordingLevelRelationships) != 0) {
      sb.Append(letter).Append("recording-level-rels");
      letter = '+';
    }
    if ((inc & Include.RecordingRelationships) != 0) {
      sb.Append(letter).Append("recording-rels");
      letter = '+';
    }
    if ((inc & Include.ReleaseGroupRelationships) != 0) {
      sb.Append(letter).Append("release-group-rels");
      letter = '+';
    }
    if ((inc & Include.ReleaseRelationships) != 0) {
      sb.Append(letter).Append("release-rels");
      letter = '+';
    }
    if ((inc & Include.SeriesRelationships) != 0) {
      sb.Append(letter).Append("series-rels");
      letter = '+';
    }
    if ((inc & Include.UrlRelationships) != 0) {
      sb.Append(letter).Append("url-rels");
      letter = '+';
    }
    if ((inc & Include.WorkLevelRelationships) != 0) {
      sb.Append(letter).Append("work-level-rels");
      letter = '+';
    }
    if ((inc & Include.WorkRelationships) != 0) {
      sb.Append(letter).Append("work-rels");
    }
  }

  private static void AddReleaseFilter(StringBuilder sb, ReleaseType? type, ReleaseStatus? status) {
    if (type is not null) {
      sb.Append((sb.Length == 0) ? '?' : '&').Append("type");
      var letter = '=';
      // Primary Types
      if ((type.Value & ReleaseType.Album) != 0) {
        sb.Append(letter).Append("album");
        letter = '|';
      }
      if ((type.Value & ReleaseType.Broadcast) != 0) {
        sb.Append(letter).Append("broadcast");
        letter = '|';
      }
      if ((type.Value & ReleaseType.EP) != 0) {
        sb.Append(letter).Append("ep");
        letter = '|';
      }
      if ((type.Value & ReleaseType.Other) != 0) {
        sb.Append(letter).Append("other");
        letter = '|';
      }
      if ((type.Value & ReleaseType.Single) != 0) {
        sb.Append(letter).Append("single");
        letter = '|';
      }
      // Secondary Types
      if ((type.Value & ReleaseType.Audiobook) != 0) {
        sb.Append(letter).Append("audiobook");
        letter = '|';
      }
      if ((type.Value & ReleaseType.Compilation) != 0) {
        sb.Append(letter).Append("compilation");
        letter = '|';
      }
      if ((type.Value & ReleaseType.DJMix) != 0) {
        sb.Append(letter).Append("dj-mix");
        letter = '|';
      }
      if ((type.Value & ReleaseType.Interview) != 0) {
        sb.Append(letter).Append("interview");
        letter = '|';
      }
      if ((type.Value & ReleaseType.Live) != 0) {
        sb.Append(letter).Append("live");
        letter = '|';
      }
      if ((type.Value & ReleaseType.MixTape) != 0) {
        sb.Append(letter).Append("mixtape/street");
        letter = '|';
      }
      if ((type.Value & ReleaseType.Remix) != 0) {
        sb.Append(letter).Append("remix");
        letter = '|';
      }
      if ((type.Value & ReleaseType.Soundtrack) != 0) {
        sb.Append(letter).Append("soundtrack");
        letter = '|';
      }
      if ((type.Value & ReleaseType.SpokenWord) != 0) {
        sb.Append(letter).Append("spokenword");
      }
    }
    if (status is not null) {
      sb.Append((sb.Length == 0) ? '?' : '&').Append("status");
      var letter = '=';
      if ((status.Value & ReleaseStatus.Bootleg) != 0) {
        sb.Append(letter).Append("bootleg");
        letter = '|';
      }
      if ((status.Value & ReleaseStatus.Official) != 0) {
        sb.Append(letter).Append("official");
        letter = '|';
      }
      if ((status.Value & ReleaseStatus.Promotion) != 0) {
        sb.Append(letter).Append("promotion");
        letter = '|';
      }
      if ((status.Value & ReleaseStatus.PseudoRelease) != 0) {
        sb.Append(letter).Append("pseudo-release");
      }
    }
  }

  private static string BuildExtraText(Include inc) {
    var sb = new StringBuilder();
    Query.AddIncludeText(sb, inc);
    return sb.ToString();
  }

  private static string BuildExtraText(Include inc, Uri resource) {
    var sb = new StringBuilder();
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

  private static string BuildExtraText(Include inc, int[]? toc, bool allMediaFormats, bool noStubs) {
    var sb = new StringBuilder();
    if (toc is not null) {
      sb.Append((sb.Length == 0) ? '?' : '&').Append("toc=");
      for (var i = 0; i < toc.Length; ++i) {
        if (i > 0) {
          sb.Append('+');
        }
        sb.Append(toc[i]);
      }
    }
    if (allMediaFormats) {
      sb.Append((sb.Length == 0) ? '?' : '&').Append("media-format=all");
    }
    if (noStubs) {
      sb.Append((sb.Length == 0) ? '?' : '&').Append("cdstubs=no");
    }
    Query.AddIncludeText(sb, inc);
    return sb.ToString();
  }

  private static string BuildExtraText(Include inc, string query, ReleaseType? type = null, ReleaseStatus? status = null) {
    if (string.IsNullOrWhiteSpace(query)) {
      throw new ArgumentException("A browse or search query must not be blank.", nameof(query));
    }
    var sb = new StringBuilder();
    sb.Append('?').Append(query);
    Query.AddIncludeText(sb, inc);
    Query.AddReleaseFilter(sb, type, status);
    return sb.ToString();
  }

  #endregion

  #region Web Client / IDisposable

// Disable complaints about WebRequest until this is rewritten for HttpClient
// RIDER-71277: for Rider, we need to allow ALL obsolete symbols by disabling CS0618
#pragma warning disable CS0618
#pragma warning disable SYSLIB0014

  private readonly SemaphoreSlim _clientLock = new(1);

  private bool _disposed;

  private readonly string _fullUserAgent;

  private WebClient? _client;

  private WebClient WebClient {
    get {
      if (this._disposed) {
        throw new ObjectDisposedException(nameof(Query));
      }
      var wc = this._client ??= new WebClient { Encoding = Encoding.UTF8 };
      wc.BaseAddress = this.BaseUri.ToString();
      return wc;
    }
  }

  /// <summary>Closes the web client in use by this query, if there is one.</summary>
  /// <remarks>The next web service request will create a new client.</remarks>
  public void Close() {
    this._clientLock.Wait();
    try {
      this._client?.Dispose();
      this._client = null;
    }
    finally {
      this._clientLock.Release();
    }
  }

  /// <summary>Disposes the web client in use by this query, if there is one.</summary>
  /// <remarks>Further attempts at web service requests will cause <see cref="ObjectDisposedException"/> to be thrown.</remarks>
  public void Dispose() {
    this.Dispose(true);
    GC.SuppressFinalize(this);
  }

  private void Dispose(bool disposing) {
    if (!disposing) {
      return;
    }
    try {
      this.Close();
      this._clientLock.Dispose();
    }
    finally {
      this._disposed = true;
    }
  }

  /// <summary>Finalizes this query, releasing any and all resources.</summary>
  ~Query() {
    this.Dispose(false);
  }

  #endregion

  #region Basic Request Execution

  private string PerformRequest(string address, HttpMethod method, string accept, string? contentType, string? body = null) {
    Debug.Print($"[{DateTime.UtcNow}] WEB SERVICE REQUEST: {method.Method} {this.BaseUri}{address}");
    this._clientLock.Wait();
    try {
      var wc = this.WebClient;
      if (this.BearerToken is not null) {
        wc.Headers.Set("Authorization", $"Bearer {this.BearerToken}");
      }
      if (contentType is not null) {
        wc.Headers.Set("Content-Type", contentType);
      }
      wc.Headers.Set("Accept", accept);
      wc.Headers.Set("User-Agent", this._fullUserAgent);
      wc.QueryString.Clear();
      try {
        if (method == HttpMethod.Get) {
          return wc.DownloadString(address);
        }
        if (body is not null) {
          Debug.Print($"[{DateTime.UtcNow}] => BODY ({contentType}): {body}");
        }
        return wc.UploadString(address, method.ToString(), body ?? string.Empty);
      }
      catch (WebException we) {
        Query.MaybeMapException(we);
        throw;
      }
    }
    finally {
      this._clientLock.Release();
    }
  }

  internal string PerformRequest(string entity, Guid id, string extra) {
    var address = $"{entity}/{id:D}{extra}";
    var json = Query.ApplyDelay(() => this.PerformRequest(address, HttpMethod.Get, "application/json", null));
    Debug.Print($"[{DateTime.UtcNow}] => JSON: <<{JsonUtils.Prettify(json)}>>");
    return json;
  }

  internal string PerformRequest(string entity, string? id, string extra) {
    var address = $"{entity}/{id}{extra}";
    var json = Query.ApplyDelay(() => this.PerformRequest(address, HttpMethod.Get, "application/json", null));
    Debug.Print($"[{DateTime.UtcNow}] => JSON: <<{JsonUtils.Prettify(json)}>>");
    return json;
  }

  private async Task<string> PerformRequestAsync(string address, HttpMethod method, string accept, string? contentType,
                                                 string? body = null) {
    Debug.Print($"[{DateTime.UtcNow}] WEB SERVICE REQUEST: {method.Method} {this.BaseUri}{address}");
    await this._clientLock.WaitAsync();
    try {
      var wc = this.WebClient;
      if (this.BearerToken is not null) {
        wc.Headers.Set("Authorization", $"Bearer {this.BearerToken}");
      }
      if (contentType is not null) {
        wc.Headers.Set("Content-Type", contentType);
      }
      wc.Headers.Set("Accept", accept);
      wc.Headers.Set("User-Agent", this._fullUserAgent);
      wc.QueryString.Clear();
      try {
        if (method == HttpMethod.Get) {
          return await wc.DownloadStringTaskAsync(address).ConfigureAwait(false);
        }
        if (body is not null) {
          Debug.Print($"[{DateTime.UtcNow}] => BODY ({contentType}): {body}");
        }
        return await wc.UploadStringTaskAsync(address, method.ToString(), body ?? string.Empty).ConfigureAwait(false);
      }
      catch (WebException we) {
        await Query.MaybeMapExceptionAsync(we);
        throw;
      }
    }
    finally {
      this._clientLock.Release();
    }
  }

  internal async Task<string> PerformRequestAsync(string entity, Guid id, string extra) {
    var address = $"{entity}/{id:D}{extra}";
    var json = await Query.ApplyDelayAsync(() => this.PerformRequestAsync(address, HttpMethod.Get, "application/json", null));
    Debug.Print($"[{DateTime.UtcNow}] => JSON: <<{JsonUtils.Prettify(json)}>>");
    return json;
  }

  internal async Task<string> PerformRequestAsync(string entity, string? id, string extra) {
    var address = $"{entity}/{id}{extra}";
    var json = await Query.ApplyDelayAsync(() => this.PerformRequestAsync(address, HttpMethod.Get, "application/json", null));
    Debug.Print($"[{DateTime.UtcNow}] => JSON: <<{JsonUtils.Prettify(json)}>>");
    return json;
  }

  internal string PerformSubmission(ISubmission submission) {
    var address = $"{submission.Entity}/?client={submission.Client}";
    var method = submission.Method;
    var contentType = submission.ContentType;
    var body = submission.RequestBody;
    var msg = Query.ApplyDelay(() => this.PerformRequest(address, method, "application/json", contentType, body));
    return Query.ExtractMessage(msg) ?? "";
  }

  internal async Task<string> PerformSubmissionAsync(ISubmission submission) {
    var address = $"{submission.Entity}/?client={submission.Client}";
    var method = submission.Method;
    var contentType = submission.ContentType;
    var body = submission.RequestBody;
    var msg = await Query.ApplyDelayAsync(() => this.PerformRequestAsync(address, method, "application/json", contentType, body));
    return Query.ExtractMessage(msg) ?? "";
  }

  #endregion

}
