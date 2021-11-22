﻿using System;
using System.Collections.Generic;

using JetBrains.Annotations;

using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities;

/// <summary>A medium associated with a release.</summary>
[PublicAPI]
public interface IMedium : IJsonBasedObject {

  /// <summary>The data tracks on the medium, if any.</summary>
  IReadOnlyList<ITrack>? DataTracks { get; }

  /// <summary>The physical discs associated with the medium, if any.</summary>
  IReadOnlyList<IDisc>? Discs { get; }

  /// <summary>The medium's format, expressed as text.</summary>
  string? Format { get; }

  /// <summary>The medium's format, expressed as an MBID.</summary>
  Guid? FormatId { get; }

  /// <summary>The position of the medium in its release's medium list.</summary>
  int Position { get; }

  /// <summary>The pre-gap track on the medium, if any.</summary>
  ITrack? Pregap { get; }

  /// <summary>The medium's title.</summary>
  string? Title { get; }

  /// <summary>The number of tracks on the medium.</summary>
  int TrackCount { get; }

  /// <summary>The first track present in <see cref="Tracks"/>, if not all tracks were loaded by this request.</summary>
  int? TrackOffset { get; }

  /// <summary>The normal (audio) tracks on the medium, if any.</summary>
  IReadOnlyList<ITrack>? Tracks { get; }

}
