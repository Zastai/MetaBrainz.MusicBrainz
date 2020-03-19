using System;

using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A typed MusicBrainz entity.</summary>
  [PublicAPI]
  public interface ITypedEntity : IEntity {

    /// <summary>The type of the entity, if applicable, expressed as text.</summary>
    string? Type { get; }

    /// <summary>The type of the entity, if applicable, expressed as an MBID.</summary>
    Guid? TypeId { get; }

  }

}
