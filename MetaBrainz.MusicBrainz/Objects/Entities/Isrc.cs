using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Isrc : JsonBasedObject, IIsrc {

  public Isrc(string value, IReadOnlyList<IRecording> recordings) {
    this.Recordings = recordings;
    this.Value = value;
  }

  public IReadOnlyList<IRecording> Recordings { get; }

  public string Value { get; }

  public override string ToString() => this.Value;

}
