using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class CoverArtArchive : ICoverArtArchive {

    public bool Artwork => this._json.artwork;

    public bool Back => this._json.back;

    public int Count => this._json.count;

    public bool? Darkened => this._json.darkened;

    public bool Front => this._json.front;

    #region JSON-Based Construction

    internal CoverArtArchive(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty] public bool artwork;
      [JsonProperty] public bool back;
      [JsonProperty] public int count;
      [JsonProperty] public bool? darkened;
      [JsonProperty] public bool front;
    }

    #endregion

  }

}
