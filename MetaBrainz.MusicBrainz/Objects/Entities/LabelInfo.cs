using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class LabelInfo : JsonBasedObject, ILabelInfo {

    [JsonPropertyName("catalog-number")]
    public string? CatalogNumber { get; set; }

    [JsonPropertyName("label")]
    public ILabel? Label { get; set; }

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
