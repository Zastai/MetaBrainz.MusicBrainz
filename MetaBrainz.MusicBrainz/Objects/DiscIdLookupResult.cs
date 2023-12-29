using System.Collections.Generic;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects;

internal sealed class DiscIdLookupResult : JsonBasedObject, IDiscIdLookupResult {

  public DiscIdLookupResult() {
  }

  public DiscIdLookupResult(IDisc? disc) {
    this.Disc = disc;
  }

  public DiscIdLookupResult(IReadOnlyList<IRelease>? releases) {
    this.Releases = releases;
  }

  public DiscIdLookupResult(ICdStub? stub) {
    this.Stub = stub;
  }

  public IDisc? Disc { get; }

  public IReadOnlyList<IRelease>? Releases { get; }

  public ICdStub? Stub { get; }

  /// <summary>Gets the textual representation of the disc ID lookup result.</summary>
  /// <returns>A string describing the lookup results.</returns>
  public override string ToString() {
    if (this.Disc is not null) {
      return $"Disc: {this.Disc}";
    }
    if (this.Stub is not null) {
      return $"CD Stub: {this.Stub}";
    }
    if (this.Releases is not null) {
      return $"{this.Releases.Count} Release(s)";
    }
    return "Result Data Not Recognized";
  }

}
