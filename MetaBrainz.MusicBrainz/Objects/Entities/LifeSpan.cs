using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class LifeSpan : JsonBasedObject, ILifeSpan {

  public PartialDate? Begin { get; init; }

  public PartialDate? End { get; init; }

  public bool Ended { get; init; }

  public override string ToString() {
    var text = this.Begin?.ToString() ?? "????";
    if (this.End is not null) {
      if (this.End != this.Begin) {
        text += $" – {this.End}";
      }
    }
    else if (this.Ended) {
      text += " – ????";
    }
    else {
      text += " –";
    }
    return text;
  }

}
