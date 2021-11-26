using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects;

internal sealed class DiscIdLookupResult : JsonBasedObject, IDiscIdLookupResult {

  public IDisc? Disc { get; set; }

  public IReadOnlyList<IRelease>? Releases { get; set; }

  public ICdStub? Stub { get; set; }

  /// <summary>Gets the textual representation of the disc ID lookup result.</summary>
  /// <returns>A string describing the lookup results.</returns>
  public override string ToString() {
    if (this.Disc is not null) {
      return "Disc: " + this.Disc;
    }
    if (this.Stub is not null) {
      return "CD Stub: " + this.Stub;
    }
    if (this.Releases is not null) {
      return $"{this.Releases.Count} Release(s)";
    }
    return string.Empty; // should be impossible
  }

}
