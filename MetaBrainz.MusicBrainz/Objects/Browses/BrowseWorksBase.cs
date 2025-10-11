using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal abstract class BrowseWorksBase(Query query, string endpoint, string? value, IReadOnlyDictionary<string, string>? options,
                                        int? limit = null, int? offset = null)
  : BrowseResults<IWork>(query, endpoint, value, options, limit, offset) {

  public sealed override IReadOnlyList<IWork> Results => this.CurrentResult?.Works ?? [];

}
