﻿using JetBrains.Annotations;

using MetaBrainz.Common.Json;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities; 

/// <summary>A named credit for an artist.</summary>
[PublicAPI]
public interface INameCredit : IJsonBasedObject {

  /// <summary>The artist being credited.</summary>
  IArtist? Artist { get; }

  /// <summary>The join phrase used to combine this credit with subsequent ones.</summary>
  string? JoinPhrase { get; }

  /// <summary>The name under which the artist is credited.</summary>
  string? Name { get; }

}