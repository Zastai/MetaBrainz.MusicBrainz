using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal abstract class BrowseWorksBase : BrowseResults<IWork> {

  protected BrowseWorksBase(Query query, string endpoint, string? value, IReadOnlyDictionary<string, string>? options,
                            int? limit = null, int? offset = null) : base(query, endpoint, value, options, limit, offset) {
  }

  public sealed override IReadOnlyList<IWork> Results => this.CurrentResult?.Works ?? Array.Empty<IWork>();

}
