using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  internal sealed class FoundReleaseGroups : SearchResults<IFoundReleaseGroup> {

    public FoundReleaseGroups(Query query, string queryString, int? limit = null, int? offset = null)
    : base(query, "release-group", queryString, limit, offset)
    { }

    public override IReadOnlyList<IFoundReleaseGroup> Results => this.CurrentResult?.ReleaseGroups ?? Array.Empty<IFoundReleaseGroup>();

  }

}
