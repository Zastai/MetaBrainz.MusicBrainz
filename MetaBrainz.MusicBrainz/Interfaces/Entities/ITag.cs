using JetBrains.Annotations;

using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A tag attached to an entity.</summary>
  [PublicAPI]
  public interface ITag : IJsonBasedObject {

    /// <summary>The name of the tag.</summary>
    string? Name { get; }

    /// <summary>The number of votes that have been registered for this tag.</summary>
    int VoteCount { get; }

  }

}
