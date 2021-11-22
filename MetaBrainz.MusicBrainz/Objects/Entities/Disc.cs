using System;
using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities; 

internal sealed class Disc : JsonBasedObject, IDisc {

  public Disc(string id, IReadOnlyList<int> offsets, int sectors) {
    this.Id = id;
    this.Offsets = offsets;
    this.Sectors = sectors;
  }

  public string Id { get; }

  public IReadOnlyList<int> Offsets { get; }

  public IReadOnlyList<IRelease>? Releases { get; set; }

  public int Sectors { get; }

  public override string ToString() {
    var duration = TimeSpan.FromSeconds(this.Sectors / 75.0);
    return $"{this.Id} ({this.Offsets.Count} track(s), {duration:g})";
  }

}