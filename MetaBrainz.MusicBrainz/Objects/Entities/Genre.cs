using System;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities {

  internal sealed class Genre : Entity, IGenre {

    public Genre(Guid id, string name) : base(EntityType.Genre, id) {
      this.Name = name;
    }

    public string? Disambiguation { get; set; }

    public string Name { get; }

    public int? VoteCount { get; set; }

    public override string ToString() {
      var text = this.Name;
      if (this.VoteCount.HasValue)
        text += $" (votes: {this.VoteCount})";
      return text;
    }

  }

}
