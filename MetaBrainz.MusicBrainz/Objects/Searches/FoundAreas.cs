using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundAreas : SearchResults<ISearchResult<IArea>> {

    public FoundAreas(Query query, string queryString, int? limit, int? offset, bool simple)
    : base(query, "area", queryString, limit, offset, simple)
    { }

    public override IReadOnlyList<ISearchResult<IArea>> Results => this.CurrentResult?.Areas ?? Array.Empty<ISearchResult<IArea>>();

  }

}
