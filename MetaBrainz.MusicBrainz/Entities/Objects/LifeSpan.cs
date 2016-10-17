using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  public sealed class LifeSpan : ILifeSpan {

    public string Begin => this._json.begin;

    public string End => this._json.end;

    public bool? Ended => this._json.ended;

    #region JSON-Based Construction

    internal LifeSpan(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty] public string begin;
      [JsonProperty] public string end;
      [JsonProperty] public bool? ended;
    }

    #endregion

  }

}
