using System;
using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Medium : JsonBasedObject, IMedium {

  public IReadOnlyList<ITrack>? DataTracks { get; init; }

  public IReadOnlyList<IDisc>? Discs { get; init; }

  public string? Format { get; init; }

  public Guid? FormatId { get; init; }

  public Guid? Id { get; init; }

  public int Position { get; init; }

  public ITrack? Pregap { get; init; }

  public string? Title { get; init; }

  public int TrackCount { get; init; }

  public int? TrackOffset { get; init; }

  public IReadOnlyList<ITrack>? Tracks { get; init; }

  public override string ToString() {
    var text = this.Format ?? "Medium";
    if (!string.IsNullOrEmpty(this.Title)) {
      text += " “" + this.Title + "”";
    }
    text += $" ({this.TrackCount} track(s))";
    return text;
  }

}
