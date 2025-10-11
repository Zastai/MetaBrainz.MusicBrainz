using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class CdStub : JsonBasedObject, ICdStub {

  public required string Id { get; init; }

  public string? Artist { get; init; }

  public string? Barcode { get; init; }

  public string? Disambiguation { get; init; }

  public required string Title { get; init; }

  public int TrackCount { get; init; }

  public IReadOnlyList<ISimpleTrack>? Tracks { get; init; }

  public override string ToString() {
    var text = string.Empty;
    if (this.Artist is not null) {
      text += this.Artist + " / ";
    }
    text += this.Title;
    if (!string.IsNullOrEmpty(this.Disambiguation)) {
      text += " (" + this.Disambiguation + ")";
    }
    return text;
  }

}
