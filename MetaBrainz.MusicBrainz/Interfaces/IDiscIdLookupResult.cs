using System.Collections.Generic;

using JetBrains.Annotations;

using MetaBrainz.Common.Json;
using MetaBrainz.MusicBrainz.Interfaces.Entities;

namespace MetaBrainz.MusicBrainz.Interfaces;

/// <summary>The result of a lookup for a MusicBrainz disc ID: a disc or cd stub for direct ID matches, or a release list for a fuzzy lookup.</summary>
[PublicAPI]
public interface IDiscIdLookupResult : IJsonBasedObject {

  /// <summary>The disc returned by the lookup (if any was found).</summary>
  IDisc? Disc { get; }

  /// <summary>The list of matching releases, if a fuzzy TOC lookup was done.</summary>
  IReadOnlyList<IRelease>? Releases { get; }

  /// <summary>The CD stub returned by the lookup (if any was found).</summary>
  ICdStub? Stub { get; }

}
