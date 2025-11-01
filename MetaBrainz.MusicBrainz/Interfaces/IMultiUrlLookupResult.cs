using System.Collections.Generic;

using JetBrains.Annotations;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Interfaces;

/// <summary>The result of looking up multiple URLs at once.</summary>
[PublicAPI]
public interface IMultiUrlLookupResult : IJsonBasedObject {

  /// <summary>The starting offset within the total set of matches for the current result set.</summary>
  int Offset { get; }

  /// <summary>The current results.</summary>
  IReadOnlyList<IUrl> Results { get; }

  /// <summary>The total number of matches.</summary>
  int TotalResults { get; }

}
