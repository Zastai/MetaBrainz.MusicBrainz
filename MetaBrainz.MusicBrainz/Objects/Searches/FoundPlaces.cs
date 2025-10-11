using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundPlaces(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<IPlace>(query, "place", queryString, limit, offset, simple, static r => r?.Places);
