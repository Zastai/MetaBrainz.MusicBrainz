using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A CD stub (information entered about a CD by someone without a MusicBrainz account).</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface ICdStub {

    /// <summary>The name of the artist for the CD.</summary>
    string Artist { get; }

    /// <summary>The barcode for the CD.</summary>
    string Barcode { get; }

    /// <summary>The disambiguation comment for the CD.</summary>
    string Disambiguation { get; }

    /// <summary>The Musicbrainz disc ID for the CD.</summary>
    string Id { get; }

    /// <summary>The title for the CD.</summary>
    string Title { get; }

    /// <summary>The number of tracks on the CD.</summary>
    int? TrackCount { get; }

    /// <summary>The track list for the CD.</summary>
    IReadOnlyList<ISimpleTrack> Tracks { get; }

  }

}
