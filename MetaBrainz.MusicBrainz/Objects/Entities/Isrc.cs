using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Isrc : JsonBasedObject, IIsrc {

  public required IReadOnlyList<IRecording> Recordings { get; init; }

  public required string Value { get; init; }

  public override string ToString() => this.Value;

}
