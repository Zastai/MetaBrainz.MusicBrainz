using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal abstract class BrowseWorksBase(Query query, string endpoint, string? value, ReadOnlyQueryOptions? options,
                                        int? limit = null, int? offset = null)
  : BrowseResults<IWork>(query, endpoint, value, options, limit, offset, static r => r?.Works);
