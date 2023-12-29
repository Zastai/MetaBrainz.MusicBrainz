using System;
using System.Collections.Generic;
using System.Diagnostics;
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

  protected SearchResults(Query query, string endpoint, string queryString, int? limit, int? offset, bool simple)
    : base(query, endpoint, null, limit, offset) {
    this._queryString = queryString;
    this._simple = simple;
  }

  private readonly string _queryString;

  private readonly bool _simple;

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

  protected sealed override string FullExtraText() {
    var extra = "?query=" + Uri.EscapeDataString(this._queryString);
    if (this.Offset > 0) {
      extra += $"&offset={this.Offset}";
    }
    if (this.Limit is not null) {
      extra += $"&limit={this.Limit}";
    }
    if (this._simple) {
      extra += "&dismax=true";
    }
    return extra;
  }

  public sealed override int TotalResults => this.CurrentResult?.Count ?? 0;

  public override IReadOnlyDictionary<string, object?>? UnhandledProperties => this.CurrentResult?.UnhandledProperties;

}
