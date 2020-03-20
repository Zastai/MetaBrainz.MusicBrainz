using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class Disc : JsonBasedObject, IDisc {

    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("offset-count")]
    public int OffsetCount { get; set; }

    [JsonPropertyName("offsets")]
    public IReadOnlyList<int>? Offsets { get; set; }

    [JsonPropertyName("releases")]
    public IReadOnlyList<IRelease>? Releases { get; set; }

    [JsonPropertyName("sectors")]
    public int Sectors { get; set; }

    public override string ToString() {
      var duration = new TimeSpan(0, 0, 0, 0, (int) (this.Sectors / 75.0 * 1000));
      return $"{this.Id} ({this.OffsetCount} track(s), {duration:g})";
    }

  }

}
