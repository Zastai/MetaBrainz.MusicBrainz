using System;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class Rating : IRating {

    public decimal? Value => this._json.value;

    public int VoteCount => this._json.votes_count;

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

    #region JSON-Based Construction

    internal Rating(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty(Required = Required.AllowNull)] public decimal? value;
      [JsonProperty("votes-count", Required = Required.Always)] public int votes_count;
    }

    #endregion

  }

}
