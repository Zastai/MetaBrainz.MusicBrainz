using System;
using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IMedium {

    IEnumerable<ITrack> DataTracks { get; }

    IEnumerable<IDisc> Discs { get; }

    string Format { get; }

    Guid? FormatId { get; }

    int? Position { get; }

    ITrack Pregap { get; }

    string Title { get; }

    int TrackCount { get; }

    int TrackOffset { get; }

    IEnumerable<ITrack> Tracks { get; }

  }

}
