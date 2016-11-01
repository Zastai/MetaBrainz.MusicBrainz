using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>An entity that is related to other entities.</summary>
  public interface IRelatableEntity {

    /// <summary>The relationships associated with this entity.</summary>
    IEnumerable<IRelationship> Relationships { get; }

  }

}
