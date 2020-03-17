using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.XPath;

using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Submissions;
using MetaBrainz.MusicBrainz.Objects;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz {

  public sealed partial class Query : IDisposable {

    #region JSON Options

    private static readonly JsonSerializerOptions SerializerOptions = new JsonSerializerOptions {
      // @formatter:off
      AllowTrailingCommas         = false,
      IgnoreNullValues            = false,
      IgnoreReadOnlyProperties    = true,
      PropertyNameCaseInsensitive = false,
      WriteIndented               = true,
      // @formatter:on
      Converters = {
        // Mappers for interfaces that appear in scalar properties.
        // @formatter:off
        new JsonInterfaceConverter<IArea,               Area              >(),
        new JsonInterfaceConverter<IArtist,             Artist            >(),
        new JsonInterfaceConverter<ICoordinates,        Coordinates       >(),
        new JsonInterfaceConverter<ICoverArtArchive,    CoverArtArchive   >(),
        new JsonInterfaceConverter<IEvent,              Event             >(),
        new JsonInterfaceConverter<IInstrument,         Instrument        >(),
        new JsonInterfaceConverter<ILabel,              Label             >(),
        new JsonInterfaceConverter<ILifeSpan,           LifeSpan          >(),
        new JsonInterfaceConverter<IPlace,              Place             >(),
        new JsonInterfaceConverter<IRating,             Rating            >(),
        new JsonInterfaceConverter<IRecording,          Recording         >(),
        new JsonInterfaceConverter<IRelease,            Release           >(),
        new JsonInterfaceConverter<IReleaseGroup,       ReleaseGroup      >(),
        new JsonInterfaceConverter<ISeries,             Series            >(),
        new JsonInterfaceConverter<ITextRepresentation, TextRepresentation>(),
        new JsonInterfaceConverter<ITrack,              Track             >(),
        new JsonInterfaceConverter<IUrl,                Url               >(),
        new JsonInterfaceConverter<IUserRating,         UserRating        >(),
        new JsonInterfaceConverter<IWork,               Work              >(),
        // @formatter:on
        // Mappers for interfaces that appear in array properties.
        // @formatter:off
        new JsonInterfaceListConverter<IAlias,         Alias        >(),
        new JsonInterfaceListConverter<ICollection,    Collection   >(),
        new JsonInterfaceListConverter<IDisc,          Disc         >(),
        new JsonInterfaceListConverter<ILabelInfo,     LabelInfo    >(),
        new JsonInterfaceListConverter<IMedium,        Medium       >(),
        new JsonInterfaceListConverter<INameCredit,    NameCredit   >(),
        new JsonInterfaceListConverter<IRecording,     Recording    >(),
        new JsonInterfaceListConverter<IRelationship,  Relationship >(),
        new JsonInterfaceListConverter<IRelease,       Release      >(),
        new JsonInterfaceListConverter<IReleaseEvent,  ReleaseEvent >(),
        new JsonInterfaceListConverter<IReleaseGroup,  ReleaseGroup >(),
        new JsonInterfaceListConverter<ISimpleTrack,   SimpleTrack  >(),
        new JsonInterfaceListConverter<ITag,           Tag          >(),
        new JsonInterfaceListConverter<ITrack,         Track        >(),
        new JsonInterfaceListConverter<IUserTag,       UserTag      >(),
        new JsonInterfaceListConverter<IWork,          Work         >(),
        new JsonInterfaceListConverter<IWorkAttribute, WorkAttribute>(),
        // @formatter:on
        // This one is for UnhandledProperties - it tries to create useful types for a field of type 'object'
        new JsonAnythingConverter(),
      }
    };

    internal static T Deserialize<T>(string json) {
      return JsonUtils.Deserialize<T>(json, Query.SerializerOptions);
    }

    #endregion

    #region Message / Error Handling

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    private sealed class MessageOrError {

      [JsonPropertyName("error")]
      public string? Error { get; set; }

      [JsonPropertyName("message")]
      public string? Message { get; set; }

    }

    private static string? ExtractError(WebResponse response) {
      if (response.ContentLength == 0)
        return null;
      try {
        using var stream = response.GetResponseStream();
        if (stream == null)
          return null;
        if (response.ContentType.StartsWith("application/xml")) {
          Debug.Print($"[{DateTime.UtcNow}] => RESPONSE ({response.ContentType}): <{response.ContentLength} byte(s)>");
          StringBuilder? sb = null;
          var xpath = new XPathDocument(stream).CreateNavigator().Select("/error/text");
          while (xpath.MoveNext()) {
            if (sb == null)
              sb = new StringBuilder();
            else
              sb.AppendLine();
            sb.Append(xpath.Current?.InnerXml);
          }
          Debug.Print($"[{DateTime.UtcNow}] => ERROR: \"{sb}\"");
          return sb?.ToString();
        }
        if (response.ContentType.StartsWith("application/json")) {
          var characterSet = "utf-8";
          if (response is HttpWebResponse httpResponse) {
            characterSet = httpResponse.CharacterSet;
            if (string.IsNullOrWhiteSpace(characterSet))
              characterSet = "utf-8";
          }
          var enc = Encoding.GetEncoding(characterSet);
          using var sr = new StreamReader(stream, enc);
          var json = sr.ReadToEnd();
          Debug.Print($"[{DateTime.UtcNow}] => RESPONSE ({response.ContentType}): \"{JsonUtils.Prettify(json)}\"");
          var moe = JsonUtils.Deserialize<MessageOrError>(json);
          Debug.Print($"[{DateTime.UtcNow}] => ERROR: \"{moe?.Error}\"");
          return moe?.Error;
        }
        Debug.Print($"[{DateTime.UtcNow}] => UNHANDLED ERROR RESPONSE ({response.ContentType}): <{response.ContentLength} byte(s)>");
      }
      catch (Exception e) {
        Debug.Print($"[{DateTime.UtcNow}] => FAILED TO PROCESS ERROR RESPONSE: [{e.GetType()}] {e.Message}");
        /* keep calm and fall through */
      }
      return null;
    }

    private static async Task<string?> ExtractErrorAsync(WebResponse response) {
      if (response == null || response.ContentLength == 0)
        return null;
      try {
#if NETSTD_GE_2_1 || NETCORE_GE_3_0
        var stream = response.GetResponseStream();
        await using var _ = stream.ConfigureAwait(false);
#else
        using var stream = response.GetResponseStream();
#endif
        if (stream == null)
          return null;
        if (response.ContentType.StartsWith("application/xml")) {
          Debug.Print($"[{DateTime.UtcNow}] => RESPONSE ({response.ContentType}): <{response.ContentLength} byte(s)>");
          StringBuilder? sb = null;
          var xpath = new XPathDocument(stream).CreateNavigator().Select("/error/text");
          while (xpath.MoveNext()) {
            if (sb == null)
              sb = new StringBuilder();
            else
              sb.AppendLine();
            sb.Append(xpath.Current?.InnerXml);
          }
          Debug.Print($"[{DateTime.UtcNow}] => ERROR: \"{sb}\"");
          return sb?.ToString();
        }
        if (response.ContentType.StartsWith("application/json")) {
          var characterSet = "utf-8";
          if (response is HttpWebResponse httpResponse) {
            characterSet = httpResponse.CharacterSet;
            if (string.IsNullOrWhiteSpace(characterSet))
              characterSet = "utf-8";
          }
          var enc = Encoding.GetEncoding(characterSet);
          using var sr = new StreamReader(stream, enc, false, 1024, true);
          var json = await sr.ReadToEndAsync().ConfigureAwait(false);
          Debug.Print($"[{DateTime.UtcNow}] => RESPONSE ({response.ContentType}): \"{JsonUtils.Prettify(json)}\"");
          var moe = JsonUtils.Deserialize<MessageOrError>(json);
          Debug.Print($"[{DateTime.UtcNow}] => ERROR: \"{moe?.Error}\"");
          return moe?.Error;
        }
        Debug.Print($"[{DateTime.UtcNow}] => UNHANDLED ERROR RESPONSE ({response.ContentType}): <{response.ContentLength} byte(s)>");
      }
      catch (Exception e) {
        Debug.Print($"[{DateTime.UtcNow}] => FAILED TO PROCESS ERROR RESPONSE: [{e.GetType()}] {e.Message}");
        // keep calm and fall through
      }
      return null;
    }

    private static string? ExtractMessage(string response) {
      try {
        Debug.Print($"[{DateTime.UtcNow}] => RESPONSE (application/json): \"{JsonUtils.Prettify(response)}\"");
        var moe = JsonUtils.Deserialize<MessageOrError>(response);
        Debug.Print($"[{DateTime.UtcNow}] => MESSAGE: \"{moe?.Message}\"");
        return moe?.Message;
      }
      catch { /* keep calm and fall through */ }
      return null;
    }

    #endregion

    #region Delay Processing

    private static readonly SemaphoreSlim RequestLock = new SemaphoreSlim(1);

    private static DateTime _lastRequestTime;

    private static T ApplyDelay<T>(Func<T> request) {
      if (DelayBetweenRequests <= 0.0)
        return request();
      while (true) {
        RequestLock.Wait();
        try {
          if ((DateTime.UtcNow - _lastRequestTime).TotalSeconds >= DelayBetweenRequests) {
            _lastRequestTime = DateTime.UtcNow;
            return request();
          }
        }
        finally {
          RequestLock.Release();
        }
        Thread.Sleep((int) (500 * DelayBetweenRequests));
      }
    }

    private static async Task<T> ApplyDelayAsync<T>(Func<Task<T>> request) {
      if (DelayBetweenRequests <= 0.0)
        return await request().ConfigureAwait(false);
      while (true) {
        await RequestLock.WaitAsync();
        try {
          if ((DateTime.UtcNow - _lastRequestTime).TotalSeconds >= DelayBetweenRequests) {
            _lastRequestTime = DateTime.UtcNow;
            return await request().ConfigureAwait(false);
          }
        }
        finally {
          RequestLock.Release();
        }
        await Task.Delay((int) (500 * DelayBetweenRequests)).ConfigureAwait(false);
      }
    }

    #endregion

    #region Query String Processing

    private static void AddIncludeText(StringBuilder sb, Include inc) {
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
      if ((inc & Include.Genres)      != 0) { sb.Append(letter).Append("genres");       letter = '+'; }
      if ((inc & Include.Ratings)     != 0) { sb.Append(letter).Append("ratings");      letter = '+'; }
      if ((inc & Include.Tags)        != 0) { sb.Append(letter).Append("tags");         letter = '+'; }
      if ((inc & Include.UserGenres)  != 0) { sb.Append(letter).Append("user-genres");  letter = '+'; }
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
      if (type.HasValue) {
        sb.Append((sb.Length == 0) ? '?' : '&').Append("type");
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
        sb.Append((sb.Length == 0) ? '?' : '&').Append("status");
        var letter = '=';
        if ((status.Value & ReleaseStatus.Bootleg)       != 0) { sb.Append(letter).Append("bootleg");        letter = '|'; }
        if ((status.Value & ReleaseStatus.Official)      != 0) { sb.Append(letter).Append("official");       letter = '|'; }
        if ((status.Value & ReleaseStatus.Promotion)     != 0) { sb.Append(letter).Append("promotion");      letter = '|'; }
        if ((status.Value & ReleaseStatus.PseudoRelease) != 0) { sb.Append(letter).Append("pseudo-release"); letter = '|'; }
      }
    }

    private static string BuildExtraText(Include inc) {
      var sb = new StringBuilder();
      AddIncludeText(sb, inc);
      return sb.ToString();
    }

    private static string BuildExtraText(Include inc, Uri resource) {
      var sb = new StringBuilder();
      sb.Append("?resource=").Append(Uri.EscapeDataString(resource.ToString()));
      AddIncludeText(sb, inc);
      return sb.ToString();
    }

    private static string BuildExtraText(Include inc, ReleaseStatus? status, ReleaseType? type = null) {
      var sb = new StringBuilder();
      AddIncludeText(sb, inc);
      AddReleaseFilter(sb, type, status);
      return sb.ToString();
    }

    private static string BuildExtraText(Include inc, int[]? toc, bool allMediaFormats, bool noStubs) {
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
      AddIncludeText(sb, inc);
      return sb.ToString();
    }

    private static string BuildExtraText(Include inc, string query, ReleaseType? type = null, ReleaseStatus? status = null) {
      if (string.IsNullOrWhiteSpace(query))
        throw new ArgumentException("A browse or search query must not be blank.", nameof(query));
      var sb = new StringBuilder();
      sb.Append('?').Append(query);
      AddIncludeText(sb, inc);
      AddReleaseFilter(sb, type, status);
      return sb.ToString();
    }

    #endregion

    #region Web Client / IDisposable

    private readonly SemaphoreSlim _clientLock = new SemaphoreSlim(1);

    private bool _disposed;

    private readonly string _fullUserAgent;

    private WebClient? _webClient;

    private WebClient WebClient {
      get {
        if (this._disposed)
          throw new ObjectDisposedException(nameof(Query));
        var wc = this._webClient ??= new WebClient { Encoding = Encoding.UTF8 };
        wc.BaseAddress = this.BaseUri.ToString();
        return wc;
      }
    }

    /// <summary>Closes the web client in use by this query, if there is one.</summary>
    /// <remarks>The next web service request will create a new client.</remarks>
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public void Close() {
      this._clientLock.Wait();
      try {
        this._webClient?.Dispose();
        this._webClient = null;
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
      if (!disposing)
        return;
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

    private string PerformRequest(string address, Method method, string accept, string? contentType, string? body = null) {
      Debug.Print($"[{DateTime.UtcNow}] WEB SERVICE REQUEST: {method} {this.BaseUri}{address}");
      this._clientLock.Wait();
      try {
        var wc = this.WebClient;
        if (this.BearerToken != null)
          wc.Headers.Set("Authorization", $"Bearer {this.BearerToken}");
        if (contentType != null)
          wc.Headers.Set("Content-Type", contentType);
        wc.Headers.Set("Accept", accept);
        wc.Headers.Set("User-Agent", this._fullUserAgent);
        wc.QueryString.Clear();
        try {
          if (method == Method.GET)
            return wc.DownloadString(address);
          if (body != null)
            Debug.Print($"[{DateTime.UtcNow}] => BODY ({contentType}): {body}");
          return wc.UploadString(address, method.ToString(), body ?? string.Empty);
        }
        catch (WebException we) {
          var msg = ExtractError(we.Response);
          if (msg != null)
            throw new QueryException(msg, we);
          throw;
        }
      }
      finally {
        this._clientLock.Release();
      }
    }

    internal string PerformRequest(string entity, string? id, string extra) {
      var address = $"{entity}/{id}{extra}";
      var json = ApplyDelay(() => this.PerformRequest(address, Method.GET, "application/json", null));
      Debug.Print($"[{DateTime.UtcNow}] => JSON: <<{JsonUtils.Prettify(json)}>>");
      return json;
    }

    private async Task<string> PerformRequestAsync(string address, Method method, string accept, string? contentType, string? body = null) {
      Debug.Print($"[{DateTime.UtcNow}] WEB SERVICE REQUEST: {method} {this.BaseUri}{address}");
      await this._clientLock.WaitAsync();
      try {
        var wc = this.WebClient;
        if (this.BearerToken != null)
          wc.Headers.Set("Authorization", $"Bearer {this.BearerToken}");
        if (contentType != null)
          wc.Headers.Set("Content-Type", contentType);
        wc.Headers.Set("Accept", accept);
        wc.Headers.Set("User-Agent", this._fullUserAgent);
        wc.QueryString.Clear();
        try {
          if (method == Method.GET)
            return await wc.DownloadStringTaskAsync(address).ConfigureAwait(false);
          if (body != null)
            Debug.Print($"[{DateTime.UtcNow}] => BODY ({contentType}): {body}");
          return await wc.UploadStringTaskAsync(address, method.ToString(), body ?? string.Empty).ConfigureAwait(false);
        }
        catch (WebException we) {
          var msg = await ExtractErrorAsync(we.Response);
          if (msg != null)
            throw new QueryException(msg, we);
          throw;
        }
      }
      finally {
        this._clientLock.Release();
      }
    }

    internal async Task<string> PerformRequestAsync(string entity, string? id, string extra) {
      var address = $"{entity}/{id}{extra}";
      var json = await ApplyDelayAsync(() => this.PerformRequestAsync(address, Method.GET, "application/json", null));
      Debug.Print($"[{DateTime.UtcNow}] => JSON: <<{JsonUtils.Prettify(json)}>>");
      return json;
    }

    internal string PerformSubmission(ISubmission submission) {
      var address = $"{submission.Entity}/?client={submission.Client}";
      var method = submission.Method;
      var contentType = submission.ContentType;
      var body = submission.RequestBody;
      var msg = ApplyDelay(() => this.PerformRequest(address, method, "application/json", contentType, body));
      return ExtractMessage(msg) ?? "";
    }

    internal async Task<string> PerformSubmissionAsync(ISubmission submission) {
      var address = $"{submission.Entity}/?client={submission.Client}";
      var method = submission.Method;
      var contentType = submission.ContentType;
      var body = submission.RequestBody;
      var msg = await ApplyDelayAsync(() => this.PerformRequestAsync(address, method, "application/json", contentType, body));
      return ExtractMessage(msg) ?? "";
    }

    #endregion

  }

}
