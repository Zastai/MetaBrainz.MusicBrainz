using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundReleaseGroups : SearchResults<ISearchResult<IReleaseGroup>> {

  public FoundReleaseGroups(Query query, string queryString, int? limit, int? offset, bool simple)
    : base(query, "release-group", queryString, limit, offset, simple) {
  }

  public override IReadOnlyList<ISearchResult<IReleaseGroup>> Results
    => this.CurrentResult?.ReleaseGroups ?? Array.Empty<ISearchResult<IReleaseGroup>>();

}
