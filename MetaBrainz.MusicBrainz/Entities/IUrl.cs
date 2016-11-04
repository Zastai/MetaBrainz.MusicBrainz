using System;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A MusicBrainz URL.</summary>
  public interface IUrl : IEntity, IRelatableEntity {

    /// <summary>The URL's resource location.</summary>
    Uri Resource { get; }

  }

}
