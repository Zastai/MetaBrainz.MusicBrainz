using System;

using JetBrains.Annotations;

using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A genre attached to an entity.</summary>
  [PublicAPI]
  public interface IGenre : IJsonBasedObject {

    /// <summary>The disambiguation text for the genre, if applicable.</summary>
    string? Disambiguation { get; }

    /// <summary>The MBID that identifies this genre.</summary>
    Guid Id { get; }

    /// <summary>The name of the genre.</summary>
    string Name { get; }

    /// <summary>The number of votes that have been registered for this genre, if applicable.</summary>
    int? VoteCount { get; }

  }

}
