using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseReleases(Query query, ReadOnlyQueryOptions options, int? limit, int? offset)
  : BrowseResults<IRelease>(query, "release", null, options, limit, offset, static r => r?.Releases);
