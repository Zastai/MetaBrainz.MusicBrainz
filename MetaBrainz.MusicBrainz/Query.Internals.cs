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

  private static readonly ProductInfoHeaderValue LibraryComment = new("(https://github.com/Zastai/MetaBrainz.MusicBrainz)");

  private static readonly ProductInfoHeaderValue LibraryProductInfo = HttpUtils.CreateUserAgentHeader<Query>();

  private HttpClient? _client;

  private Action<HttpClient>? _clientConfiguration;

  private Func<HttpClient>? _clientCreation;

  private readonly bool _clientOwned;

  private bool _disposed;

  private readonly List<ProductInfoHeaderValue> _userAgent = new(Query.DefaultUserAgent);

  private HttpClient Client {
    get {
      if (this._disposed) {
        throw new ObjectDisposedException(nameof(Query));
      }
      if (this._client is null) {
        var client = this._clientCreation is not null ? this._clientCreation() : new HttpClient();
        foreach (var userAgent in this._userAgent) {
          client.DefaultRequestHeaders.UserAgent.Add(userAgent);
        }
        this._clientConfiguration?.Invoke(client);
        this._client = client;
      }
      return this._client;
    }
  }

  /// <summary>
  /// Closes the underlying web service client in use by this MusicBrainz query client, if one has been created.<br/>
  /// The next web service request will create a new client.
  /// </summary>
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

  /// <summary>Discards all resources held by this MusicBrainz query client, if any.</summary>
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

  /// <summary>Finalizes this query, releasing any and all resources.</summary>
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
      var contents = await response.GetStringContentAsync(cancellationToken).ConfigureAwait(false);
      if (!string.IsNullOrWhiteSpace(contents)) {
        try {
          var mr = JsonSerializer.Deserialize<MessageResult>(contents, Query.JsonReaderOptions);
          if (mr is null) {
            throw new JsonException("Message response had null content.");
          }
          message = mr.Message;
          if (mr.UnhandledProperties != null) {
            foreach (var prop in mr.UnhandledProperties) {
              Debug.Print("[{0}] => UNEXPECTED MESSAGE PROPERTY: {1} -> {2}", DateTime.UtcNow, prop.Key, prop.Value);
            }
          }
        }
        catch (Exception e) {
          Debug.Print("[{0}] => FAILED TO PARSE MESSAGE RESPONSE CONTENT AS JSON: {1}", DateTime.UtcNow, e.Message);
          message = null;
        }
        if (message is not null) {
          Debug.Print("[{0}] => MESSAGE: '{1}'", DateTime.UtcNow, message);
        }
        else {
          Debug.Print("[{0}] => MESSAGE RESPONSE CONTENT: '{1}'", DateTime.UtcNow, contents);
        }
      }
      else {
        Debug.Print("[{0}] => NO MESSAGE RESPONSE CONTENT", DateTime.UtcNow);
      }
    }
    catch {
      // keep calm and fall through
    }
    return message;
  }

  private async Task<HttpResponseMessage> PerformRequestAsync(Uri uri, HttpMethod method, HttpContent? body,
                                                              CancellationToken cancellationToken) {
    Debug.Print($"[{DateTime.UtcNow}] WEB SERVICE REQUEST: {method.Method} {uri}");
    var client = this.Client;
    using var request = new HttpRequestMessage(method, uri);
    request.Content = body;
    {
      var headers = request.Headers;
      headers.Accept.Add(Query.AcceptHeader);
      if (this.BearerToken is not null) {
        headers.Authorization = new AuthenticationHeaderValue("Bearer", this.BearerToken);
      }
      // Use whatever user agent the client has set, plus our own.
      foreach (var userAgent in client.DefaultRequestHeaders.UserAgent) {
        headers.UserAgent.Add(userAgent);
      }
      headers.UserAgent.Add(Query.LibraryProductInfo);
      headers.UserAgent.Add(Query.LibraryComment);
      Debug.Print($"[{DateTime.UtcNow}] => HEADERS: {TextUtils.FormatMultiLine(headers.ToString())}");
    }
    if (body is not null) {
      // FIXME: Should this include the actual body text too?
      Debug.Print($"[{DateTime.UtcNow}] => BODY ({body.Headers.ContentType}): {body.Headers.ContentLength ?? 0} bytes");
    }
    var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
    Debug.Print($"[{DateTime.UtcNow}] WEB SERVICE RESPONSE: {(int) response.StatusCode}/{response.StatusCode} " +
                $"'{response.ReasonPhrase}' (v{response.Version})");
    Debug.Print($"[{DateTime.UtcNow}] => HEADERS: {TextUtils.FormatMultiLine(response.Headers.ToString())}");
    Debug.Print($"[{DateTime.UtcNow}] => CONTENT ({response.Content.Headers.ContentType}): " +
                $"{response.Content.Headers.ContentLength ?? 0} bytes");
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
          Debug.Print("[{0}] => ERROR '{1}' ({2})", DateTime.UtcNow, er.Error, er.Help);
          if (er.UnhandledProperties != null) {
            foreach (var prop in er.UnhandledProperties) {
              Debug.Print("[{0}] => UNEXPECTED ERROR PROPERTY: {1} -> {2}", DateTime.UtcNow, prop.Key, prop.Value);
            }
          }
        }
        catch (Exception e) {
          Debug.Print("[{0}] => FAILED TO PARSE ERROR RESPONSE CONTENT AS JSON: {1}", DateTime.UtcNow, e.Message);
          er = null;
        }
        if (er != null) {
          throw new HttpError(error.Status, er.Error, response.Version, er.Help, error);
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
