using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundTags(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<ITag>(query, "tag", queryString, limit, offset, simple, static r => r?.Tags);
