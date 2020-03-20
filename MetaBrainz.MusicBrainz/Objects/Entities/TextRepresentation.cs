using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class TextRepresentation : JsonBasedObject, ITextRepresentation {

    [JsonPropertyName("language")]
    public string? Language { get; set; }

    [JsonPropertyName("script")]
    public string? Script { get; set; }

    public override string ToString() => $"{this.Language ?? "???"} / {this.Script ?? "???"}";

  }

}
