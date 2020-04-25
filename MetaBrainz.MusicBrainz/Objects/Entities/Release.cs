using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  internal sealed class Release : Entity, IRelease {

    public Release(Guid id) : base(EntityType.Release, id) {
    }

    public IReadOnlyList<IAlias>? Aliases { get; set; }

    public string? Annotation { get; set; }

    public IReadOnlyList<INameCredit>? ArtistCredit { get; set; }

    public string? Asin { get; set; }

    public string? Barcode { get; set; }

    public IReadOnlyList<ICollection>? Collections { get; set; }

    public string? Country { get; set; }

    public ICoverArtArchive? CoverArtArchive { get; set; }

    public PartialDate? Date { get; set; }

    public string? Disambiguation { get; set; }

    public IReadOnlyList<IGenre>? Genres { get; set; }

    public IReadOnlyList<ILabelInfo>? LabelInfo { get; set; }

    public IReadOnlyList<IMedium>? Media { get; set; }

    public string? Packaging { get; set; }

    public Guid? PackagingId { get; set; }

    public string? Quality { get; set; }

    public IReadOnlyList<IRelationship>? Relationships { get; set; }

    public IReadOnlyList<IReleaseEvent>? ReleaseEvents { get; set; }

    public IReleaseGroup? ReleaseGroup { get; set; }

    public string? Status { get; set; }

    public Guid? StatusId { get; set; }

    public IReadOnlyList<ITag>? Tags { get; set; }

    public ITextRepresentation? TextRepresentation { get; set; }

    public string? Title { get; set; }

    public IReadOnlyList<IGenre>? UserGenres { get; set; }

    public IReadOnlyList<ITag>? UserTags { get; set; }

    public override string ToString() {
      var text = string.Empty;
      if (this.ArtistCredit != null) {
        foreach (var nc in this.ArtistCredit)
          text += nc.ToString();
        text += " / ";
      }
      text += this.Title;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += $" ({this.Disambiguation})";
      return text;
    }

  }

}
