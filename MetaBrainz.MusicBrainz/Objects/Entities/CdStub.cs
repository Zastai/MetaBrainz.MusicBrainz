using System.Collections.Generic;
using System.Text;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class CdStub : JsonBasedObject, ICdStub {

  public required string Id { get; init; }

  public required string Artist { get; init; }

  public required string Barcode { get; init; }

  public required string Disambiguation { get; init; }

  public required string Title { get; init; }

  public int TrackCount { get; init; }

  public required IReadOnlyList<ISimpleTrack> Tracks { get; init; }

  public override string ToString() {
    var text = new StringBuilder();
    if (this.Artist is not "") {
      text.Append(this.Artist).Append(" / ");
    }
    text.Append(this.Title);
    if (this.Disambiguation is not "") {
      text.Append(" (").Append(this.Disambiguation).Append(')');
    }
    return text.ToString();
  }

}
