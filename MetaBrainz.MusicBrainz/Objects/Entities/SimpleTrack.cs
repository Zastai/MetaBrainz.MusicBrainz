using System;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class SimpleTrack : JsonBasedObject, ISimpleTrack {

  public string? Artist { get; init; }

  public required TimeSpan Length { get; init; }

  public required string Title { get; init; }

  public override string ToString() {
    var text = string.Empty;
    if (this.Artist is not null) {
      text += this.Artist + " / ";
    }
    text += $"{this.Title} ({this.Length:g})";
    return text;
  }

}
