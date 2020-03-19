using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundAnnotations : SearchResults<IFoundAnnotation> {

    public FoundAnnotations(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "annotation", queryString, limit, offset)
    { }

    public override IReadOnlyList<IFoundAnnotation> Results => this.CurrentResult?.Annotations ?? Array.Empty<IFoundAnnotation>();

  }

}
