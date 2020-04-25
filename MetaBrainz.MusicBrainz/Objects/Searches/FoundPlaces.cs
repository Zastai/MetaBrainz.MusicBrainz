using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundPlaces : SearchResults<ISearchResult<IPlace>> {

    public FoundPlaces(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "place", queryString, limit, offset)
    { }

    public override IReadOnlyList<ISearchResult<IPlace>> Results => this.CurrentResult?.Places ?? Array.Empty<ISearchResult<IPlace>>();

  }

}
