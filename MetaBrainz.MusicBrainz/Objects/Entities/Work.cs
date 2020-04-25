using System;
using System.Collections.Generic;

using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Interfaces.Searches;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  internal sealed class Work : Entity, IFoundWork {

    public Work(Guid id) : base(EntityType.Work, id) {
    }

    public IReadOnlyList<IAlias>? Aliases { get; set; }

    public string? Annotation { get; set; }

    public IReadOnlyList<IWorkAttribute>? Attributes { get; set; }

    public string? Disambiguation { get; set; }

    public IReadOnlyList<IGenre>? Genres { get; set; }

    public IReadOnlyList<string>? Iswcs { get; set; }

    public string? Language { get; set; }

    public IReadOnlyList<string>? Languages { get; set; }

    public IRating? Rating { get; set; }

    public IReadOnlyList<IRelationship>? Relationships { get; set; }

    public IReadOnlyList<ITag>? Tags { get; set; }

    public string? Title { get; set; }

    public string? Type { get; set; }

    public Guid? TypeId { get; set; }

    public IReadOnlyList<IGenre>? UserGenres { get; set; }

    public IRating? UserRating { get; set; }

    public IReadOnlyList<ITag>? UserTags { get; set; }

    public override string ToString() {
      var text = string.Empty;
      if (this.SearchScore.HasValue)
        text += $"[Score: {this.SearchScore.Value}] ";
      if (this.Title != null)
        text += this.Title;
      if (!string.IsNullOrEmpty(this.Disambiguation))
        text += $" ({this.Disambiguation})";
      if (this.Type != null)
        text += $" ({this.Type})";
      return text;
    }

  }

}
