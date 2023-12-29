using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Artist : Entity, IArtist {

  public Artist(Guid id) : base(EntityType.Artist, id) {
  }

  public IReadOnlyList<IAlias>? Aliases { get; init; }

  public string? Annotation { get; init; }

  public IArea? Area { get; init; }

  public IArea? BeginArea { get; init; }

  public string? Country { get; init; }

  public string? Disambiguation { get; init; }

  public IArea? EndArea { get; init; }

  public string? Gender { get; init; }

  public Guid? GenderId { get; init; }

  public IReadOnlyList<IGenre>? Genres { get; init; }

  public IReadOnlyList<string>? Ipis { get; init; }

  public IReadOnlyList<string>? Isnis { get; init; }

  public ILifeSpan? LifeSpan { get; init; }

  public string? Name { get; init; }

  public IRating? Rating { get; init; }

  public IReadOnlyList<IRecording>? Recordings { get; init; }

  public IReadOnlyList<IRelationship>? Relationships { get; init; }

  public IReadOnlyList<IReleaseGroup>? ReleaseGroups { get; init; }

  public IReadOnlyList<IRelease>? Releases { get; init; }

  public string? SortName { get; init; }

  public IReadOnlyList<ITag>? Tags { get; init; }

  public string? Type { get; init; }

  public Guid? TypeId { get; init; }

  public IReadOnlyList<IGenre>? UserGenres { get; init; }

  public IRating? UserRating { get; init; }

  public IReadOnlyList<ITag>? UserTags { get; init; }

  public IReadOnlyList<IWork>? Works { get; init; }

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
