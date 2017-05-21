using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class LifeSpan : ILifeSpan {

    [JsonProperty("begin", Required = Required.Default)]
    public PartialDate Begin { get; private set; }

    [JsonProperty("end", Required = Required.Default)]
    public PartialDate End { get; private set; }

    public bool Ended => this._ended.GetValueOrDefault();

    [JsonProperty("ended", Required = Required.Default)]
    private bool? _ended = null;

    #region Search Server Compatibility

    // The search server's serialization differs in the following ways:
    // - begin/end are not serialized when not set (instead of being serialized as null)
    // => Adjusted the Required flags for the properties (to allow their omission).
    // - the Ended flag is sometimes omitted, sometimes serialized as null when not set
    // => Adjusted the Required flags for the property (to allow its omission), and added a nullable backing field.

    #endregion

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
