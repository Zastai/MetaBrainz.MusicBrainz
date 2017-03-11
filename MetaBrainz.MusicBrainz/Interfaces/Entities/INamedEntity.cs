using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Entities {

  #if NETFX_LT_4_5
  using AliasList = IEnumerable<IAlias>;
  #else
  using AliasList = IReadOnlyList<IAlias>;
  #endif

  /// <summary>An entity with a name.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface INamedEntity {

    /// <summary>The aliases for this entity.</summary>
    AliasList Aliases { get; }

    /// <summary>The text used to distinguish this entity from others with the same name.</summary>
    string Disambiguation { get; }

    /// <summary>The entity's name.</summary>
    string Name { get; }

  }

}
