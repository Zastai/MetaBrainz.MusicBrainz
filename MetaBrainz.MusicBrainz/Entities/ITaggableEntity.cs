using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A entity that can have tags applied to it.</summary>
  public interface ITaggableEntity : IMbEntity {

    /// <summary>The tags associated with this entity.</summary>
    IEnumerable<ITag> Tags { get; }

    /// <summary>The tags set on this entity by the authenticated user.</summary>
    IEnumerable<IUserTag> UserTags { get; }

  }

}
