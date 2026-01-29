using System;
using System.Collections.Generic;
using System.Text;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Track : JsonBasedObject, ITrack {

  public required IReadOnlyList<INameCredit> ArtistCredit { get; init; }

  public required Guid Id { get; init; }

  public TimeSpan? Length { get; init; }

  public string? Number { get; init; }

  public int? Position { get; init; }

  public IRecording? Recording { get; init; }

  public required string Title { get; init; }

  public override string ToString() {
    var text = new StringBuilder();
    if (this.Number is not null) {
      text.Append(this.Number).Append(". ");
    }
    foreach (var nc in this.ArtistCredit) {
      text.Append(nc);
    }
    if (this.ArtistCredit.Count > 0) {
      text.Append(" / ");
    }
    text.Append(this.Title);
    if (this.Length is not null) {
      text.Append(" (").Append(this.Length.Value.ToString("g")).Append(')');
    }
    return text.ToString();
  }

}
