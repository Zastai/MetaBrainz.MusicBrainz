using System;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class SimpleTrack : JsonBasedObject, ISimpleTrack {

  public SimpleTrack(string title, TimeSpan length) {
    this.Title = title;
    this.Length = length;
  }

  public string? Artist { get; set; }

  public TimeSpan Length { get; }

  public string Title { get; }

  public override string ToString() {
    var text = string.Empty;
    if (this.Artist is not null) {
      text += this.Artist + " / ";
    }
    text += $"{this.Title} ({this.Length:g})";
    return text;
  }

}
