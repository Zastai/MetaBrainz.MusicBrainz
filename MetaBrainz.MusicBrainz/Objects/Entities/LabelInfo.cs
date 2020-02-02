using System.Text.Json.Serialization;
using JetBrains.Annotations;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class LabelInfo : JsonBasedObject, ILabelInfo {

    [JsonPropertyName("catalog-number")]
    public string CatalogNumber { get; set; }

    public ILabel Label => this.TheLabel;

    [JsonPropertyName("label")]
    public Label TheLabel { get; set; }

    public override string ToString() {
      var text = string.Empty;
      if (this.Label != null) {
        text += this.Label;
        if (this.CatalogNumber != null)
          text += ": ";
      }
      if (this.CatalogNumber != null)
        text += this.CatalogNumber;
      return text;
    }

  }

}
