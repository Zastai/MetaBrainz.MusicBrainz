namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>Simple information about a track on a CD.</summary>
  public interface ISimpleTrackInfo : IResource {

    /// <summary>The artist for the track.</summary>
    string Artist { get; }

    /// <summary>The length for the track (in seconds).</summary>
    uint Length { get; }

    /// <summary>The title for the track.</summary>
    string Title { get; }

  }

}
