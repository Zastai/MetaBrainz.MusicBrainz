using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Local")]
  [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
  [JsonObject(MemberSerialization.OptIn)]
  internal sealed class ReleaseEvent : IReleaseEvent {

    public IArea Area => this._area;

    [JsonProperty("area", Required = Required.Default)]
    private Area _area = null;

    [JsonProperty("date", Required = Required.Default)]
    public PartialDate Date { get; private set; }

    #region Search Server Compatibility

    // The search server's serialization differs in the following ways:
    // - the area and date are not serialized when not set (instead of being serialized as null)
    // => Adjusted the Required flags for affected properties (to allow their omission).

    #endregion

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
