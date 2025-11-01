using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseRecordings(Query query, ReadOnlyQueryOptions options, int? limit, int? offset)
  : BrowseResults<IRecording>(query, "recording", null, options, limit, offset, static r => r?.Recordings);
