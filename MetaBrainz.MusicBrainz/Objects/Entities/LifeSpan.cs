using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class LifeSpan : ILifeSpan {

    [JsonProperty("begin", Required = Required.AllowNull)]
    public PartialDate Begin { get; private set; }

    [JsonProperty("end", Required = Required.AllowNull)]
    public PartialDate End { get; private set; }

    [JsonProperty("ended", Required = Required.Always)]
    public bool Ended { get; private set; }

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
