using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundReleases(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<ISearchResult<IRelease>>(query, "release", queryString, limit, offset, simple) {

  public override IReadOnlyList<ISearchResult<IRelease>> Results => this.CurrentResult?.Releases ?? [];

}
