using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A tag set by the authenticated user.</summary>
  [PublicAPI]
  public interface IUserTag : IJsonBasedObject {

    /// <summary>The name of the tag.</summary>
    string? Name { get; }

  }

}
