using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  #if NETFX_LT_4_5
  using RecordingList = IEnumerable<IRecording>;
  #else
  using RecordingList = IReadOnlyList<IRecording>;
  #endif

  /// <summary>Information associated with an ISRC (International Standard Recording Code).</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface IIsrc {

    /// <summary>The recordings that have this ISRC assigned to them.</summary>
    RecordingList Recordings { get; }

    /// <summary>The ISRC value.</summary>
    string Value { get; }

  }

}
