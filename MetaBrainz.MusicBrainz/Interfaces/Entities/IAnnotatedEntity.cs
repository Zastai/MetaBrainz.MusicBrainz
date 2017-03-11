using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>An entity that can have an associated annotation.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public interface IAnnotatedEntity {

    /// <summary>The annotation for this entity.</summary>
    string Annotation { get; }

  }

}
