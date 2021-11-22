using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundSeries : SearchResults<ISearchResult<ISeries>> {

  public FoundSeries(Query query, string queryString, int? limit, int? offset, bool simple)
    : base(query, "series", queryString, limit, offset, simple) {
  }

  public override IReadOnlyList<ISearchResult<ISeries>> Results
    => this.CurrentResult?.Series ?? Array.Empty<ISearchResult<ISeries>>();

}
