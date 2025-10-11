using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundUrls(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<IUrl>(query, "url", queryString, limit, offset, simple, static r => r?.Urls);
