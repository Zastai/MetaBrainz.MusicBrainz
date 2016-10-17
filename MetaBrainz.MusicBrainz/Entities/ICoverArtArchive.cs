namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>Information about available CoverArt Archive items.</summary>
  public interface ICoverArtArchive {

    /// <summary>Flag indicating that artwork is available.</summary>
    bool Artwork { get; }

    /// <summary>Flag indicating that a back cover image is available.</summary>
    bool Back { get; }

    /// <summary>The number of items available.</summary>
    int Count { get; }

    /// <summary>Flag indicating that the CoverArt Archive has received a takedown request for this release (preventing further uploads).</summary>
    bool? Darkened { get; }

    /// <summary>Flag indicating that a front cover image is available.</summary>
    bool Front { get; }

  }

}
