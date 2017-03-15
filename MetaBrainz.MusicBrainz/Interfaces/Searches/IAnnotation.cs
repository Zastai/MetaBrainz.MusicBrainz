using System;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Searches {

  /// <summary>An annotation on a MusicBrainz entity.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface IAnnotation {

    /// <summary>The MBID of the entity the annotation is attached to.</summary>
    Guid Entity { get; }

    /// <summary>The type of entity the annotation is attached to.</summary>
    string Type { get; }

    /// <summary>The name of the entity the annotation is attached to.</summary>
    string Name { get; }

    /// <summary>The annotation's text.</summary>
    string Text { get; }

  }

}
