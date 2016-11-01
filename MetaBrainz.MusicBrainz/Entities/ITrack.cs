using System;
using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A track on a medium.</summary>
  public interface ITrack {

    /// <summary>The MBID for the track.</summary>
    Guid Id { get; }

    IEnumerable<INameCredit> ArtistCredit { get; }

    int? Length { get; }

    string Number { get; }

    int? Position { get; }

    IRecording Recording { get; }

    string Title { get; }

  }

}
