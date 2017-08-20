using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A MusicBrainz entity that can be related to other MusicBrainz entities.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public interface IRelatableEntity : IEntity {

    /// <summary>The relationships associated with this entity.</summary>
    IReadOnlyList<IRelationship> Relationships { get; }

  }

}
