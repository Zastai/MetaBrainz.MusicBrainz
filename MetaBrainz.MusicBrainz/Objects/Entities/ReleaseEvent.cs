using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class ReleaseEvent : JsonBasedObject, IReleaseEvent {

    [JsonPropertyName("area")]
    public IArea? Area { get; set; }

    [JsonPropertyName("date")]
    public PartialDate? Date { get; set; }

    public override string ToString() {
      if (object.ReferenceEquals(this.Date, null))
        return this.Area?.ToString() ?? string.Empty;
      var text = this.Date.ToString();
      if (this.Area != null)
        text += " (" + this.Area + ")";
      return text;
    }

  }

}
