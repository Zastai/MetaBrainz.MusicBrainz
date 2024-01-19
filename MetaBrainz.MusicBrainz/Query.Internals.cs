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

  private static async Task<T> ApplyDelayAsync<T>([InstantHandle] Func<CancellationToken, Task<T>> request,
                                                  CancellationToken cancellationToken) {
    if (Query.DelayBetweenRequests <= 0.0) {
      return await request(cancellationToken).ConfigureAwait(false);
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
        return await request(cancellationToken).ConfigureAwait(false);
      }
      await Task.Delay((int) (500 * Query.DelayBetweenRequests), cancellationToken).ConfigureAwait(false);
    }
  }

  #endregion

  #region Query String Processing

  private static void AddIncludeText(IDictionary<string, string> options, Include inc) {
    if (inc == Include.None) {
      return;
    }
    var included = new List<string>(16);
    // Linked Entities
    if ((inc & Include.Artists) != 0) {
      included.Add("artists");
    }
    if ((inc & Include.Collections) != 0) {
      included.Add("collections");
    }
    if ((inc & Include.Labels) != 0) {
      included.Add("labels");
    }
    if ((inc & Include.Recordings) != 0) {
      included.Add("recordings");
    }
    if ((inc & Include.ReleaseGroups) != 0) {
      included.Add("release-groups");
    }
    if ((inc & Include.Releases) != 0) {
      included.Add("releases");
    }
    if ((inc & Include.Works) != 0) {
      included.Add("works");
    }
    // Special Cases
    if ((inc & Include.ArtistCredits) != 0) {
      included.Add("artist-credits");
    }
    if ((inc & Include.DiscIds) != 0) {
      included.Add("discids");
    }
    if ((inc & Include.Isrcs) != 0) {
      included.Add("isrcs");
    }
    if ((inc & Include.Media) != 0) {
      included.Add("media");
    }
    if ((inc & Include.UserCollections) != 0) {
      included.Add("user-collections");
    }
    if ((inc & Include.VariousArtists) != 0) {
      included.Add("various-artists");
    }
    // Optional Info
    if ((inc & Include.Aliases) != 0) {
      included.Add("aliases");
    }
    if ((inc & Include.Annotation) != 0) {
      included.Add("annotation");
    }
    if ((inc & Include.Genres) != 0) {
      included.Add("genres");
    }
    if ((inc & Include.Ratings) != 0) {
      included.Add("ratings");
    }
    if ((inc & Include.Tags) != 0) {
      included.Add("tags");
    }
    if ((inc & Include.UserGenres) != 0) {
      included.Add("user-genres");
    }
    if ((inc & Include.UserRatings) != 0) {
      included.Add("user-ratings");
    }
    if ((inc & Include.UserTags) != 0) {
      included.Add("user-tags");
    }
    // Relationships
    if ((inc & Include.AreaRelationships) != 0) {
      included.Add("area-rels");
    }
    if ((inc & Include.ArtistRelationships) != 0) {
      included.Add("artist-rels");
    }
    if ((inc & Include.EventRelationships) != 0) {
      included.Add("event-rels");
    }
    if ((inc & Include.InstrumentRelationships) != 0) {
      included.Add("instrument-rels");
    }
    if ((inc & Include.LabelRelationships) != 0) {
      included.Add("label-rels");
    }
    if ((inc & Include.PlaceRelationships) != 0) {
      included.Add("place-rels");
    }
    if ((inc & Include.RecordingLevelRelationships) != 0) {
      included.Add("recording-level-rels");
    }
    if ((inc & Include.RecordingRelationships) != 0) {
      included.Add("recording-rels");
    }
    if ((inc & Include.ReleaseGroupLevelRelationships) != 0) {
      included.Add("release-group-level-rels");
    }
    if ((inc & Include.ReleaseGroupRelationships) != 0) {
      included.Add("release-group-rels");
    }
    if ((inc & Include.ReleaseRelationships) != 0) {
      included.Add("release-rels");
    }
    if ((inc & Include.SeriesRelationships) != 0) {
      included.Add("series-rels");
    }
    if ((inc & Include.UrlRelationships) != 0) {
      included.Add("url-rels");
    }
    if ((inc & Include.WorkLevelRelationships) != 0) {
      included.Add("work-level-rels");
    }
    if ((inc & Include.WorkRelationships) != 0) {
      included.Add("work-rels");
    }
    options["inc"] = string.Join('+', included);
  }

  private static void AddReleaseFilter(IDictionary<string, string> options, ReleaseType? type, ReleaseStatus? status) {
    if (type is not null) {
      var types = new List<string>(8);
      // Primary Types
      if ((type.Value & ReleaseType.Album) != 0) {
        types.Add("album");
      }
      if ((type.Value & ReleaseType.Broadcast) != 0) {
        types.Add("broadcast");
      }
      if ((type.Value & ReleaseType.EP) != 0) {
        types.Add("ep");
      }
      if ((type.Value & ReleaseType.Other) != 0) {
        types.Add("other");
      }
      if ((type.Value & ReleaseType.Single) != 0) {
        types.Add("single");
      }
      // Secondary Types
      if ((type.Value & ReleaseType.AudioDrama) != 0) {
        types.Add("audio drama");
      }
      if ((type.Value & ReleaseType.Audiobook) != 0) {
        types.Add("audiobook");
      }
      if ((type.Value & ReleaseType.Compilation) != 0) {
        types.Add("compilation");
      }
      if ((type.Value & ReleaseType.DJMix) != 0) {
        types.Add("dj-mix");
      }
      if ((type.Value & ReleaseType.Demo) != 0) {
        types.Add("demo");
      }
      if ((type.Value & ReleaseType.FieldRecording) != 0) {
        types.Add("field recording");
      }
      if ((type.Value & ReleaseType.Interview) != 0) {
        types.Add("interview");
      }
      if ((type.Value & ReleaseType.Live) != 0) {
        types.Add("live");
      }
      if ((type.Value & ReleaseType.MixTape) != 0) {
        types.Add("mixtape/street");
      }
      if ((type.Value & ReleaseType.Remix) != 0) {
        types.Add("remix");
      }
      if ((type.Value & ReleaseType.Soundtrack) != 0) {
        types.Add("soundtrack");
      }
      if ((type.Value & ReleaseType.SpokenWord) != 0) {
        types.Add("spokenword");
      }
      options["type"] = string.Join('|', types);
    }
    if (status is not null) {
      var statuses = new List<string>(8);
      if ((status.Value & ReleaseStatus.Bootleg) != 0) {
        statuses.Add("bootleg");
      }
      if ((status.Value & ReleaseStatus.Cancelled) != 0) {
        statuses.Add("cancelled");
      }
      if ((status.Value & ReleaseStatus.Official) != 0) {
        statuses.Add("official");
      }
      if ((status.Value & ReleaseStatus.Promotion) != 0) {
        statuses.Add("promotion");
      }
      if ((status.Value & ReleaseStatus.PseudoRelease) != 0) {
        statuses.Add("pseudo-release");
      }
      if ((status.Value & ReleaseStatus.Withdrawn) != 0) {
        statuses.Add("withdrawn");
      }
      options["status"] = string.Join('|', statuses);
    }
  }

  private Uri BuildUri(string path, IReadOnlyDictionary<string, string>? options, string? format)
    => new UriBuilder(this.UrlScheme, this.Server, this.Port, Query.WebServiceRoot + path, Query.Extra(options, format)).Uri;

  private static Dictionary<string, string> CreateOptions(Include inc) {
    var options = new Dictionary<string, string>();
    Query.AddIncludeText(options, inc);
    return options;
  }

  private static Dictionary<string, string> CreateOptions(Include inc, Uri resource) {
    var options = new Dictionary<string, string> { ["resource"] = Uri.EscapeDataString(resource.ToString()) };
    Query.AddIncludeText(options, inc);
    return options;
  }

  private static Dictionary<string, string> CreateOptions(Include inc, ReleaseStatus? status, ReleaseType? type = null) {
    var options = new Dictionary<string, string>();
    Query.AddIncludeText(options, inc);
    Query.AddReleaseFilter(options, type, status);
    return options;
  }

  private static Dictionary<string, string> CreateOptions(string field, Guid id) => Query.CreateOptions(field, id.ToString("D"));

  private static Dictionary<string, string> CreateOptions(string field, Guid id, Include inc, ReleaseType? type = null,
                                                          ReleaseStatus? status = null) {
    var options = new Dictionary<string, string> { [field] = id.ToString("D") };
    Query.AddIncludeText(options, inc);
    Query.AddReleaseFilter(options, type, status);
    return options;
  }

  private static Dictionary<string, string> CreateOptions(string field, string value) => new() { [field] = value };

  private static Dictionary<string, string> CreateOptions(int[]? toc, Include inc, bool allMediaFormats, bool noStubs) {
    var options = new Dictionary<string, string>();
    if (toc is not null) {
      options["toc"] = string.Join('+', toc);
    }
    if (allMediaFormats) {
      options["media-format"] = "all";
    }
    if (noStubs) {
      options["cdstubs"] = "no";
    }
    Query.AddIncludeText(options, inc);
    return options;
  }

  private static string Extra(IReadOnlyDictionary<string, string>? options, string? format) {
    if ((options is null || options.Count == 0) && format is null) {
      return "";
    }
    var sb = new StringBuilder();
    char separator;
    if (format is not null) {
      sb.Append("?fmt=").Append(format);
      separator = '&';
    }
    else {
      separator = '?';
    }
    if (options is not null) {
      foreach (var (field, value) in options) {
        // Should we just use Uri.EscapeDataString here? We'd need to be careful with stuff we join together with | and +.
        var adjustedValue = value.Replace(' ', '+');
        sb.Append(separator).Append(field).Append('=').Append(adjustedValue);
        separator = '&';
      }
    }
    return sb.ToString();
  }

  #endregion

  #region HttpClient / IDisposable

  private static readonly MediaTypeWithQualityHeaderValue AcceptHeaderForJson = new("application/json");

  private static readonly MediaTypeWithQualityHeaderValue AcceptHeaderForText = new("text/plain");

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
                                                              CancellationToken cancellationToken, string? format = null) {
    using var request = new HttpRequestMessage(method, uri);
    var ts = Query.TraceSource;
    ts.TraceEvent(TraceEventType.Verbose, 1, "WEB SERVICE REQUEST: {0} {1}", method.Method, request.RequestUri);
    var client = this.Client;
    {
      var headers = request.Headers;
      switch (format) {
        case "json":
          headers.Accept.Add(Query.AcceptHeaderForJson);
          break;
        case "txt":
          headers.Accept.Add(Query.AcceptHeaderForText);
          break;
      }
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

  internal Task<HttpResponseMessage> PerformRequestAsync(string entity, Guid id, IReadOnlyDictionary<string, string>? options,
                                                         CancellationToken cancellationToken)
    => this.PerformRequestAsync(entity, id.ToString("D"), options, cancellationToken);

  internal Task<HttpResponseMessage> PerformRequestAsync(string entity, string? id, IReadOnlyDictionary<string, string>? options,
                                                         CancellationToken cancellationToken, string? format = "json") {
    var uri = this.BuildUri($"{entity}/{id}", options, format);
    return Query.ApplyDelayAsync(token => this.PerformRequestAsync(uri, HttpMethod.Get, null, token, format), cancellationToken);
  }

  internal Task<T> PerformRequestAsync<T>(string entity, Guid id, IReadOnlyDictionary<string, string>? options,
                                          CancellationToken cancellationToken)
    => this.PerformRequestAsync<T>(entity, id.ToString("D"), options, cancellationToken);

  internal async Task<T> PerformRequestAsync<T>(string entity, string? id, IReadOnlyDictionary<string, string>? options,
                                                CancellationToken cancellationToken) {
    using var response = await this.PerformRequestAsync(entity, id, options, cancellationToken).ConfigureAwait(false);
    return await JsonUtils.GetJsonContentAsync<T>(response, Query.JsonReaderOptions, cancellationToken).ConfigureAwait(false);
  }

  internal async Task<string> PerformSubmissionAsync(ISubmission submission, CancellationToken cancellationToken) {
    var uri = this.BuildUri(submission.Entity, Query.CreateOptions("client", submission.Client), null);
    var method = submission.Method;
    var body = submission.RequestBody;
    using var content = body is null ? null : new StringContent(body, Encoding.UTF8, submission.ContentType);
    var delayedRequest = Query.ApplyDelayAsync(token => this.PerformRequestAsync(uri, method, content, token), cancellationToken);
    using var response = await delayedRequest.ConfigureAwait(false);
    return await Query.ExtractMessageAsync(response, cancellationToken).ConfigureAwait(false) ?? "";
  }

  #endregion

}
