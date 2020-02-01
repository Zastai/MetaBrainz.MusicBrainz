using System;
using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A MusicBrainz URL.</summary>
  [PublicAPI]
  public interface IUrl : IRelatableEntity {

    /// <summary>The URL's resource location.</summary>
    Uri Resource { get; }

  }

}
