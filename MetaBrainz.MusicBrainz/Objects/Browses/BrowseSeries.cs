using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseSeries : BrowseResults<ISeries> {

  public BrowseSeries(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "series", null, extra, limit, offset) {
  }

  public override IReadOnlyList<ISeries> Results => this.CurrentResult?.Series ?? Array.Empty<ISeries>();

}
