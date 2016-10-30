using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class Rating : IRating {

    public decimal Value => this._json.value;

    public int VoteCount => this._json.votes_count;

    #region JSON-Based Construction

    internal Rating(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty(Required = Required.Always)] public decimal value;
      [JsonProperty("votes-count", Required = Required.Always)] public int votes_count;
    }

    #endregion

  }

}
