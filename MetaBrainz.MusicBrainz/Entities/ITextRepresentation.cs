namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>Information about a release's textual representation.</summary>
  public interface ITextRepresentation {

    /// <summary>The ISO 363-2 language code for the release, if set.</summary>
    string Language { get; }

    /// <summary>The ISO 15924 script code for the release, if set.</summary>
    string Script { get; }

  }

}
