using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundSeries : SearchResults<ISearchResult<ISeries>> {

    public FoundSeries(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "series", queryString, limit, offset)
    { }

    public override IReadOnlyList<ISearchResult<ISeries>> Results => this.CurrentResult?.Series ?? Array.Empty<ISearchResult<ISeries>>();

  }

}
