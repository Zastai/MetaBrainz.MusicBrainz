using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

using JetBrains.Annotations;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
  internal sealed class ReleaseGroup : Entity, IFoundReleaseGroup {

    public ReleaseGroup(Guid id) : base(EntityType.ReleaseGroup, id) {
    }

    public IReadOnlyList<IAlias>? Aliases { get; set; }

    public string? Annotation { get; set; }

    public IReadOnlyList<INameCredit>? ArtistCredit { get; set; }

    public string? Disambiguation { get; set; }

    public PartialDate? FirstReleaseDate { get; set; }

    public IReadOnlyList<ITag>? Genres { get; set; }

    public string? PrimaryType { get; set; }

    public Guid? PrimaryTypeId { get; set; }

    public IRating? Rating { get; set; }

    public IReadOnlyList<IRelationship>? Relationships { get; set; }

    public IReadOnlyList<IRelease>? Releases { get; set; }

    public IReadOnlyList<string>? SecondaryTypes { get; set; }

    public IReadOnlyList<Guid>? SecondaryTypeIds { get; set; }

    public IReadOnlyList<ITag>? Tags { get; set; }

    public string? Title { get; set; }

    public IReadOnlyList<IUserTag>? UserGenres { get; set; }

    public IUserRating? UserRating { get; set; }

    public IReadOnlyList<IUserTag>? UserTags { get; set; }

    public override string ToString() {
      var text = string.Empty;
      if (this.SearchScore.HasValue)
        text += $"[Score: {this.SearchScore.Value}] ";
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
