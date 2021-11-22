using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses; 

internal sealed class BrowseInstruments : BrowseResults<IInstrument> {

  public BrowseInstruments(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "instrument", null, extra, limit, offset) {
  }

  public override IReadOnlyList<IInstrument> Results => this.CurrentResult?.Instruments ?? Array.Empty<IInstrument>();

}