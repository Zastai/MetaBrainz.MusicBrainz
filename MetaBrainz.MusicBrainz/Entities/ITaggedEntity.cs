using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A tagged entity.</summary>
  public interface ITaggedEntity {

    /// <summary>The tags associated with this entity.</summary>
    IEnumerable<ITag> Tags { get; }

    /// <summary>The tags set on this entity by the authenticated user.</summary>
    IEnumerable<IUserTag> UserTags { get; }

  }

}
