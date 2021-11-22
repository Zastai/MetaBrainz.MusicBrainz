using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities; 

internal sealed class Label : Entity, ILabel {

  public Label(Guid id) : base(EntityType.Label, id) {
  }

  public IReadOnlyList<IAlias>? Aliases { get; set; }

  public string? Annotation { get; set; }

  public IArea? Area { get; set; }

  public string? Country { get; set; }

  public string? Disambiguation { get; set; }

  public IReadOnlyList<IGenre>? Genres { get; set; }

  public IReadOnlyList<string>? Ipis { get; set; }

  public IReadOnlyList<string>? Isnis { get; set; }

  public int? LabelCode { get; set; }

  public ILifeSpan? LifeSpan { get; set; }

  public string? Name { get; set; }

  public IRating? Rating { get; set; }

  public IReadOnlyList<IRelationship>? Relationships { get; set; }

  public IReadOnlyList<IRelease>? Releases { get; set; }

  public string? SortName { get; set; }

  public IReadOnlyList<ITag>? Tags { get; set; }

  public string? Type { get; set; }

  public Guid? TypeId { get; set; }

  public IReadOnlyList<IGenre>? UserGenres { get; set; }

  public IRating? UserRating { get; set; }

  public IReadOnlyList<ITag>? UserTags { get; set; }

  public override string ToString() {
    var text = this.Name ?? string.Empty;
    if (!string.IsNullOrEmpty(this.Disambiguation)) {
      text += " (" + this.Disambiguation + ")";
    }
    if (this.Type != null) {
      text += " (" + this.Type + ")";
    }
    return text;
  }

}