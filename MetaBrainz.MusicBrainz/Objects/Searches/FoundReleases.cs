using System;
using System.Collections.Generic;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundReleases : SearchResults<IFoundRelease> {

    public FoundReleases(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "release", queryString, limit, offset)
    { }

    public override IReadOnlyList<IFoundRelease> Results => this.CurrentResult?.Releases ?? Array.Empty<IFoundRelease>();

  }

}
