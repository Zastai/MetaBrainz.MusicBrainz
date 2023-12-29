using System;

using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Genre : Entity, IGenre {

  public Genre(Guid id, string name) : base(EntityType.Genre, id) {
    this.Name = name;
  }

  public string? Disambiguation { get; init; }

  public string Name { get; }

  public int? VoteCount { get; init; }

  public override string ToString() {
    var text = this.Name;
    if (this.VoteCount is not null) {
      text += $" (votes: {this.VoteCount})";
    }
    return text;
  }

}
