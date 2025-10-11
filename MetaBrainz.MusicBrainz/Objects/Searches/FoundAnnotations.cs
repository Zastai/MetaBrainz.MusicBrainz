using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundAnnotations(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<ISearchResult<IAnnotation>>(query, "annotation", queryString, limit, offset, simple) {

  public override IReadOnlyList<ISearchResult<IAnnotation>> Results => this.CurrentResult?.Annotations ?? [];

}
