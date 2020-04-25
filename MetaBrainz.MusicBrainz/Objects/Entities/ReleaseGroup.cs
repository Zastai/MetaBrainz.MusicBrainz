using System;
using System.Collections.Generic;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class ReleaseGroup : Entity, IReleaseGroup {

    public ReleaseGroup(Guid id) : base(EntityType.ReleaseGroup, id) {
    }

    public IReadOnlyList<IAlias>? Aliases { get; set; }

    public string? Annotation { get; set; }

    public IReadOnlyList<INameCredit>? ArtistCredit { get; set; }

    public string? Disambiguation { get; set; }

    public PartialDate? FirstReleaseDate { get; set; }

    public IReadOnlyList<IGenre>? Genres { get; set; }

    public string? PrimaryType { get; set; }

    public Guid? PrimaryTypeId { get; set; }

    public IRating? Rating { get; set; }

    public IReadOnlyList<IRelationship>? Relationships { get; set; }

    public IReadOnlyList<IRelease>? Releases { get; set; }

    public IReadOnlyList<string>? SecondaryTypes { get; set; }

    public IReadOnlyList<Guid>? SecondaryTypeIds { get; set; }

    public IReadOnlyList<ITag>? Tags { get; set; }

    public string? Title { get; set; }

    public IReadOnlyList<IGenre>? UserGenres { get; set; }

    public IRating? UserRating { get; set; }

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
      if (!string.IsNullOrEmpty(this.PrimaryType))
        text += $" ({this.PrimaryType})";
      return text;
    }

  }

}
