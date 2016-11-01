using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A compact disc.</summary>
  public interface IDisc {

    /// <summary>The Musicbrainz disc ID for this disc.</summary>
    string Id { get; }

    /// <summary>The number of offsets set for this disc.</summary>
    int OffsetCount { get; }

    /// <summary>The offsets (Red Book sector addresses) for this disc.</summary>
    IEnumerable<int> Offsets { get; }

    /// <summary>The releases that include this disc.</summary>
    IEnumerable<IRelease> Releases { get; }

    /// <summary>The total size of this disc, in Red Book sectors.</summary>
    int Sectors { get; }

  }

}
