using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

using MetaBrainz.Common;
using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Submissions;
using MetaBrainz.MusicBrainz.Json;
using MetaBrainz.MusicBrainz.Objects;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query : IDisposable {

  internal static readonly JsonSerializerOptions JsonReaderOptions = JsonUtils.CreateReaderOptions(Converters.Readers);

  #region Delay Processing

  private static readonly SemaphoreSlim DelayLock = new(1);

  private static DateTime _lastRequestTime;

  private static async Task<T> ApplyDelayAsync<T>([InstantHandle] Func<Task<T>> request, CancellationToken cancellationToken) {
    if (Query.DelayBetweenRequests <= 0.0) {
      return await request().ConfigureAwait(false);
    }
    while (true) {
      await Query.DelayLock.WaitAsync(cancellationToken).ConfigureAwait(false);
      var ready = false;
      try {
        var now = DateTime.UtcNow;
        if ((now - Query._lastRequestTime).TotalSeconds >= Query.DelayBetweenRequests) {
          Query._lastRequestTime = now;
          ready = true;
        }
      }
      finally {
        Query.DelayLock.Release();
      }
      if (ready) {
        return await request().ConfigureAwait(false);
      }
      await Task.Delay((int) (500 * Query.DelayBetweenRequests), cancellationToken).ConfigureAwait(false);
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

  private static string BuildExtraText(Include inc, string field, Guid id, ReleaseType? type = null, ReleaseStatus? status = null) {
    var sb = new StringBuilder();
    sb.Append('?').Append(field).Append('=').Append(id.ToString("D"));
    Query.AddIncludeText(sb, inc);
    Query.AddReleaseFilter(sb, type, status);
    return sb.ToString();
  }

  private static string BuildExtraText(string field, Guid id) => Query.BuildExtraText(field, id.ToString("D"));

  private static string BuildExtraText(string field, string value) {
    var sb = new StringBuilder();
    sb.Append('?').Append(field).Append('=').Append(value);
    return sb.ToString();
  }

  #endregion

  #region HttpClient / IDisposable

  private static readonly MediaTypeWithQualityHeaderValue AcceptHeader = new("application/json");

  private static readonly ProductInfoHeaderValue LibraryComment = new($"({Query.UserAgentUrl})");

  private static readonly ProductInfoHeaderValue LibraryProductInfo = HttpUtils.CreateUserAgentHeader<Query>();

  private HttpClient? _client;

  private Action<HttpClient>? _clientConfiguration;

  private Func<HttpClient>? _clientCreation;

  private readonly bool _clientOwned;

  private bool _disposed;

  private readonly List<ProductInfoHeaderValue> _userAgent = new(Query.DefaultUserAgent);

  private HttpClient Client {
    get {
#if NET6_0
      if (this._disposed) {
        throw new ObjectDisposedException(nameof(Query));
      }
#else
      ObjectDisposedException.ThrowIf(this._disposed, typeof(Query));
#endif
      if (this._client is null) {
        var client = this._clientCreation is not null ? this._clientCreation() : new HttpClient();
        this._userAgent.ForEach(client.DefaultRequestHeaders.UserAgent.Add);
        this._clientConfiguration?.Invoke(client);
        this._client = client;
      }
      return this._client;
    }
  }

  /// <summary>
  /// Closes the underlying web service client in use by this MusicBrainz query client, if there is one.</summary>
  /// <remarks>The next web service request will create a new client.</remarks>
  /// <exception cref="InvalidOperationException">When this instance is using an explicitly provided client instance.</exception>
  public void Close() {
    if (!this._clientOwned) {
      throw new InvalidOperationException("An explicitly provided client instance is in use.");
    }
    Interlocked.Exchange(ref this._client, null)?.Dispose();
  }

  /// <summary>Sets up code to run to configure a newly-created HTTP client.</summary>
  /// <param name="code">The configuration code for an HTTP client, or <see langword="null"/> to clear such code.</param>
  /// <remarks>The configuration code will be called <em>after</em> <see cref="UserAgent"/> is applied.</remarks>
  public void ConfigureClient(Action<HttpClient>? code) {
    this._clientConfiguration = code;
  }

  /// <summary>Sets up code to run to create an HTTP client.</summary>
  /// <param name="code">The creation code for an HTTP client, or <see langword="null"/> to clear such code.</param>
  /// <remarks>
  /// <see cref="UserAgent"/> and any code set via <see cref="ConfigureClient(System.Action{System.Net.Http.HttpClient}?)"/> will be
  /// applied to the client returned by <paramref name="code"/>.
  /// </remarks>
  public void ConfigureClientCreation(Func<HttpClient>? code) {
    this._clientCreation = code;
  }

  /// <summary>Discards any and all resources held by this MusicBrainz query client.</summary>
  /// <remarks>Further attempts at web service requests will cause <see cref="ObjectDisposedException"/> to be thrown.</remarks>
  public void Dispose() {
    this.Dispose(true);
    GC.SuppressFinalize(this);
  }

  private void Dispose(bool disposing) {
    if (!disposing) {
      // no unmanaged resources
      return;
    }
    try {
      if (this._clientOwned) {
        this.Close();
      }
      this._client = null;
    }
    finally {
      this._disposed = true;
    }
  }

  /// <summary>Finalizes this instance, releasing any and all resources.</summary>
  ~Query() {
    this.Dispose(false);
  }

  #endregion

  #region Basic Request Execution

  private Uri BuildUri(string path, string? extra = null)
    => new UriBuilder(this.UrlScheme, this.Server, this.Port, Query.WebServiceRoot + path, extra).Uri;

  private static async Task<string?> ExtractMessageAsync(HttpResponseMessage response, CancellationToken cancellationToken) {
    string? message = null;
    try {
      var ts = Query.TraceSource;
      var contents = await response.GetStringContentAsync(cancellationToken).ConfigureAwait(false);
      if (!string.IsNullOrWhiteSpace(contents)) {
        try {
          var mr = JsonSerializer.Deserialize<MessageResult>(contents, Query.JsonReaderOptions);
          if (mr is null) {
            throw new JsonException("Message response had null content.");
          }
          message = mr.Message;
          if (mr.UnhandledProperties is not null) {
            foreach (var prop in mr.UnhandledProperties) {
              ts.TraceEvent(TraceEventType.Verbose, 100, "UNEXPECTED MESSAGE PROPERTY: {0} -> {1}", prop.Key, prop.Value);
            }
          }
        }
        catch (Exception e) {
          ts.TraceEvent(TraceEventType.Verbose, 101, "FAILED TO PARSE MESSAGE RESPONSE CONTENT AS JSON: {0}", e.Message);
          message = null;
        }
        if (message is not null) {
          ts.TraceEvent(TraceEventType.Verbose, 102, "MESSAGE: '{0}'", message);
        }
        else {
          ts.TraceEvent(TraceEventType.Verbose, 103, "MESSAGE RESPONSE CONTENT: '{0}'", contents);
        }
      }
      else {
        ts.TraceEvent(TraceEventType.Verbose, 104, "NO MESSAGE RESPONSE CONTENT");
      }
    }
    catch {
      // keep calm and fall through
    }
    return message;
  }

  private async Task<HttpResponseMessage> PerformRequestAsync(Uri uri, HttpMethod method, HttpContent? body,
                                                              CancellationToken cancellationToken) {
    using var request = new HttpRequestMessage(method, uri);
    var ts = Query.TraceSource;
    ts.TraceEvent(TraceEventType.Verbose, 1, "WEB SERVICE REQUEST: {0} {1}", method.Method, request.RequestUri);
    var client = this.Client;
    {
      var headers = request.Headers;
      headers.Accept.Add(Query.AcceptHeader);
      if (this.BearerToken is not null) {
        headers.Authorization = new AuthenticationHeaderValue("Bearer", this.BearerToken);
      }
      // Use whatever user agent the client has set, plus our own.
      {
        var userAgent = headers.UserAgent;
        foreach (var ua in client.DefaultRequestHeaders.UserAgent) {
          userAgent.Add(ua);
        }
        userAgent.Add(Query.LibraryProductInfo);
        userAgent.Add(Query.LibraryComment);
      }
    }
    if (ts.Switch.ShouldTrace(TraceEventType.Verbose)) {
      ts.TraceEvent(TraceEventType.Verbose, 2, "HEADERS: {0}", TextUtils.FormatMultiLine(request.Headers.ToString()));
      if (body is not null) {
        var headers = body.Headers;
        ts.TraceEvent(TraceEventType.Verbose, 3, "BODY ({0}, {1} bytes): {2}", headers.ContentType, headers.ContentLength ?? 0,
                      TextUtils.FormatMultiLine(await body.ReadAsStringAsync(cancellationToken)));
      }
      else {
        ts.TraceEvent(TraceEventType.Verbose, 3, "NO BODY");
      }
    }
    request.Content = body;
    var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
    if (ts.Switch.ShouldTrace(TraceEventType.Verbose)) {
      ts.TraceEvent(TraceEventType.Verbose, 4, "WEB SERVICE RESPONSE: {0:D}/{0} '{1}' (v{2})", response.StatusCode,
                    response.ReasonPhrase, response.Version);
      ts.TraceEvent(TraceEventType.Verbose, 5, "HEADERS: {0}", TextUtils.FormatMultiLine(response.Headers.ToString()));
      var headers = response.Content.Headers;
      ts.TraceEvent(TraceEventType.Verbose, 6, "CONTENT ({0}): {1} bytes", headers.ContentType, headers.ContentLength ?? 0);
    }
    var rateLimitInfo = new RateLimitInfo(response.Headers);
    this._rateLimitLock.EnterWriteLock();
    try {
      this._rateLimitInfo = rateLimitInfo;
    }
    finally {
      this._rateLimitLock.ExitWriteLock();
    }
    try {
      return await response.EnsureSuccessfulAsync(cancellationToken);
    }
    catch (HttpError error) {
      if (!string.IsNullOrWhiteSpace(error.Content)) {
        ErrorResult? er;
        try {
          er = JsonSerializer.Deserialize<ErrorResult>(error.Content, Query.JsonReaderOptions);
          if (er is null) {
            throw new JsonException("Error response had null content.");
          }
          ts.TraceEvent(TraceEventType.Verbose, 7, "ERROR '{0}' ({1})", er.Error, er.Help);
          if (er.UnhandledProperties is not null) {
            foreach (var prop in er.UnhandledProperties) {
              ts.TraceEvent(TraceEventType.Verbose, 8, "UNEXPECTED ERROR PROPERTY: {0} -> {1}", prop.Key, prop.Value);
            }
          }
        }
        catch (Exception e) {
          ts.TraceEvent(TraceEventType.Verbose, 9, "FAILED TO PARSE ERROR RESPONSE CONTENT AS JSON: {0}", e.Message);
          er = null;
        }
        if (er is not null) {
          throw new HttpError(error.Status, er.Error, response.Version, $"{er.Error}\n{er.Help}", error);
        }
      }
      throw;
    }
  }

  internal Task<HttpResponseMessage> PerformRequestAsync(string entity, Guid id, string extra, CancellationToken cancellationToken)
    => this.PerformRequestAsync(entity, id.ToString("D"), extra, cancellationToken);

  internal Task<HttpResponseMessage> PerformRequestAsync(string entity, string? id, string extra,
                                                         CancellationToken cancellationToken) {
    return Query.ApplyDelayAsync(() => {
      var uri = this.BuildUri($"{entity}/{id}", extra);
      return this.PerformRequestAsync(uri, HttpMethod.Get, null, cancellationToken);
    }, cancellationToken);
  }

  internal Task<T> PerformRequestAsync<T>(string entity, Guid id, string extra, CancellationToken cancellationToken)
    => this.PerformRequestAsync<T>(entity, id.ToString("D"), extra, cancellationToken);

  internal async Task<T> PerformRequestAsync<T>(string entity, string? id, string extra, CancellationToken cancellationToken) {
    using var response = await this.PerformRequestAsync(entity, id, extra, cancellationToken).ConfigureAwait(false);
    return await JsonUtils.GetJsonContentAsync<T>(response, Query.JsonReaderOptions, cancellationToken).ConfigureAwait(false);
  }

  internal async Task<string> PerformSubmissionAsync(ISubmission submission, CancellationToken cancellationToken) {
    var uri = this.BuildUri(submission.Entity, Query.BuildExtraText("client", submission.Client));
    var method = submission.Method;
    var body = submission.RequestBody;
    using var content = body is null ? null : new StringContent(body, Encoding.UTF8, submission.ContentType);
    using var response = await Query.ApplyDelayAsync(() => this.PerformRequestAsync(uri, method, content, cancellationToken),
                                                     cancellationToken).ConfigureAwait(false);
    return await Query.ExtractMessageAsync(response, cancellationToken).ConfigureAwait(false) ?? "";
  }

  #endregion

}
