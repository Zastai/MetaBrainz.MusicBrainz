using System;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal abstract class Entity(EntityType type) : JsonBasedObject, IEntity {

  public EntityType EntityType { get; } = type;

  public required Guid Id { get; init; }

}
