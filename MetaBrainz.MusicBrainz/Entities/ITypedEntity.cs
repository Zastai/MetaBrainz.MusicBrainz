using System;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A typed entity.</summary>
  public interface ITypedEntity {

    /// <summary>The type of the entity, expressed as text.</summary>
    string Type { get; }

    /// <summary>The type of the entity, expressed as an MBID.</summary>
    Guid? TypeId { get; }

  }

}
