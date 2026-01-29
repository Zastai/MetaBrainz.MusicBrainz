using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities;

/// <summary>A MusicBrainz genre.</summary>
[PublicAPI]
public interface IGenre : INamedEntity {

  /// <summary>The number of votes that have been registered for this genre, if applicable.</summary>
  int VoteCount { get; }

}
