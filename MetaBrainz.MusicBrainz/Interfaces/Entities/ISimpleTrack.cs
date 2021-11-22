﻿using System;

using JetBrains.Annotations;

using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities; 

/// <summary>Simple information about a track on a CD.</summary>
[PublicAPI]
public interface ISimpleTrack : IJsonBasedObject {

  /// <summary>The artist for the track.</summary>
  string? Artist { get; }

  /// <summary>The length for the track.</summary>
  TimeSpan Length { get; }

  /// <summary>The title for the track.</summary>
  string? Title { get; }

}