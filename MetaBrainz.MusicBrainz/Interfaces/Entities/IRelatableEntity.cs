using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  #if NETFX_GE_4_5
  using RelationshipList = IReadOnlyList<IRelationship>;
  #else
  using RelationshipList = IEnumerable<IRelationship>;
  #endif

  /// <summary>A MusicBrainz entity that can be related to other MusicBrainz entities.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public interface IRelatableEntity : IEntity {

    /// <summary>The relationships associated with this entity.</summary>
    RelationshipList Relationships { get; }

  }

}
