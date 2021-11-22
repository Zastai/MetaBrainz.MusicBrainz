using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities; 

internal sealed class LifeSpan : JsonBasedObject, ILifeSpan {

  public PartialDate? Begin { get; set; }

  public PartialDate? End { get; set; }

  public bool Ended { get; set; }

  public override string ToString() {
    var text = this.Begin?.ToString() ?? "????";
    if (!object.ReferenceEquals(this.End, null)) {
      if (this.End != this.Begin)
        text += $" – {this.End}";
    }
    else if (this.Ended)
      text += " – ????";
    else
      text += " –";
    return text;
  }

}