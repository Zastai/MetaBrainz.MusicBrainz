using System;

using JetBrains.Annotations;

using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities;

/// <summary>An annotation on a MusicBrainz entity.</summary>
[PublicAPI]
public interface IAnnotation : IJsonBasedObject {

  /// <summary>The MBID of the entity the annotation is attached to.</summary>
  Guid? Entity { get; }

  /// <summary>The type of entity the annotation is attached to.</summary>
  EntityType? Type { get; }

  /// <summary>The name of the entity the annotation is attached to.</summary>
  string Name { get; }

  /// <summary>The annotation's text.</summary>
  string Text { get; }

}
