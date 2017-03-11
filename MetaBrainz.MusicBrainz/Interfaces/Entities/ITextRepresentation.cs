using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>Information about a release's textual representation.</summary>
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface ITextRepresentation {

    /// <summary>The ISO 363-2 language code for the release, if set.</summary>
    string Language { get; }

    /// <summary>The ISO 15924 script code for the release, if set.</summary>
    string Script { get; }

  }

}
