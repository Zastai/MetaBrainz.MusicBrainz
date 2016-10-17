using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class NameCredit : INameCredit {

    public IArtist Artist => this._json.artist.WrapObject(ref this._artist, j => new Artist(j));

    private Artist _artist;

    public string JoinPhrase => this._json.joinphrase;

    public string Name => this._json.name;

    #region JSON-Based Construction

    internal NameCredit(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty(Required = Required.Always)] public Artist.JSON artist;
      [JsonProperty(Required = Required.Always)] public string joinphrase;
      [JsonProperty(Required = Required.Always)] public string name;
    }

    #endregion

  }

}
