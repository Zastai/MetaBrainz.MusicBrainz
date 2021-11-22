using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses; 

internal abstract class BrowseResults<TResult>
  : PagedQueryResults<IBrowseResults<TResult>, TResult, BrowseResult>,
    IBrowseResults<TResult>
where TResult : IEntity {

  protected BrowseResults(Query query, string endpoint, string? value, string extra, int? limit = null, int? offset = null)
    : base(query, endpoint, value, limit, offset) {
    this.Extra = extra;
  }

  private readonly string Extra;

  protected override IBrowseResults<TResult> Deserialize(string json) {
    this.CurrentResult = Query.Deserialize<BrowseResult>(json);
    return this;
  }

  protected sealed override string FullExtraText() {
    var extra = this.Extra;
    if (string.IsNullOrEmpty(extra))
      extra = $"?offset={this.Offset}";
    else
      extra += $"&offset={this.Offset}";
    if (this.Limit.HasValue)
      extra += $"&limit={this.Limit}";
    return extra;
  }

  public override int TotalResults => this.CurrentResult?.Count ?? 0;

  public override IReadOnlyDictionary<string, object?>? UnhandledProperties => this.CurrentResult?.UnhandledProperties;

}