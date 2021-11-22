﻿using JetBrains.Annotations;

using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities;

/// <summary>Information about available CoverArt Archive items.</summary>
[PublicAPI]
public interface ICoverArtArchive : IJsonBasedObject {

  /// <summary>Flag indicating that artwork is available.</summary>
  bool Artwork { get; }

  /// <summary>Flag indicating that a back cover image is available.</summary>
  bool Back { get; }

  /// <summary>The number of items available.</summary>
  int Count { get; }

  /// <summary>Flag indicating that the CoverArt Archive has received a take-down request for this release (preventing further uploads).</summary>
  bool Darkened { get; }

  /// <summary>Flag indicating that a front cover image is available.</summary>
  bool Front { get; }

}
