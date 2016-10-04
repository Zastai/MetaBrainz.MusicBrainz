using System;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz {

  /// <summary>Class representing the result of a disc-id lookup: a disc or cd stub for direct ID matches, or a release list for a fuzzy lookup.</summary>
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "NotAccessedField.Global")]
  public sealed class DiscIdLookupResult {

    /// <summary>Creates a new <see cref="DiscIdLookupResult"/> instance based on the specified metadata.</summary>
    /// <param name="metadata">The metadata to take the result from.</param>
    public DiscIdLookupResult(IMetadata metadata) {
      if (metadata == null)
        throw new ArgumentNullException(nameof(metadata));
      this.Disc        = metadata.Disc;
      this.ReleaseList = metadata.ReleaseList;
      this.Stub        = metadata.CdStub;
    }

    /// <summary>The disc returned by the lookup (if any was found).</summary>
    public readonly IDisc Disc;

    /// <summary>The list of matching releases, if a fuzzy TOC lookup was done.</summary>
    public readonly IResourceList<IRelease> ReleaseList;

    /// <summary>The CD stub returned by the lookup (if any was found).</summary>
    public readonly ICdStub Stub;

  }

}
