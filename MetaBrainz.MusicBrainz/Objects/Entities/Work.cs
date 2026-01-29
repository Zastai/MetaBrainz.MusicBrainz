using System;
using System.Collections.Generic;
using System.Text;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Work() : Entity(EntityType.Work), IWork {

  public required IReadOnlyList<IAlias> Aliases { get; init; }

  public required string Annotation { get; init; }

  public required IReadOnlyList<IWorkAttribute> Attributes { get; init; }

  public required string Disambiguation { get; init; }

  public required IReadOnlyList<IGenre> Genres { get; init; }

  public required IReadOnlyList<string> Iswcs { get; init; }

  public string? Language { get; init; }

  public required IReadOnlyList<string> Languages { get; init; }

  public IRating? Rating { get; init; }

  public required IReadOnlyList<IRelationship> Relationships { get; init; }

  public required IReadOnlyList<ITag> Tags { get; init; }

  public required string Title { get; init; }

  public string? Type { get; init; }

  public Guid? TypeId { get; init; }

  public required IReadOnlyList<IGenre> UserGenres { get; init; }

  public IRating? UserRating { get; init; }

  public required IReadOnlyList<ITag> UserTags { get; init; }

  public override string ToString() {
    var text = new StringBuilder();
    text.Append(this.Title);
    if (this.Disambiguation is not "") {
      text.Append(" (").Append(this.Disambiguation).Append(')');
    }
    if (this.Type is not null) {
      text.Append(" (").Append(this.Type).Append(')');
    }
    return text.ToString();
  }

}
