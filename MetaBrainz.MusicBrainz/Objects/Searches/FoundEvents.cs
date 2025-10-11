using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundEvents(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<IEvent>(query, "event", queryString, limit, offset, simple, static r => r?.Events);
