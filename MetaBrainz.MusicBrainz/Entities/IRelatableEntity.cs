using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A MusicBrainz entity that can be related to other MusicBrainz entities.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public interface IRelatableEntity : IEntity {

    /// <summary>The relationships associated with this entity.</summary>
    IEnumerable<IRelationship> Relationships { get; }

  }

}
