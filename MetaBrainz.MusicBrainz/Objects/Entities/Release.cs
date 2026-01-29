using System;
using System.Collections.Generic;
using System.Text;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Release() : Entity(EntityType.Release), IRelease {

  public required IReadOnlyList<IAlias> Aliases { get; init; }

  public required string Annotation { get; init; }

  public required IReadOnlyList<INameCredit> ArtistCredit { get; init; }

  public required string Asin { get; init; }

  public required string Barcode { get; init; }

  public required IReadOnlyList<ICollection> Collections { get; init; }

  public string? Country { get; init; }

  public ICoverArtArchive? CoverArtArchive { get; init; }

  public PartialDate? Date { get; init; }

  public required string Disambiguation { get; init; }

  public required IReadOnlyList<IGenre> Genres { get; init; }

  public required IReadOnlyList<ILabelInfo> LabelInfo { get; init; }

  public required IReadOnlyList<IMedium> Media { get; init; }

  public string? Packaging { get; init; }

  public Guid? PackagingId { get; init; }

  public string? Quality { get; init; }

  public required IReadOnlyList<IRelationship> Relationships { get; init; }

  public required IReadOnlyList<IReleaseEvent> ReleaseEvents { get; init; }

  public IReleaseGroup? ReleaseGroup { get; init; }

  public string? Status { get; init; }

  public Guid? StatusId { get; init; }

  public required IReadOnlyList<ITag> Tags { get; init; }

  public ITextRepresentation? TextRepresentation { get; init; }

  public required string Title { get; init; }

  public required IReadOnlyList<IGenre> UserGenres { get; init; }

  public required IReadOnlyList<ITag> UserTags { get; init; }

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
    return text.ToString();
  }

}
