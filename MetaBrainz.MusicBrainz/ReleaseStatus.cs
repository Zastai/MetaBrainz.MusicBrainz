using System;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz {

  /// <summary>Enumeration of the release status values.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [Flags]
  public enum ReleaseStatus : byte {

    /// <summary>An unofficial/underground release that was not sanctioned by the artist and/or the record company. This includes unofficial live recordings and pirated releases.</summary>
    Bootleg       = 1 << 0,

    /// <summary>Any release officially sanctioned by the artist and/or their record company. Most releases will fit into this category.</summary>
    Official      = 1 << 1,

    /// <summary>A give-away release or a release intended to promote an upcoming official release (e.g. pre-release versions, releases included with a magazine, versions supplied to radio DJs for air-play).</summary>
    Promotional   = 1 << 2,

    /// <summary>An alternate version of a release where the titles have been changed. These don't correspond to any real release and should be linked to the original release using the transl(iter)ation relationship.</summary>
    PseudoRelease = 1 << 3,

    // The MusicBrainz documentation pages call the status "Promotional", which matches "Official". However, the release editor (and web service) use "Promotion").
    /// <summary>A give-away release or a release intended to promote an upcoming official release (e.g. pre-release versions, releases included with a magazine, versions supplied to radio DJs for air-play).</summary>
    Promotion = ReleaseStatus.Promotional,

  }

}
