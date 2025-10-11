using System;
using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Track : JsonBasedObject, ITrack {

  public IReadOnlyList<INameCredit>? ArtistCredit { get; init; }

  public required Guid Id { get; init; }

  public TimeSpan? Length { get; init; }

  public string? Number { get; init; }

  public int? Position { get; init; }

  public IRecording? Recording { get; init; }

  public required string Title { get; init; }

  public override string ToString() {
    var text = string.Empty;
    if (this.Number is not null) {
      text += $"{this.Number}. ";
    }
    if (this.ArtistCredit is not null) {
      foreach (var nc in this.ArtistCredit) {
        text += nc.ToString();
      }
      text += " / ";
    }
    text += this.Title;
    if (this.Length is not null) {
      text += $" ({this.Length.Value:g})";
    }
    return text;
  }

}
