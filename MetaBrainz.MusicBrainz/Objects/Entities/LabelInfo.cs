using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class LabelInfo : JsonBasedObject, ILabelInfo {

  public string? CatalogNumber { get; init; }

  public ILabel? Label { get; init; }

  public override string ToString() {
    var text = string.Empty;
    if (this.Label is not null) {
      text += this.Label;
      if (this.CatalogNumber is not null) {
        text += ": ";
      }
    }
    if (this.CatalogNumber is not null) {
      text += this.CatalogNumber;
    }
    return text;
  }

}
