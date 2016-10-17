using System;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class WorkAttribute : IWorkAttribute {

    public string Type => this._json.type;

    public Guid? TypeId => this._json.type_id;

    public string Value => this._json.value;

    public Guid? ValueId => this._json.value_id;

    #region JSON-Based Construction

    internal WorkAttribute(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty] public string type;
      [JsonProperty("type-id")] public Guid? type_id;
      [JsonProperty(Required = Required.Always)] public string value;
      [JsonProperty("value-id")] public Guid? value_id;
    }

    #endregion

  }

}
