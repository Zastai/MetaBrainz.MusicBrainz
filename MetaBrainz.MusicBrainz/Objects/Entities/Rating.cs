using System;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Rating : JsonBasedObject, IRating {

  public decimal? Value { get; set; }

  public int? VoteCount { get; set; }

  public override string ToString() {
    var text = string.Empty;
    if (this.Value.HasValue) {
      var stars = Math.Round(this.Value.Value, MidpointRounding.AwayFromZero);
      for (var i = 1; i <= 5; ++i) {
        text = string.Concat(text, (stars >= i) ? "★" : "☆");
      }
    }
    else {
      text += "<not rated>";
    }
    if (this.VoteCount.HasValue) {
      text += $" (votes: {this.VoteCount})";
    }
    return text;
  }

}
