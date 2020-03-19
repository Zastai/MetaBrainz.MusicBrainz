using System;
using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class Rating : JsonBasedObject, IRating {

    [JsonPropertyName("value")]
    public decimal? Value { get; set; }

    [JsonPropertyName("votes-count")]
    public int VoteCount { get; set; }

    public override string ToString() {
      var text = string.Empty;
      if (this.Value.HasValue) {
        var stars = Math.Round(this.Value.Value, MidpointRounding.AwayFromZero);
        for (var i = 1; i <= 5; ++i)
          text = string.Concat(text, (stars >= i) ? "★" : "☆");
        text += $" (votes: {this.VoteCount})";
      }
      return text;
    }

  }

}
