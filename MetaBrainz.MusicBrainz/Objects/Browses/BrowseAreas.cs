using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses; 

internal sealed class BrowseAreas : BrowseResults<IArea> {

  public BrowseAreas(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "area", null, extra, limit, offset)
  { }

  public override IReadOnlyList<IArea> Results => this.CurrentResult?.Areas ?? Array.Empty<IArea>();

}