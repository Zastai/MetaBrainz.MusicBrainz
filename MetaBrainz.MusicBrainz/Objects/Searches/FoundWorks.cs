using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundWorks(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<IWork>(query, "work", queryString, limit, offset, simple, static r => r?.Works);
