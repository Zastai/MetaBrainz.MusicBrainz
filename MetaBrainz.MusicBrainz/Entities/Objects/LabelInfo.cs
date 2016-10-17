using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  public sealed class LabelInfo : ILabelInfo {

    public string CatalogNumber => this._json.catalog_number;

    public ILabel Label => this._json.label.WrapObject(ref this._label, j => new Label(j));

    private Label _label;

    #region JSON-Based Construction

    internal LabelInfo(JSON json) {
      this._json = json;
    }

    private readonly JSON _json;

    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal sealed class JSON {
      [JsonProperty("catalog-number")] public string catalog_number;
      [JsonProperty] public Label.JSON label;
    }

    #endregion

  }

}
