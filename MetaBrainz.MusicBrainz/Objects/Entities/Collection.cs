using System;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Collection : Entity, ICollection {

  public Collection(Guid id, EntityType contentType, int itemCount) : base(EntityType.Collection, id) {
    this.ContentType = contentType;
    this.ItemCount = itemCount;
  }

  public string? Editor { get; init; }

  public EntityType ContentType { get; }

  public string? Name { get; init; }

  public string? Type { get; init; }

  public Guid? TypeId { get; init; }

  public int ItemCount { get; }

  public override string ToString() => $"{this.Name} ({this.Type}) ({this.ItemCount} item(s))";

}
