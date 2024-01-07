using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class SearchResults : JsonBasedObject {

  public SearchResults(int count, int offset, DateTimeOffset created) {
    this.Count = count;
    this.Created = created;
    this.Offset = offset;
  }

  public IReadOnlyList<ISearchResult<IAnnotation>>? Annotations;

  public IReadOnlyList<ISearchResult<IArea>>? Areas;

  public IReadOnlyList<ISearchResult<IArtist>>? Artists;

  public IReadOnlyList<ISearchResult<ICdStub>>? CdStubs;

  public readonly int Count;

  public readonly DateTimeOffset Created;

  public IReadOnlyList<ISearchResult<IEvent>>? Events;

  public IReadOnlyList<ISearchResult<IInstrument>>? Instruments;

  public IReadOnlyList<ISearchResult<ILabel>>? Labels;

  public readonly int Offset;

  public IReadOnlyList<ISearchResult<IPlace>>? Places;

  public IReadOnlyList<ISearchResult<IRecording>>? Recordings;

  public IReadOnlyList<ISearchResult<IReleaseGroup>>? ReleaseGroups;

  public IReadOnlyList<ISearchResult<IRelease>>? Releases;

  public IReadOnlyList<ISearchResult<ISeries>>? Series;

  public IReadOnlyList<ISearchResult<ITag>>? Tags;

  public IReadOnlyList<ISearchResult<IUrl>>? Urls;

  public IReadOnlyList<ISearchResult<IWork>>? Works;

}

internal abstract class SearchResults<TInterface>
  : PagedQueryResults<ISearchResults<TInterface>, TInterface, SearchResults>, ISearchResults<TInterface>
where TInterface : ISearchResult {

  protected SearchResults(Query query, string endpoint, string queryString, int? limit, int? offset,
                          bool simple) : base(query, endpoint, null, limit, offset) {
    this._options["query"] = Uri.EscapeDataString(queryString);
    if (simple) {
      this._options["dismax"] = "true";
    }
  }

  private readonly Dictionary<string, string> _options = new();

  public DateTimeOffset? Created => this.CurrentResult?.Created;

  protected sealed override async Task<ISearchResults<TInterface>> DeserializeAsync(HttpResponseMessage response,
                                                                                    CancellationToken cancellationToken) {
    var task = JsonUtils.GetJsonContentAsync<SearchResults>(response, Query.JsonReaderOptions, cancellationToken);
    this.CurrentResult = await task.ConfigureAwait(false);
    if (this.Offset != this.CurrentResult.Offset) {
      Query.TraceSource.TraceEvent(TraceEventType.Verbose, 200, "Unexpected offset in search results: {0} != {1}.", this.Offset,
                                   this.CurrentResult.Offset);
    }
    return this;
  }

  protected sealed override IReadOnlyDictionary<string, string> FullOptions() {
    if (this.Offset > 0) {
      this._options["offset"] = this.Offset.ToString(CultureInfo.InvariantCulture);
    }
    else {
      this._options.Remove("offset");
    }
    if (this.Limit is not null) {
      this._options["limit"] = this.Limit.Value.ToString(CultureInfo.InvariantCulture);
    }
    else {
      this._options.Remove("limit");
    }
    return this._options;
  }

  public sealed override int TotalResults => this.CurrentResult?.Count ?? 0;

  public override IReadOnlyDictionary<string, object?>? UnhandledProperties => this.CurrentResult?.UnhandledProperties;

}
