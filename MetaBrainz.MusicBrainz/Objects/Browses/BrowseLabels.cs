using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseLabels(Query query, ReadOnlyQueryOptions options, int? limit, int? offset)
  : BrowseResults<ILabel>(query, "label", null, options, limit, offset, static r => r?.Labels);
