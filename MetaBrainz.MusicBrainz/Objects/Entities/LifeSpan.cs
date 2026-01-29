using System.Text;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class LifeSpan : JsonBasedObject, ILifeSpan {

  public PartialDate? Begin { get; init; }

  public PartialDate? End { get; init; }

  public bool Ended { get; init; }

  public override string ToString() {
    var text = new StringBuilder();
    if (this.Begin != null) {
      text.Append(this.Begin);
    }
    else {
      text.Append("????");
    }
    if (this.End is not null) {
      if (this.End != this.Begin) {
        text.Append(" - ").Append(this.End);
      }
    }
    else if (this.Ended) {
      text.Append(" – ????");
    }
    else {
      text.Append(" –");
    }
    return text.ToString();
  }

}
