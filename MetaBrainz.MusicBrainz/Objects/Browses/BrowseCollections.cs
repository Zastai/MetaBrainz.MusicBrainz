using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class BrowseCollections : BrowseResults<ICollection> {

    public BrowseCollections(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "collection", null, extra, limit, offset) {
    }

    public override IReadOnlyList<ICollection> Results => this.CurrentResult?.Collections ?? Array.Empty<ICollection>();

  }

}
