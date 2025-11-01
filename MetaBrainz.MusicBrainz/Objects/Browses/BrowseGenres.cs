using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseGenres(Query query, ReadOnlyQueryOptions options, int? limit, int? offset)
  : BrowseResults<IGenre>(query, "genre", "all", options, limit, offset, static r => r?.Genres);
