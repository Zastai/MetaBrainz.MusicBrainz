using System;
using System.Collections.Generic;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
internal sealed class ReleaseGroup : Entity, IReleaseGroup {

  public ReleaseGroup(Guid id) : base(EntityType.ReleaseGroup, id) {
  }

  public IReadOnlyList<IAlias>? Aliases { get; init; }

  public string? Annotation { get; init; }

  public IReadOnlyList<INameCredit>? ArtistCredit { get; init; }

  public string? Disambiguation { get; init; }

  public PartialDate? FirstReleaseDate { get; init; }

  public IReadOnlyList<IGenre>? Genres { get; init; }

  public string? PrimaryType { get; init; }

  public Guid? PrimaryTypeId { get; init; }

  public IRating? Rating { get; init; }

  public IReadOnlyList<IRelationship>? Relationships { get; init; }

  public IReadOnlyList<IRelease>? Releases { get; init; }

  public IReadOnlyList<string>? SecondaryTypes { get; init; }

  public IReadOnlyList<Guid>? SecondaryTypeIds { get; init; }

  public IReadOnlyList<ITag>? Tags { get; init; }

  public string? Title { get; init; }

  public IReadOnlyList<IGenre>? UserGenres { get; init; }

  public IRating? UserRating { get; init; }

  public IReadOnlyList<ITag>? UserTags { get; init; }

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
    if (!string.IsNullOrEmpty(this.PrimaryType)) {
      text += $" ({this.PrimaryType})";
    }
    return text;
  }

}
