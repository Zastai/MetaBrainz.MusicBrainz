using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  internal sealed class Recording : Entity, IRecording {

    public Recording(Guid id) : base(EntityType.Recording, id) {
    }

    public IReadOnlyList<IAlias>? Aliases { get; set; }

    public string? Annotation { get; set; }

    public IReadOnlyList<INameCredit>? ArtistCredit { get; set; }

    public string? Disambiguation { get; set; }

    public IReadOnlyList<IGenre>? Genres { get; set; }

    public IReadOnlyList<string>? Isrcs { get; set; }

    public TimeSpan? Length { get; set; }

    public IRating? Rating { get; set; }

    public IReadOnlyList<IRelationship>? Relationships { get; set; }

    public IReadOnlyList<IRelease>? Releases { get; set; }

    public IReadOnlyList<ITag>? Tags { get; set; }

    public string? Title { get; set; }

    public IReadOnlyList<IGenre>? UserGenres { get; set; }

    public IRating? UserRating { get; set; }

    public IReadOnlyList<ITag>? UserTags { get; set; }

    public bool Video { get; set; }

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
      if (this.Length.HasValue)
        text += $" ({this.Length.Value:g})";
      return text;
    }

  }

}
