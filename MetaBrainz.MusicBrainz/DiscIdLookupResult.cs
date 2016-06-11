using System;

using MetaBrainz.MusicBrainz.Model;
using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz {

  /// <summary>Class representing the result of a disc-id lookup: a disc or cd stub for direct ID matches, or a release list for a fuzzy lookup.</summary>
  public sealed class DiscIdLookupResult {

    /// <summary>Creates a new <see cref="DiscIdLookupResult"/> instance based on the specified metadata.</summary>
    /// <param name="metadata">The metadata to take the result from.</param>
    public DiscIdLookupResult(Metadata metadata) {
      if (metadata == null)
        throw new ArgumentNullException(nameof(metadata));
      this.Disc        = metadata.Disc;
      this.ReleaseList = metadata.ReleaseList;
      this.Stub        = metadata.CdStub;
    }

    /// <summary>The disc returned by the lookup (if any was found).</summary>
    public readonly Disc Disc;

    /// <summary>The list of matching releases, if a fuzzy TOC lookup was done.</summary>
    public readonly ReleaseList ReleaseList;

    /// <summary>The CD stub returned by the lookup (if any was found).</summary>
    public readonly CdStub Stub;

  }

}
