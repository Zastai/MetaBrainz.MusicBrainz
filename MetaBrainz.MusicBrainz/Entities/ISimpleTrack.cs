namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>Simple information about a track on a CD.</summary>
  public interface ISimpleTrack {

    /// <summary>The artist for the track.</summary>
    string Artist { get; }

    /// <summary>The length for the track, in milliseconds.</summary>
    int Length { get; }

    /// <summary>The title for the track.</summary>
    string Title { get; }

  }

}
