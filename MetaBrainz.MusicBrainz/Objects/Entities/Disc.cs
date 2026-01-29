using System;
using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Disc : JsonBasedObject, IDisc {

  public required string Id { get; init; }

  public required IReadOnlyList<int> Offsets { get; init; }

  public required IReadOnlyList<IRelease> Releases { get; init; }

  public required int Sectors { get; init; }

  public override string ToString() {
    var duration = TimeSpan.FromSeconds(this.Sectors / 75.0);
    return $"{this.Id} ({this.Offsets.Count} track(s), {duration:g})";
  }

}
