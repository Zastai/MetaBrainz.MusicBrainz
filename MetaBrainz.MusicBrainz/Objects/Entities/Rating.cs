using System;
using System.Text;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Rating : JsonBasedObject, IRating {

  public decimal Value { get; init; }

  public int VoteCount { get; init; }

  public override string ToString() {
    var text = new StringBuilder();
    {
      var stars = Math.Round(this.Value, MidpointRounding.AwayFromZero);
      for (var i = 1; i <= 5; ++i) {
        text.Append(stars >= i ? '★' : '☆');
      }
    }
    if (this.VoteCount is not 0) {
      text.Append(" (votes: ").Append(this.VoteCount).Append(')');
    }
    return text.ToString();
  }

}
