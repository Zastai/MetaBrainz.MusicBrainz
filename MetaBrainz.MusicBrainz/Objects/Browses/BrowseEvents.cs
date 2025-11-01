using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseEvents(Query query, ReadOnlyQueryOptions options, int? limit, int? offset)
  : BrowseResults<IEvent>(query, "event", null, options, limit, offset, static r => r?.Events);
