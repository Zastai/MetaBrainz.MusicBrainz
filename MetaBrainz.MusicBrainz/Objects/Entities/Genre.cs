using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Objects.Entities;

internal sealed class Genre() : Entity(EntityType.Genre), IGenre {

  public required string Disambiguation { get; init; }

  public required string Name { get; init; }

  public int VoteCount { get; init; }

  public override string ToString() => this.VoteCount is 0 ? this.Name : $"{this.Name} (votes: {this.VoteCount})";

}
