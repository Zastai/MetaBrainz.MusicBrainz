using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseCollections(Query query, IReadOnlyDictionary<string, string> options, int? limit, int? offset)
  : BrowseResults<ICollection>(query, "collection", null, options, limit, offset, static r => r?.Collections);
