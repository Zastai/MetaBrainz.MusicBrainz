using System;
using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  internal sealed class Track : JsonBasedObject, ITrack {

    public Track(Guid id, string title) {
      this.Id = id;
      this.Title = title;
    }

    public IReadOnlyList<INameCredit>? ArtistCredit { get; set; }

    public Guid Id { get; }

    public TimeSpan? Length { get; set; }

    public string? Number { get; set; }

    public int? Position { get; set; }

    public IRecording? Recording { get; set; }

    public string Title { get; }

    public override string ToString() {
      var text = string.Empty;
      if (this.Number != null)
        text += $"{this.Number}. ";
      if (this.ArtistCredit != null) {
        foreach (var nc in this.ArtistCredit)
          text += nc.ToString();
        text += " / ";
      }
      text += this.Title;
      if (this.Length.HasValue) {
        text += $" ({this.Length.Value:g})";
      }
      return text;
    }

  }

}
