using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseRecordings(Query query, IReadOnlyDictionary<string, string> options, int? limit, int? offset)
  : BrowseResults<IRecording>(query, "recording", null, options, limit, offset, static r => r?.Recordings);
