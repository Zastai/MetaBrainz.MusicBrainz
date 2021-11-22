using System;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities; 

internal sealed class Collection : Entity, ICollection {

  public Collection(Guid id, EntityType contentType, int itemCount) : base(EntityType.Collection, id) {
    this.ContentType = contentType;
    this.ItemCount = itemCount;
  }

  public string? Editor { get; set; }

  public EntityType ContentType { get; }

  public string? Name { get; set; }

  public string? Type { get; set; }

  public Guid? TypeId { get; set; }

  public int ItemCount { get; }

  public override string ToString() => $"{this.Name} ({this.Type}) ({this.ItemCount} item(s))";

}