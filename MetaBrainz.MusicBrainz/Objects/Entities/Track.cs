using System;
using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Track : JsonBasedObject, ITrack {

  public Track(Guid id, string title) {
    this.Id = id;
    this.Title = title;
  }

  public IReadOnlyList<INameCredit>? ArtistCredit { get; init; }

  public Guid Id { get; }

  public TimeSpan? Length { get; init; }

  public string? Number { get; init; }

  public int? Position { get; init; }

  public IRecording? Recording { get; init; }

  public string Title { get; }

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
