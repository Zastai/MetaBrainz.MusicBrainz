using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A compact disc.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface IDisc {

    /// <summary>The MusicBrainz disc ID for this disc.</summary>
    string Id { get; }

    /// <summary>The number of offsets set for this disc.</summary>
    int OffsetCount { get; }

    /// <summary>The offsets (Red Book sector addresses) for this disc.</summary>
    IReadOnlyList<int> Offsets { get; }

    /// <summary>The releases that include this disc.</summary>
    IReadOnlyList<IRelease> Releases { get; }

    /// <summary>The total size of this disc, in Red Book sectors.</summary>
    int Sectors { get; }

  }

}
