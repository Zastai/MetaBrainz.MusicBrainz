using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundReleaseGroups(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<IReleaseGroup>(query, "release-group", queryString, limit, offset, simple, static r => r?.ReleaseGroups);
