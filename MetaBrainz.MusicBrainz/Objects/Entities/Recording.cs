using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Recording : Entity, IRecording {

  public Recording(Guid id) : base(EntityType.Recording, id) {
  }

  public IReadOnlyList<IAlias>? Aliases { get; init; }

  public string? Annotation { get; init; }

  public IReadOnlyList<INameCredit>? ArtistCredit { get; init; }

  public string? Disambiguation { get; init; }

  public PartialDate? FirstReleaseDate { get; init; }

  public IReadOnlyList<IGenre>? Genres { get; init; }

  public IReadOnlyList<string>? Isrcs { get; init; }

  public TimeSpan? Length { get; init; }

  public IRating? Rating { get; init; }

  public IReadOnlyList<IRelationship>? Relationships { get; init; }

  public IReadOnlyList<IRelease>? Releases { get; init; }

  public IReadOnlyList<ITag>? Tags { get; init; }

  public string? Title { get; init; }

  public IReadOnlyList<IGenre>? UserGenres { get; init; }

  public IRating? UserRating { get; init; }

  public IReadOnlyList<ITag>? UserTags { get; init; }

  public bool Video { get; init; }

  public override string ToString() {
    var text = string.Empty;
    if (this.ArtistCredit is not null) {
      foreach (var nc in this.ArtistCredit) {
        text += nc.ToString();
      }
      text += " / ";
    }
    text += this.Title;
    if (!string.IsNullOrEmpty(this.Disambiguation)) {
      text += $" ({this.Disambiguation})";
    }
    if (this.Length is not null) {
      text += $" ({this.Length.Value:g})";
    }
    return text;
  }

}
