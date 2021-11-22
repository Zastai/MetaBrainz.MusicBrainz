using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities; 

/// <summary>An entity with a title.</summary>
[PublicAPI]
public interface ITitledEntity : IEntity {

  /// <summary>The text used to distinguish this entity from others with the same title.</summary>
  string? Disambiguation { get; }

  /// <summary>The entity's title.</summary>
  string? Title { get; }

}