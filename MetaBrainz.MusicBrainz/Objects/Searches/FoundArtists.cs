using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundArtists(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<IArtist>(query, "artist", queryString, limit, offset, simple, static r => r?.Artists);
