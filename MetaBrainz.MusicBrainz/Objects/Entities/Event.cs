using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Event : Entity, IEvent {

  public Event(Guid id) : base(EntityType.Event, id) {
  }

  public IReadOnlyList<IAlias>? Aliases { get; set; }

  public string? Annotation { get; set; }

  public bool Cancelled { get; set; }

  public string? Disambiguation { get; set; }

  public IReadOnlyList<IGenre>? Genres { get; set; }

  public ILifeSpan? LifeSpan { get; set; }

  public string? Name { get; set; }

  public IRating? Rating { get; set; }

  public IReadOnlyList<IRelationship>? Relationships { get; set; }

  public string? Setlist { get; set; }

  public IReadOnlyList<ITag>? Tags { get; set; }

  public string? Time { get; set; }

  public string? Type { get; set; }

  public Guid? TypeId { get; set; }

  public IReadOnlyList<IGenre>? UserGenres { get; set; }

  public IRating? UserRating { get; set; }

  public IReadOnlyList<ITag>? UserTags { get; set; }

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
