using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundAnnotations : SearchResults<ISearchResult<IAnnotation>> {

    public FoundAnnotations(Query query, string queryString, int? limit, int? offset, bool simple)
    : base(query, "annotation", queryString, limit, offset, simple)
    { }

    public override IReadOnlyList<ISearchResult<IAnnotation>> Results => this.CurrentResult?.Annotations ?? Array.Empty<ISearchResult<IAnnotation>>();

  }

}
