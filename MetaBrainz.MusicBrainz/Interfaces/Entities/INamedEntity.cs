using System.Collections.Generic;
using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>An entity with a name.</summary>
  [PublicAPI]
  public interface INamedEntity : IEntity {

    /// <summary>The aliases for this entity.</summary>
    IReadOnlyList<IAlias>? Aliases { get; }

    /// <summary>The text used to distinguish this entity from others with the same name.</summary>
    string? Disambiguation { get; }

    /// <summary>The entity's name.</summary>
    string? Name { get; }

  }

}
