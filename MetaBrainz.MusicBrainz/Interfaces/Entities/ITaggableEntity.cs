using System.Collections.Generic;

using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities;

/// <summary>An entity that can have tags applied to it.</summary>
[PublicAPI]
public interface ITaggableEntity : IEntity {

  /// <summary>The genres associated with this entity.</summary>
  IReadOnlyList<IGenre> Genres { get; }

  /// <summary>The tags associated with this entity.</summary>
  IReadOnlyList<ITag> Tags { get; }

  /// <summary>The genres set on this entity by the authenticated user.</summary>
  IReadOnlyList<IGenre> UserGenres { get; }

  /// <summary>The tags set on this entity by the authenticated user.</summary>
  IReadOnlyList<ITag> UserTags { get; }

}
