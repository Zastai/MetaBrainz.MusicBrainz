using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  public sealed class SimpleTrack : ISimpleTrack {

    public string Artist => this._json.artist;

    public int Length => this._json.length;

    public string Title => this._json.title;

    #region JSON-Based Construction

    internal SimpleTrack(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty] public string artist;
      [JsonProperty] public int length;
      [JsonProperty] public string title;
    }

    #endregion

  }

}
