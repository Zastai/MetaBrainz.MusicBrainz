using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundLabels(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<ILabel>(query, "label", queryString, limit, offset, simple, static r => r?.Labels);
