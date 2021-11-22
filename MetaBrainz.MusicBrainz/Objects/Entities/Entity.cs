using System;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities; 

internal abstract class Entity : JsonBasedObject, IEntity {

  protected Entity(EntityType type, Guid id) {
    this.EntityType = type;
    this.Id = id;
  }

  public EntityType EntityType { get; }

  public Guid Id { get; }

}