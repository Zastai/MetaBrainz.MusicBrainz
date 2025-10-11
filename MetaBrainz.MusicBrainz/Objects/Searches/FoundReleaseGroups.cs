using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundReleaseGroups(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<ISearchResult<IReleaseGroup>>(query, "release-group", queryString, limit, offset, simple) {

  public override IReadOnlyList<ISearchResult<IReleaseGroup>> Results => this.CurrentResult?.ReleaseGroups ?? [];

}
