using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  internal sealed class BrowseReleases : BrowseResults<IRelease> {

    public BrowseReleases(Query query, string extra, int? limit = null, int? offset = null)
    : base(query, "release", null, extra, limit, offset) {
    }

    public override IReadOnlyList<IRelease> Results => this.CurrentResult?.Releases ?? Array.Empty<IRelease>();

  }

}
