using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class ReleaseEvent : JsonBasedObject, IReleaseEvent {

  public IArea? Area { get; init; }

  public PartialDate? Date { get; init; }

  public override string ToString() {
    if (this.Date is null) {
      return this.Area?.ToString() ?? string.Empty;
    }
    var text = this.Date.ToString();
    if (this.Area is not null) {
      text += " (" + this.Area + ")";
    }
    return text;
  }

}
