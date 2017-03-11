using System;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A typed MusicBrainz entity.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface ITypedEntity : IEntity {

    /// <summary>The type of the entity, if applicable, expressed as text.</summary>
    string Type { get; }

    /// <summary>The type of the entity, if applicable, expressed as an MBID.</summary>
    Guid? TypeId { get; }

  }

}
