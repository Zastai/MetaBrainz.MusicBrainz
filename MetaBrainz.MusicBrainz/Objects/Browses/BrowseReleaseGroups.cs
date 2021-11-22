using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses; 

internal sealed class BrowseReleaseGroups : BrowseResults<IReleaseGroup> {

  public BrowseReleaseGroups(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "release-group", null, extra, limit, offset) {
  }

  public override IReadOnlyList<IReleaseGroup> Results => this.CurrentResult?.ReleaseGroups ?? Array.Empty<IReleaseGroup>();

}