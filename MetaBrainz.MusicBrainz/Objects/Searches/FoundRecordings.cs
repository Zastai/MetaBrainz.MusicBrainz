using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Searches;

internal sealed class FoundRecordings(Query query, string queryString, int? limit, int? offset, bool simple)
  : SearchResults<IRecording>(query, "recording", queryString, limit, offset, simple, static r => r?.Recordings);
