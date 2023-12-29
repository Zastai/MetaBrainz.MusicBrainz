using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Work : Entity, IWork {

  public Work(Guid id) : base(EntityType.Work, id) {
  }

  public IReadOnlyList<IAlias>? Aliases { get; init; }

  public string? Annotation { get; init; }

  public IReadOnlyList<IWorkAttribute>? Attributes { get; init; }

  public string? Disambiguation { get; init; }

  public IReadOnlyList<IGenre>? Genres { get; init; }

  public IReadOnlyList<string>? Iswcs { get; init; }

  public string? Language { get; init; }

  public IReadOnlyList<string>? Languages { get; init; }

  public IRating? Rating { get; init; }

  public IReadOnlyList<IRelationship>? Relationships { get; init; }

  public IReadOnlyList<ITag>? Tags { get; init; }

  public string? Title { get; init; }

  public string? Type { get; init; }

  public Guid? TypeId { get; init; }

  public IReadOnlyList<IGenre>? UserGenres { get; init; }

  public IRating? UserRating { get; init; }

  public IReadOnlyList<ITag>? UserTags { get; init; }

  public override string ToString() {
    var text = this.Title ?? string.Empty;
    if (!string.IsNullOrEmpty(this.Disambiguation)) {
      text += $" ({this.Disambiguation})";
    }
    if (this.Type is not null) {
      text += $" ({this.Type})";
    }
    return text;
  }

}
