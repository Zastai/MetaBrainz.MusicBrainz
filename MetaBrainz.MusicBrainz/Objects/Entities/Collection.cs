using System;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Collection() : Entity(EntityType.Collection), ICollection {

  public string? Editor { get; init; }

  public required EntityType ContentType { get; init; }

  public string? Name { get; init; }

  public string? Type { get; init; }

  public Guid? TypeId { get; init; }

  public required int ItemCount { get; init; }

  public override string ToString() => $"{this.Name} ({this.Type}) ({this.ItemCount} item(s))";

}
