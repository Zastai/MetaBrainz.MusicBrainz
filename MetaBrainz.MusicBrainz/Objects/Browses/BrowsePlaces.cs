using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowsePlaces : BrowseResults<IPlace> {

  public BrowsePlaces(Query query, IReadOnlyDictionary<string, string> options, int? limit,
                      int? offset) : base(query, "place", null, options, limit, offset) {
  }

  public override IReadOnlyList<IPlace> Results => this.CurrentResult?.Places ?? Array.Empty<IPlace>();

}
