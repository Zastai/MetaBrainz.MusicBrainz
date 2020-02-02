using System.Collections.Generic;
using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A compact disc.</summary>
  [PublicAPI]
  public interface IDisc : IJsonBasedObject {

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
