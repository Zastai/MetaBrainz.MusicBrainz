using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundAreas(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<IArea>(query, "area", queryString, limit, offset, simple, static r => r?.Areas);
