using System;
using System.Collections.Generic;
using System.Text;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Area() : Entity(EntityType.Area), IArea {

  public required IReadOnlyList<IAlias> Aliases { get; init; }

  public required string Annotation { get; init; }

  public required string Disambiguation { get; init; }

  public required IReadOnlyList<IGenre> Genres { get; init; }

  public required IReadOnlyList<string> Iso31661Codes { get; init; }

  public required IReadOnlyList<string> Iso31662Codes { get; init; }

  public required IReadOnlyList<string> Iso31663Codes { get; init; }

  public ILifeSpan? LifeSpan { get; init; }

  public required string Name { get; init; }

  public required IReadOnlyList<IRelationship> Relationships { get; init; }

  public string? SortName { get; init; }

  public required IReadOnlyList<ITag> Tags { get; init; }

  public string? Type { get; init; }

  public Guid? TypeId { get; init; }

  public required IReadOnlyList<IGenre> UserGenres { get; init; }

  public required IReadOnlyList<ITag> UserTags { get; init; }

  public override string ToString() {
    var text = new StringBuilder();
    text.Append(this.Name);
    if (this.Disambiguation is not "") {
      text.Append(" (").Append(this.Disambiguation).Append(')');
    }
    if (this.Type is not null) {
      text.Append(" (").Append(this.Type).Append(')');
    }
    return text.ToString();
  }

}
