using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Work : Entity, IWork {

  public Work(Guid id) : base(EntityType.Work, id) {
  }

  public IReadOnlyList<IAlias>? Aliases { get; set; }

  public string? Annotation { get; set; }

  public IReadOnlyList<IWorkAttribute>? Attributes { get; set; }

  public string? Disambiguation { get; set; }

  public IReadOnlyList<IGenre>? Genres { get; set; }

  public IReadOnlyList<string>? Iswcs { get; set; }

  public string? Language { get; set; }

  public IReadOnlyList<string>? Languages { get; set; }

  public IRating? Rating { get; set; }

  public IReadOnlyList<IRelationship>? Relationships { get; set; }

  public IReadOnlyList<ITag>? Tags { get; set; }

  public string? Title { get; set; }

  public string? Type { get; set; }

  public Guid? TypeId { get; set; }

  public IReadOnlyList<IGenre>? UserGenres { get; set; }

  public IRating? UserRating { get; set; }

  public IReadOnlyList<ITag>? UserTags { get; set; }

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
