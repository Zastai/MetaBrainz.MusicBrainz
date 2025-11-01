using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowsePlaces(Query query, ReadOnlyQueryOptions options, int? limit, int? offset)
  : BrowseResults<IPlace>(query, "place", null, options, limit, offset, static r => r?.Places);
