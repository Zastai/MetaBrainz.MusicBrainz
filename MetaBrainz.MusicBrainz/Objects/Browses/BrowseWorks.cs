namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseWorks(Query query, ReadOnlyQueryOptions options, int? limit, int? offset)
  : BrowseWorksBase(query, "work", null, options, limit, offset);
