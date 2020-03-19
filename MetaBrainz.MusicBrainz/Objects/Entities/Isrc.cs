using System.Collections.Generic;
using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class Isrc : JsonBasedObject, IIsrc {

    [JsonPropertyName("recordings")]
    public IReadOnlyList<IRecording>? Recordings { get; set; }

    [JsonPropertyName("isrc")]
    public string? Value { get; set; }

    public override string? ToString() => this.Value;

  }

}
