using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>An entity with a title.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface ITitledEntity {

    /// <summary>The aliases for this entity.</summary>
    IReadOnlyList<IAlias> Aliases { get; }

    /// <summary>The text used to distinguish this entity from others with the same title.</summary>
    string Disambiguation { get; }

    /// <summary>The entity's title.</summary>
    string Title { get; }

  }

}
