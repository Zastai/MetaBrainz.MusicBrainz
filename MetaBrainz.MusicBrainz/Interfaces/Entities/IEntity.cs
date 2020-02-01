using System;

using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>An MusicBrainz entity, identified by an MBID.</summary>
  [PublicAPI]
  public interface IEntity {

    /// <summary>The type of this entity.</summary>
    EntityType EntityType { get; }

    /// <summary>The MBID that identifies this entity.</summary>
    Guid MbId { get; }

  }

}
