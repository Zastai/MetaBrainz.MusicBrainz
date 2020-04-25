using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  internal sealed class Place : Entity, IPlace {

    public Place(Guid id) : base(EntityType.Place, id) {
    }

    public string? Address { get; set; }

    public IReadOnlyList<IAlias>? Aliases { get; set; }

    public string? Annotation { get; set; }

    public IArea? Area { get; set; }

    public ICoordinates? Coordinates { get; set; }

    public string? Disambiguation { get; set; }

    public IReadOnlyList<IGenre>? Genres { get; set; }

    public ILifeSpan? LifeSpan { get; set; }

    public string? Name { get; set; }

    public IReadOnlyList<IRelationship>? Relationships { get; set; }

    public IReadOnlyList<ITag>? Tags { get; set; }

    public string? Type { get; set; }

    public Guid? TypeId { get; set; }

    public IReadOnlyList<IGenre>? UserGenres { get; set; }

    public IReadOnlyList<ITag>? UserTags { get; set; }

    public override string ToString() {
      var text = this.Name ?? string.Empty;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += $" ({this.Disambiguation})";
      if (this.Type != null)
        text += $" ({this.Type})";
      return text;
    }

  }

}
