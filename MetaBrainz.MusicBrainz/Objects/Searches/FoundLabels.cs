using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundLabels(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<ISearchResult<ILabel>>(query, "label", queryString, limit, offset, simple) {

  public override IReadOnlyList<ISearchResult<ILabel>> Results => this.CurrentResult?.Labels ?? [];

}
