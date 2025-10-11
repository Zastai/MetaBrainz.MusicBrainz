using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class IswcLookup(Query query, string iswc, IReadOnlyDictionary<string, string> options)
  : BrowseWorksBase(query, "iswc", iswc, options);
