using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Entities {

  #if NETFX_LT_4_5
  using RelationshipList = IEnumerable<IRelationship>;
  #else
  using RelationshipList = IReadOnlyList<IRelationship>;
  #endif

  /// <summary>A MusicBrainz entity that can be related to other MusicBrainz entities.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public interface IRelatableEntity : IEntity {

    /// <summary>The relationships associated with this entity.</summary>
    RelationshipList Relationships { get; }

  }

}
