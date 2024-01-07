using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseReleases : BrowseResults<IRelease> {

  public BrowseReleases(Query query, IReadOnlyDictionary<string, string> options, int? limit,
                        int? offset) : base(query, "release", null, options, limit, offset) {
  }

  public override IReadOnlyList<IRelease> Results => this.CurrentResult?.Releases ?? Array.Empty<IRelease>();

}
