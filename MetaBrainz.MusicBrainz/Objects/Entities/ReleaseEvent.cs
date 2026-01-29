using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class ReleaseEvent : JsonBasedObject, IReleaseEvent {

  public IArea? Area { get; init; }

  public PartialDate? Date { get; init; }

  public override string ToString() {
    if (this.Date is null) {
      return this.Area?.ToString() ?? "";
    }
    return this.Area is null ? this.Date.ToString() : $"{this.Date} ({this.Area})";
  }

}
