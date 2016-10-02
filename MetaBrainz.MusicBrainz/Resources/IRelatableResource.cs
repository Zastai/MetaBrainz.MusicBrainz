using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>A resource that is related to other resources.</summary>
  public interface IRelatableResource : IResource {

    /// <summary>The lists of resources related to this one.</summary>
    IEnumerable<IRelationList> RelationList { get; }

  }

}
