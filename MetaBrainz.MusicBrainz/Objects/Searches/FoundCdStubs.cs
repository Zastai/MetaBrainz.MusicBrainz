using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundCdStubs(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<ICdStub>(query, "cdstub", queryString, limit, offset, simple, static r => r?.CdStubs);
