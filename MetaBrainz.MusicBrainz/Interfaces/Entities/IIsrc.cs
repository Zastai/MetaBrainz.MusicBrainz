using System.Collections.Generic;
using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>Information associated with an ISRC (International Standard Recording Code).</summary>
  [PublicAPI]
  public interface IIsrc : IJsonBasedObject {

    /// <summary>The recordings that have this ISRC assigned to them.</summary>
    IReadOnlyList<IRecording>? Recordings { get; }

    /// <summary>The ISRC value.</summary>
    string? Value { get; }

  }

}
