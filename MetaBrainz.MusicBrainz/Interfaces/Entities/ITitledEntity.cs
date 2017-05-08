using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  #if NETFX_GE_4_5
  using AliasList = IReadOnlyList<IAlias>;
  #else
  using AliasList = IEnumerable<IAlias>;
  #endif

  /// <summary>An entity with a title.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface ITitledEntity {

    /// <summary>The aliases for this entity.</summary>
    AliasList Aliases { get; }

    /// <summary>The text used to distinguish this entity from others with the same title.</summary>
    string Disambiguation { get; }

    /// <summary>The entity's title.</summary>
    string Title { get; }

  }

}
