using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal abstract class BrowseResults<TResult>
  : PagedQueryResults<IBrowseResults<TResult>, TResult, BrowseResult>,
    IBrowseResults<TResult>
where TResult : IEntity {

  protected BrowseResults(Query query, string endpoint, string? value, string extra, int? limit = null, int? offset = null)
    : base(query, endpoint, value, limit, offset) {
    this._extra = extra;
  }

  private readonly string _extra;

  protected sealed override async Task<IBrowseResults<TResult>> Deserialize(HttpResponseMessage response) {
    this.CurrentResult = await Utils.GetJsonContentAsync<BrowseResult>(response, Query.JsonReaderOptions);
    return this;
  }

  protected sealed override string FullExtraText() {
    var extra = this._extra;
    if (string.IsNullOrEmpty(extra)) {
      extra = $"?offset={this.Offset}";
    }
    else {
      extra += $"&offset={this.Offset}";
    }
    if (this.Limit is not null) {
      extra += $"&limit={this.Limit}";
    }
    return extra;
  }

  public override int TotalResults => this.CurrentResult?.Count ?? 0;

  public override IReadOnlyDictionary<string, object?>? UnhandledProperties => this.CurrentResult?.UnhandledProperties;

}
