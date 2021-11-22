﻿using JetBrains.Annotations;

using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities; 

/// <summary>Information about an entity's rating.</summary>
[PublicAPI]
public interface IRating : IJsonBasedObject {

  /// <summary>The rating value, if any, expressed as a fractional number of stars (0-5).</summary>
  decimal? Value { get; }

  /// <summary>The number of ratings submitted for the entity, if applicable.</summary>
  int? VoteCount { get; }

}