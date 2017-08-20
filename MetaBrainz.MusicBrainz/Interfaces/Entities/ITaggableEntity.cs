using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A entity that can have tags applied to it.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public interface ITaggableEntity : IEntity {

    /// <summary>The tags associated with this entity.</summary>
    IReadOnlyList<ITag> Tags { get; }

    /// <summary>The tags set on this entity by the authenticated user.</summary>
    IReadOnlyList<IUserTag> UserTags { get; }

  }

}
