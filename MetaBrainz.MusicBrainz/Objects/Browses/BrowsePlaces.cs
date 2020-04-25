using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class BrowsePlaces : BrowseResults<IPlace> {

    public BrowsePlaces(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "place", null, extra, limit, offset) {
    }

    public override IReadOnlyList<IPlace> Results => this.CurrentResult?.Places ?? Array.Empty<IPlace>();

  }

}
