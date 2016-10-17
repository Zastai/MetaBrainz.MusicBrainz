using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A collection.</summary>
  public interface ICollection : IMbEntity, ITypedEntity {

    /// <summary>The name of the editor who created the collection.</summary>
    string Editor { get; }

    /// <summary>The type of entity stored in the collection.</summary>
    string EntityType { get; }

    /// <summary>The number of items in in the collection.</summary>
    int ItemCount { get; }

    /// <summary>The name of the collection.</summary>
    string Name { get; }

  }

}
