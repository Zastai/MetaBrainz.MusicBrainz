using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class Tag : ITag {

    public string Name => this._json.name;

    public int VoteCount => this._json.count;

    #region JSON-Based Construction

    internal Tag(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty(Required = Required.Always)] public int count;
      [JsonProperty(Required = Required.Always)] public string name;
    }

    #endregion

  }

}
