using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Place : Entity, IPlace {

  public Place(Guid id) : base(EntityType.Place, id) {
  }

  public string? Address { get; init; }

  public IReadOnlyList<IAlias>? Aliases { get; init; }

  public string? Annotation { get; init; }

  public IArea? Area { get; init; }

  public ICoordinates? Coordinates { get; init; }

  public string? Disambiguation { get; init; }

  public IReadOnlyList<IGenre>? Genres { get; init; }

  public ILifeSpan? LifeSpan { get; init; }

  public string? Name { get; init; }

  public IReadOnlyList<IRelationship>? Relationships { get; init; }

  public IReadOnlyList<ITag>? Tags { get; init; }

  public string? Type { get; init; }

  public Guid? TypeId { get; init; }

  public IReadOnlyList<IGenre>? UserGenres { get; init; }

  public IReadOnlyList<ITag>? UserTags { get; init; }

  public override string ToString() {
    var text = this.Name ?? string.Empty;
    if (!string.IsNullOrEmpty(this.Disambiguation)) {
      text += $" ({this.Disambiguation})";
    }
    if (this.Type is not null) {
      text += $" ({this.Type})";
    }
    return text;
  }

}
