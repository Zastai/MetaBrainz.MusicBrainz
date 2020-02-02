using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>Information about the representation of text on a release.</summary>
  [PublicAPI]
  public interface ITextRepresentation : IJsonBasedObject {

    /// <summary>The ISO 363-2 language code for the release, if set.</summary>
    string Language { get; }

    /// <summary>The ISO 15924 script code for the release, if set.</summary>
    string Script { get; }

  }

}
