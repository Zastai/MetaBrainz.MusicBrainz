using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseCollections : BrowseResults<ICollection> {

  public BrowseCollections(Query query, IReadOnlyDictionary<string, string> options, int? limit,
                           int? offset) : base(query, "collection", null, options, limit, offset) {
  }

  public override IReadOnlyList<ICollection> Results => this.CurrentResult?.Collections ?? Array.Empty<ICollection>();

}
