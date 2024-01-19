using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseLabels : BrowseResults<ILabel> {

  public BrowseLabels(Query query, IReadOnlyDictionary<string, string> options, int? limit,
                      int? offset) : base(query, "label", null, options, limit, offset) {
  }

  public override IReadOnlyList<ILabel> Results => this.CurrentResult?.Labels ?? Array.Empty<ILabel>();

}
