using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseAreas(Query query, ReadOnlyQueryOptions options, int? limit, int? offset)
  : BrowseResults<IArea>(query, "area", null, options, limit, offset, static r => r?.Areas);
