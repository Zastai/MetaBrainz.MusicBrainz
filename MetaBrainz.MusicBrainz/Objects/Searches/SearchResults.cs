using System;
using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class SearchResults : JsonBasedObject {

  public SearchResults(int count, int offset, DateTimeOffset created) {
    this.Count = count;
    this.Offset = offset;
    this.Created = created;
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
  : PagedQueryResults<ISearchResults<TInterface>, TInterface, SearchResults>,
    ISearchResults<TInterface>
where TInterface : ISearchResult {

  protected SearchResults(Query query, string endpoint, string queryString, int? limit, int? offset, bool simple)
    : base(query, endpoint, null, limit, offset) {
    this.QueryString = queryString;
    this.Simple = simple;
  }

  private readonly string QueryString;

  private readonly bool Simple;

  public DateTimeOffset? Created => this.CurrentResult?.Created;

  protected sealed override ISearchResults<TInterface> Deserialize(string json) {
    this.CurrentResult = Query.Deserialize<SearchResults>(json);
    return this;
  }

  protected sealed override string FullExtraText() {
    var extra = "?query=" + Uri.EscapeDataString(this.QueryString);
    if (this.Offset > 0) {
      extra += $"&offset={this.Offset}";
    }
    if (this.Limit.HasValue) {
      extra += $"&limit={this.Limit}";
    }
    if (this.Simple) {
      extra += "&dismax=true";
    }
    return extra;
  }

  public sealed override int TotalResults => this.CurrentResult?.Count ?? 0;

  public override IReadOnlyDictionary<string, object?>? UnhandledProperties => this.CurrentResult?.UnhandledProperties;

}
