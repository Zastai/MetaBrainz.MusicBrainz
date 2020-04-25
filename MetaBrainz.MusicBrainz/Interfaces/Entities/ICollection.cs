using JetBrains.Annotations;

using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A collection of MusicBrainz entities.</summary>
  [PublicAPI]
  public interface ICollection : ITypedEntity {

    /// <summary>The name of the editor who created the collection.</summary>
    string? Editor { get; }

    /// <summary>The type of entity stored in the collection.</summary>
    /// <remarks>
    /// If this is set to <see cref="EntityType.Unknown"/>, the actual entity type string will be stored in
    /// <see cref="IJsonBasedObject.UnhandledProperties"/> with key "entity-type".
    /// </remarks>
    EntityType ContentType { get; }

    /// <summary>The number of items in the collection.</summary>
    int ItemCount { get; }

    /// <summary>The name of the collection.</summary>
    string? Name { get; }

  }

}
