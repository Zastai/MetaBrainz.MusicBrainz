using System;
using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  internal sealed class Alias : IAlias {

    public string BeginDate => this._json.begin_date;

    public string EndDate => this._json.end_date;

    public bool? Ended => this._json.ended;

    public string Locale => this._json.locale;

    public string Name => this._json.name;

    public bool? Primary => this._json.primary;

    public string SortName => this._json.sort_name;

    public string Type => this._json.type;

    public Guid? TypeId => this._json.type_id;

    #region JSON-Based Construction

    internal Alias(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty("begin-date")] public string begin_date;
      [JsonProperty("end-date")] public string end_date;
      [JsonProperty("ended")] public bool? ended;
      [JsonProperty] public string locale;
      [JsonProperty(Required = Required.Always)] public string name;
      [JsonProperty] public bool? primary;
      [JsonProperty("sort-name")] public string sort_name;
      [JsonProperty] public string type;
      [JsonProperty("type-id")] public Guid? type_id;
    }

    #endregion

  }

}
