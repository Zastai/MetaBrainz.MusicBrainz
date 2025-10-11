using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseWorks(Query query, IReadOnlyDictionary<string, string> options, int? limit, int? offset)
  : BrowseWorksBase(query, "work", null, options, limit, offset);
