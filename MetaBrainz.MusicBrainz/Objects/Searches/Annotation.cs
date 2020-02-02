using System;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Searches {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class Annotation : JsonBasedObject, IFoundAnnotation {

    [JsonPropertyName("entity")]
    public Guid Entity { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("score")]
    public byte Score { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    public override string ToString() => $"[{this.Score,3}] {this.Text}";

  }

}
