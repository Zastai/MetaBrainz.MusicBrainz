using System;
using System.Text;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class SimpleTrack : JsonBasedObject, ISimpleTrack {

  public required string Artist { get; init; }

  public required TimeSpan Length { get; init; }

  public required string Title { get; init; }

  public override string ToString() {
    var text = new StringBuilder();
    if (this.Artist is not "") {
      text.Append(this.Artist).Append(" / ");
    }
    text.Append(this.Title).Append(" (").Append(this.Length.ToString("g")).Append(')');
    return text.ToString();
  }

}
