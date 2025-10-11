using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundUrls(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<ISearchResult<IUrl>>(query, "url", queryString, limit, offset, simple) {

  public override IReadOnlyList<ISearchResult<IUrl>> Results => this.CurrentResult?.Urls ?? [];

}
