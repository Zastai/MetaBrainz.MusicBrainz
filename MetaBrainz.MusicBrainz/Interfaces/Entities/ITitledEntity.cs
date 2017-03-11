using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  #if NETFX_LT_4_5
  using AliasList = IEnumerable<IAlias>;
  #else
  using AliasList = IReadOnlyList<IAlias>;
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
