using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseAreas : BrowseResults<IArea> {

  public BrowseAreas(Query query, IReadOnlyDictionary<string, string> options, int? limit,
                     int? offset) : base(query, "area", null, options, limit, offset) {
  }

  public override IReadOnlyList<IArea> Results => this.CurrentResult?.Areas ?? Array.Empty<IArea>();

}
