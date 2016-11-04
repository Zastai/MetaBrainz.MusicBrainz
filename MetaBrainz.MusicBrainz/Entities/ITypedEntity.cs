using System;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A typed MusicBrainz entity.</summary>
  public interface ITypedEntity : IEntity {

    /// <summary>The type of the entity, if applicable, expressed as text.</summary>
    string Type { get; }

    /// <summary>The type of the entity, if applicable, expressed as an MBID.</summary>
    Guid? TypeId { get; }

  }

}
