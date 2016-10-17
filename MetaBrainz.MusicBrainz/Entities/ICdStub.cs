using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A CD stub (information entered about a CD by someone without a MusicBrainz account).</summary>
  public interface ICdStub : IEntity {

    /// <summary>The name of the artist for the CD.</summary>
    string Artist { get; }

    /// <summary>The barcode for the CD.</summary>
    string Barcode { get; }

    /// <summary>The disambiguation comment for the CD.</summary>
    string Disambiguation { get; }

    /// <summary>The title for the CD.</summary>
    string Title { get; }

    /// <summary>The number of tracks on the CD.</summary>
    int? TrackCount { get; }

    /// <summary>The track list for the CD.</summary>
    IEnumerable<ISimpleTrack> Tracks { get; }

  }

}
