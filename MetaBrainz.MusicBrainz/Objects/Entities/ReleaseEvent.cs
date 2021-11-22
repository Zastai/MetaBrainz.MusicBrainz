using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class ReleaseEvent : JsonBasedObject, IReleaseEvent {

  public IArea? Area { get; set; }

  public PartialDate? Date { get; set; }

  public override string ToString() {
    if (object.ReferenceEquals(this.Date, null)) {
      return this.Area?.ToString() ?? string.Empty;
    }
    var text = this.Date.ToString();
    if (this.Area != null) {
      text += " (" + this.Area + ")";
    }
    return text;
  }

}
