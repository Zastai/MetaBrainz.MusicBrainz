using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class TextRepresentation : ITextRepresentation {

    public string Language => this._json.language;

    public string Script => this._json.script;

    #region JSON-Based Construction

    internal TextRepresentation(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty] public string language;
      [JsonProperty] public string script;
    }

    #endregion

  }

}
