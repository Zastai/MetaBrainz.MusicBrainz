using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseAreas(Query query, IReadOnlyDictionary<string, string> options, int? limit, int? offset)
  : BrowseResults<IArea>(query, "area", null, options, limit, offset) {

  public override IReadOnlyList<IArea> Results => this.CurrentResult?.Areas ?? [];

}
