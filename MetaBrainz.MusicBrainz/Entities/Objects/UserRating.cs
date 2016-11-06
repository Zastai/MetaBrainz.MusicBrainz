using System;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class UserRating : IUserRating {

    [JsonProperty("value", Required = Required.AllowNull)]
    public decimal? Value { get; private set; }

    public override string ToString() {
      var text = string.Empty;
      if (this.Value.HasValue) {
        var stars = Math.Round(this.Value.Value, MidpointRounding.AwayFromZero);
        for (var i = 1; i <= 5; ++i)
          text = string.Concat(text, (stars >= i) ? '★' : '☆');
      }
      return text;
    }

  }

}
