using System;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>An item identified by an MBID.</summary>
  public interface IMbEntity : IEntity {

    /// <summary>The MBID that identifies this entity.</summary>
    Guid MBID { get; }

  }

}
