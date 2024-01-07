using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseReleaseGroups : BrowseResults<IReleaseGroup> {

  public BrowseReleaseGroups(Query query, IReadOnlyDictionary<string, string> options, int? limit,
                             int? offset) : base(query, "release-group", null, options, limit, offset) {
  }

  public override IReadOnlyList<IReleaseGroup> Results => this.CurrentResult?.ReleaseGroups ?? Array.Empty<IReleaseGroup>();

}
