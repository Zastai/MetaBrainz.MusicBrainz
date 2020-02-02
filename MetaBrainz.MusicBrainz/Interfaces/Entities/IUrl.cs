using System;
using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A MusicBrainz URL.</summary>
  [PublicAPI]
  public interface IUrl : IRelatableEntity {

    /// <summary>The resource location for the URL.</summary>
    Uri Resource { get; }

  }

}
