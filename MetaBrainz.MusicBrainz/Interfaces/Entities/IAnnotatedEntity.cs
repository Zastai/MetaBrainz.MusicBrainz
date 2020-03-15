using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>An entity that can have an associated annotation.</summary>
  [PublicAPI]
  public interface IAnnotatedEntity : IEntity {

    /// <summary>The annotation for this entity.</summary>
    string? Annotation { get; }

  }

}
