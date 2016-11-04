using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>Information associated with an ISRC (International Standard Recording Code).</summary>
  public interface IIsrc {

    /// <summary>The recordings that have this ISRC assigned to them.</summary>
    IEnumerable<IRecording> Recordings { get; }

    /// <summary>The ISRC value.</summary>
    string Value { get; }

  }

}
