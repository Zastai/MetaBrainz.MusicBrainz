using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundInstruments(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<IInstrument>(query, "instrument", queryString, limit, offset, simple, static r => r?.Instruments);
