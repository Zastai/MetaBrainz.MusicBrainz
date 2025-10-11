using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseReleases(Query query, IReadOnlyDictionary<string, string> options, int? limit, int? offset)
  : BrowseResults<IRelease>(query, "release", null, options, limit, offset) {

  public override IReadOnlyList<IRelease> Results => this.CurrentResult?.Releases ?? [];

}
