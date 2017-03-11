using System;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class Rating : IRating {

    [JsonProperty("value", Required = Required.AllowNull)]
    public decimal? Value { get; private set; }

    [JsonProperty("votes-count", Required = Required.Always)]
    public int VoteCount { get; private set; }

    public override string ToString() {
      var text = string.Empty;
      if (this.Value.HasValue) {
        var stars = Math.Round(this.Value.Value, MidpointRounding.AwayFromZero);
        for (var i = 1; i <= 5; ++i)
          text = string.Concat(text, (stars >= i) ? '★' : '☆');
        text += $" (votes: {this.VoteCount})";
      }
      return text;
    }

  }

}
