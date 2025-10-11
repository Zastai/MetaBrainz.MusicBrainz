using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Browses;

internal sealed class BrowseArtists(Query query, IReadOnlyDictionary<string, string> options, int? limit, int? offset)
  : BrowseResults<IArtist>(query, "artist", null, options, limit, offset, static r => r?.Artists);
