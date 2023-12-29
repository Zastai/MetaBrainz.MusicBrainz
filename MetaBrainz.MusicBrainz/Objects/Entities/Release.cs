using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Release : Entity, IRelease {

  public Release(Guid id) : base(EntityType.Release, id) {
  }

  public IReadOnlyList<IAlias>? Aliases { get; init; }

  public string? Annotation { get; init; }

  public IReadOnlyList<INameCredit>? ArtistCredit { get; init; }

  public string? Asin { get; init; }

  public string? Barcode { get; init; }

  public IReadOnlyList<ICollection>? Collections { get; init; }

  public string? Country { get; init; }

  public ICoverArtArchive? CoverArtArchive { get; init; }

  public PartialDate? Date { get; init; }

  public string? Disambiguation { get; init; }

  public IReadOnlyList<IGenre>? Genres { get; init; }

  public IReadOnlyList<ILabelInfo>? LabelInfo { get; init; }

  public IReadOnlyList<IMedium>? Media { get; init; }

  public string? Packaging { get; init; }

  public Guid? PackagingId { get; init; }

  public string? Quality { get; init; }

  public IReadOnlyList<IRelationship>? Relationships { get; init; }

  public IReadOnlyList<IReleaseEvent>? ReleaseEvents { get; init; }

  public IReleaseGroup? ReleaseGroup { get; init; }

  public string? Status { get; init; }

  public Guid? StatusId { get; init; }

  public IReadOnlyList<ITag>? Tags { get; init; }

  public ITextRepresentation? TextRepresentation { get; init; }

  public string? Title { get; init; }

  public IReadOnlyList<IGenre>? UserGenres { get; init; }

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
    return text;
  }

}
