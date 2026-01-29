using System;
using System.Collections.Generic;
using System.Text;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Artist() : Entity(EntityType.Artist), IArtist {

  public required IReadOnlyList<IAlias> Aliases { get; init; }

  public required string Annotation { get; init; }

  public IArea? Area { get; init; }

  public IArea? BeginArea { get; init; }

  public string? Country { get; init; }

  public required string Disambiguation { get; init; }

  public IArea? EndArea { get; init; }

  public string? Gender { get; init; }

  public Guid? GenderId { get; init; }

  public required IReadOnlyList<IGenre> Genres { get; init; }

  public required IReadOnlyList<string> Ipis { get; init; }

  public required IReadOnlyList<string> Isnis { get; init; }

  public ILifeSpan? LifeSpan { get; init; }

  public required string Name { get; init; }

  public IRating? Rating { get; init; }

  public required IReadOnlyList<IRecording> Recordings { get; init; }

  public required IReadOnlyList<IRelationship> Relationships { get; init; }

  public required IReadOnlyList<IReleaseGroup> ReleaseGroups { get; init; }

  public required IReadOnlyList<IRelease> Releases { get; init; }

  public string? SortName { get; init; }

  public required IReadOnlyList<ITag> Tags { get; init; }

  public string? Type { get; init; }

  public Guid? TypeId { get; init; }

  public required IReadOnlyList<IGenre> UserGenres { get; init; }

  public IRating? UserRating { get; init; }

  public required IReadOnlyList<ITag> UserTags { get; init; }

  public required IReadOnlyList<IWork> Works { get; init; }

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
