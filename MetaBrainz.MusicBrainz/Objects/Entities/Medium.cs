using System;
using System.Collections.Generic;
using System.Text;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Medium : JsonBasedObject, IMedium {

  public required IReadOnlyList<ITrack> DataTracks { get; init; }

  public required IReadOnlyList<IDisc> Discs { get; init; }

  public string? Format { get; init; }

  public Guid? FormatId { get; init; }

  public Guid? Id { get; init; }

  public int Position { get; init; }

  public ITrack? Pregap { get; init; }

  public required string Title { get; init; }

  public int TrackCount { get; init; }

  public int? TrackOffset { get; init; }

  public required IReadOnlyList<ITrack> Tracks { get; init; }

  public override string ToString() {
    var text = new StringBuilder();
    text.Append(this.Format ?? "Medium");
    if (this.Title is not "") {
      text.Append(" “").Append(this.Title).Append('”');
    }
    text.Append(" (").Append(this.TrackCount).Append(" track(s))");
    return text.ToString();
  }

}
