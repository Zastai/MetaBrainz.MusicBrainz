using System;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class SimpleTrack : JsonBasedObject, ISimpleTrack {

    [JsonPropertyName("artist")]
    public string? Artist { get; set; }

    [JsonPropertyName("length")]
    public int Length { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    public override string ToString() {
      var text = string.Empty;
      if (this.Artist != null)
        text += this.Artist + " / ";
      var ts = new TimeSpan(0, 0, 0, 0, this.Length);
      text += $"{this.Title} ({ts:g})";
      return text;
    }

  }

}
