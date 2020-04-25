using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundTags : SearchResults<ISearchResult<ITag>> {

    public FoundTags(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "tag", queryString, limit, offset)
    { }

    public override IReadOnlyList<ISearchResult<ITag>> Results => this.CurrentResult?.Tags ?? Array.Empty<ISearchResult<ITag>>();

  }

}
