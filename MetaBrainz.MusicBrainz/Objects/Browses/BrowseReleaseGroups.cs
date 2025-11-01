using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseReleaseGroups(Query query, ReadOnlyQueryOptions options, int? limit, int? offset)
  : BrowseResults<IReleaseGroup>(query, "release-group", null, options, limit, offset, static r => r?.ReleaseGroups);
