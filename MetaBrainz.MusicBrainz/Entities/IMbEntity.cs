using System;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>An item identified by an MBID.</summary>
  public interface IMbEntity : IEntity {

    /// <summary>The type of this entity.</summary>
    EntityType EntityType { get; }

    /// <summary>The MBID that identifies this entity.</summary>
    Guid MbId { get; }

  }

}
