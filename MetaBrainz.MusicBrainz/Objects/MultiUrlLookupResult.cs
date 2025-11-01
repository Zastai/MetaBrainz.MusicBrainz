using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects;

internal sealed class MultiUrlLookupResult : JsonBasedObject, IMultiUrlLookupResult {

  public required int Offset { get; init; }

  public required IReadOnlyList<IUrl> Results { get; init; }

  public required int TotalResults { get; init; }

  public static MultiUrlLookupResult Of(IUrl url) => new() {
    Offset = 0,
    Results = [ url ],
    TotalResults = 1,
  };

}
