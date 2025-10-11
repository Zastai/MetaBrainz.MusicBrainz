using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseLabels(Query query, IReadOnlyDictionary<string, string> options, int? limit, int? offset)
  : BrowseResults<ILabel>(query, "label", null, options, limit, offset) {

  public override IReadOnlyList<ILabel> Results => this.CurrentResult?.Labels ?? [];

}
