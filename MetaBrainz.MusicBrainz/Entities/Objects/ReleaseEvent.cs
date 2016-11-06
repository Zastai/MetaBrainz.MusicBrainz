using System.Diagnostics.CodeAnalysis;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Objects {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class ReleaseEvent : IReleaseEvent {

    public IArea Area => this._area;

    [JsonProperty("area")]
    private Area _area = null;

    [JsonProperty("date")]
    public PartialDate Date { get; private set; }

    public override string ToString() {
      if (this.Date == null)
        return this._area?.ToString() ?? string.Empty;
      var text = this.Date.ToString();
      if (this.Area != null)
        text += " (" + this.Area + ")";
      return text;
    }

  }

}
