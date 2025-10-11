using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundReleases(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<IRelease>(query, "release", queryString, limit, offset, simple, static r => r?.Releases);
