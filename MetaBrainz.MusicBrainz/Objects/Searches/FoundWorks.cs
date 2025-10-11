using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundWorks(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<ISearchResult<IWork>>(query, "work", queryString, limit, offset, simple) {

  public override IReadOnlyList<ISearchResult<IWork>> Results => this.CurrentResult?.Works ?? [];

}
