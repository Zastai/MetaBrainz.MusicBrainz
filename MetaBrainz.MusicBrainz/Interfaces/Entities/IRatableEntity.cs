﻿using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities;

/// <summary>A entity that can be rated.</summary>
[PublicAPI]
public interface IRatableEntity : IEntity {

  /// <summary>The rating for the entity.</summary>
  IRating? Rating { get; }

  /// <summary>The rating given to the entity by the authenticated user.</summary>
  IRating? UserRating { get; }

}
