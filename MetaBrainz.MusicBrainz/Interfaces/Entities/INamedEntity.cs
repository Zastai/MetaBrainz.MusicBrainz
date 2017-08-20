using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>An entity with a name.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface INamedEntity {

    /// <summary>The aliases for this entity.</summary>
    IReadOnlyList<IAlias> Aliases { get; }

    /// <summary>The text used to distinguish this entity from others with the same name.</summary>
    string Disambiguation { get; }

    /// <summary>The entity's name.</summary>
    string Name { get; }

  }

}
