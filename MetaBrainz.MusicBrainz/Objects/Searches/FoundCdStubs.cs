using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundCdStubs(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<ISearchResult<ICdStub>>(query, "cdstub", queryString, limit, offset, simple) {

  public override IReadOnlyList<ISearchResult<ICdStub>> Results => this.CurrentResult?.CdStubs ?? [];

}
