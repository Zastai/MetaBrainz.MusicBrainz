using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  #if NETFX_GE_4_5
  using DiscList  = IReadOnlyList<IDisc>;
  using TrackList = IReadOnlyList<ITrack>;
  #else
  using DiscList  = IEnumerable<IDisc>;
  using TrackList = IEnumerable<ITrack>;
  #endif

  /// <summary>A medium associated with a release.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface IMedium {

    /// <summary>The data tracks on the medium, if any.</summary>
    TrackList DataTracks { get; }

    /// <summary>The physical discs associated with the medium, if any.</summary>
    DiscList Discs { get; }

    /// <summary>The medium's format, expressed as text.</summary>
    string Format { get; }

    /// <summary>The medium's format, expressed as an MBID.</summary>
    Guid? FormatId { get; }

    /// <summary>The position of the medium in its release's medium list.</summary>
    int Position { get; }

    /// <summary>The pre-gap track on the medium, if any.</summary>
    ITrack Pregap { get; }

    /// <summary>The medium's title.</summary>
    string Title { get; }

    /// <summary>The number of tracks on the medium.</summary>
    int TrackCount { get; }

    /// <summary>The first track present in <see cref="Tracks"/>, if not all tracks were loaded by this request.</summary>
    int? TrackOffset { get; }

    /// <summary>The normal (audio) tracks on the medium, if any.</summary>
    TrackList Tracks { get; }

  }

}
