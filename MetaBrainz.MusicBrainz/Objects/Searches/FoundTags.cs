using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundTags : SearchResults<IFoundTag> {

    public FoundTags(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "tag", queryString, limit, offset)
    { }

    public override IReadOnlyList<IFoundTag> Results => this.CurrentResult?.Tags ?? Array.Empty<IFoundTag>();

  }

}
