using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundPlaces(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<ISearchResult<IPlace>>(query, "place", queryString, limit, offset, simple) {

  public override IReadOnlyList<ISearchResult<IPlace>> Results => this.CurrentResult?.Places ?? [];

}
