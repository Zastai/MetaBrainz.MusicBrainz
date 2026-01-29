using System;
using System.Collections.Generic;
using System.Text;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Recording() : Entity(EntityType.Recording), IRecording {

  public required IReadOnlyList<IAlias> Aliases { get; init; }

  public required string Annotation { get; init; }

  public required IReadOnlyList<INameCredit> ArtistCredit { get; init; }

  public required string Disambiguation { get; init; }

  public PartialDate? FirstReleaseDate { get; init; }

  public required IReadOnlyList<IGenre> Genres { get; init; }

  public required IReadOnlyList<string> Isrcs { get; init; }

  public TimeSpan? Length { get; init; }

  public IRating? Rating { get; init; }

  public required IReadOnlyList<IRelationship> Relationships { get; init; }

  public required IReadOnlyList<IRelease> Releases { get; init; }

  public required IReadOnlyList<ITag> Tags { get; init; }

  public required string Title { get; init; }

  public required IReadOnlyList<IGenre> UserGenres { get; init; }

  public IRating? UserRating { get; init; }

  public required IReadOnlyList<ITag> UserTags { get; init; }

  public bool Video { get; init; }

  public override string ToString() {
    var text = new StringBuilder();
    foreach (var nc in this.ArtistCredit) {
      text.Append(nc);
    }
    if (text.Length > 0) {
      text.Append(" / ");
    }
    text.Append(this.Title);
    if (this.Disambiguation is not "") {
      text.Append(" (").Append(this.Disambiguation).Append(')');
    }
    if (this.Length is not null) {
      text.Append(" (").Append(this.Length.Value.ToString("g")).Append(')');
    }
    return text.ToString();
  }

}
