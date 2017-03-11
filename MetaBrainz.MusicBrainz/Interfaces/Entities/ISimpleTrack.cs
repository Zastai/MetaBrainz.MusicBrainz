using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>Simple information about a track on a CD.</summary>
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface ISimpleTrack {

    /// <summary>The artist for the track.</summary>
    string Artist { get; }

    /// <summary>The length for the track, in milliseconds.</summary>
    int Length { get; }

    /// <summary>The title for the track.</summary>
    string Title { get; }

  }

}
