namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>A CD stub (information entered about a CD by someone without a MusicBrainz account).</summary>
  public interface ICdStub : IEntity {

    /// <summary>The name of the artist for the CD.</summary>
    string Artist { get; }

    /// <summary>The barcode for the CD.</summary>
    string Barcode { get; }

    /// <summary>The comment for the CD.</summary>
    string Comment { get; }

    /// <summary>The title for the CD.</summary>
    string Title { get; }

    /// <summary>The track list for the CD.</summary>
    IResourceList<ISimpleTrackInfo> TrackList { get; }

  }

}
