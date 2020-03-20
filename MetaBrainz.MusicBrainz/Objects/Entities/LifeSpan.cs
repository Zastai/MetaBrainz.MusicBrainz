using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class LifeSpan : JsonBasedObject, ILifeSpan {

    [JsonPropertyName("begin")]
    public PartialDate? Begin { get; set; }

    [JsonPropertyName("end")]
    public PartialDate? End { get; set; }

    public bool Ended => this.MaybeEnded.GetValueOrDefault();

    // The search server sometimes serializes this as null, so we need a bool? as JSON property value.
    [JsonPropertyName("ended")]
    public bool? MaybeEnded { get; set; }

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

}
