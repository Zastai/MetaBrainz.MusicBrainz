using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses; 

internal sealed class BrowseLabels : BrowseResults<ILabel> {

  public BrowseLabels(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "label", null, extra, limit, offset) {
  }

  public override IReadOnlyList<ILabel> Results => this.CurrentResult?.Labels ?? Array.Empty<ILabel>();

}