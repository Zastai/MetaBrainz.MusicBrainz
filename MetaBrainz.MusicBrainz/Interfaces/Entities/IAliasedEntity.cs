using System.Collections.Generic;

using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities; 

/// <summary>An entity with aliases.</summary>
[PublicAPI]
public interface IAliasedEntity : IEntity {

  /// <summary>The aliases for this entity.</summary>
  IReadOnlyList<IAlias>? Aliases { get; }

}