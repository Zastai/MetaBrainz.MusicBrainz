using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseSeries : BrowseResults<ISeries> {

  public BrowseSeries(Query query, IReadOnlyDictionary<string, string> options, int? limit,
                      int? offset) : base(query, "series", null, options, limit, offset) {
  }

  public override IReadOnlyList<ISeries> Results => this.CurrentResult?.Series ?? Array.Empty<ISeries>();

}
