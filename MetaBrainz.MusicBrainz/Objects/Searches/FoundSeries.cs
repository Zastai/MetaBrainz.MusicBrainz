using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundSeries(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<ISearchResult<ISeries>>(query, "series", queryString, limit, offset, simple) {

  public override IReadOnlyList<ISearchResult<ISeries>> Results => this.CurrentResult?.Series ?? [];

}
