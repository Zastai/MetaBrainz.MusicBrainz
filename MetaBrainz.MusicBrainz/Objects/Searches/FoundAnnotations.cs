using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundAnnotations : SearchResults<ISearchResult<IAnnotation>> {

    public FoundAnnotations(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "annotation", queryString, limit, offset)
    { }

    public override IReadOnlyList<ISearchResult<IAnnotation>> Results => this.CurrentResult?.Annotations ?? Array.Empty<ISearchResult<IAnnotation>>();

  }

}
