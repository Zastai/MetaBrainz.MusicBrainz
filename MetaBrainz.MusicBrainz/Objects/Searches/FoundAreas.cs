using System;
using System.Collections.Generic;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundAreas : SearchResults<IFoundArea> {

    public FoundAreas(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "area", queryString, limit, offset)
    { }

    public override IReadOnlyList<IFoundArea> Results => this.CurrentResult?.Areas ?? Array.Empty<IFoundArea>();

  }

}
