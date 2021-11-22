using System;
using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities; 

internal sealed class Medium : JsonBasedObject, IMedium {

  public IReadOnlyList<ITrack>? DataTracks { get; set; }

  public IReadOnlyList<IDisc>? Discs { get; set; }

  public string? Format { get; set; }

  public Guid? FormatId { get; set; }

  public int Position { get; set; }

  public ITrack? Pregap { get; set; }

  public string? Title { get; set; }

  public int TrackCount { get; set; }

  public int? TrackOffset { get; set; }

  public IReadOnlyList<ITrack>? Tracks { get; set; }

  public override string ToString() {
    var text = this.Format ?? "Medium";
    if (!string.IsNullOrEmpty(this.Title)) {
      text += " “" + this.Title + "”";
    }
    text += $" ({this.TrackCount} track(s))";
    return text;
  }

}