using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Genre() : Entity(EntityType.Genre), IGenre {

  public string? Disambiguation { get; init; }

  public required string Name { get; init; }

  public int? VoteCount { get; init; }

  public override string ToString() {
    var text = this.Name;
    if (this.VoteCount is not null) {
      text += $" (votes: {this.VoteCount})";
    }
    return text;
  }

}
