using System;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  internal abstract class Entity : SearchResult, IEntity {

    protected Entity(EntityType type, Guid id) {
      this.EntityType = type;
      this.Id = id;
    }

    public EntityType EntityType { get; }

    public Guid Id { get; }

  }

}
