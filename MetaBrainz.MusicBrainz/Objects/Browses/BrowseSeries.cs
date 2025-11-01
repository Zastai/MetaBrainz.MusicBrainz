using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseSeries(Query query, ReadOnlyQueryOptions options, int? limit, int? offset)
  : BrowseResults<ISeries>(query, "series", null, options, limit, offset, static r => r?.Series);
