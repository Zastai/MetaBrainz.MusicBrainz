using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>Information associated with an ISRC (International Standard Recording Code).</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface IIsrc {

    /// <summary>The recordings that have this ISRC assigned to them.</summary>
    IReadOnlyList<IRecording> Recordings { get; }

    /// <summary>The ISRC value.</summary>
    string Value { get; }

  }

}
