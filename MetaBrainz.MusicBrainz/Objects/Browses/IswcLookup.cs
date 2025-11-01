namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class IswcLookup(Query query, string iswc, ReadOnlyQueryOptions options)
  : BrowseWorksBase(query, "iswc", iswc, options);
