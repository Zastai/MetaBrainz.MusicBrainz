using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundSeries(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<ISeries>(query, "series", queryString, limit, offset, simple, static r => r?.Series);
