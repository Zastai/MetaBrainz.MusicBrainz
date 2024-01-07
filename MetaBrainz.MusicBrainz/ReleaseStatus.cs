using System;

using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz;

/// <summary>Enumeration of the release status values.</summary>
[Flags]
[PublicAPI]
public enum ReleaseStatus : byte {

  /// <summary>
  /// An unofficial/underground release that was not sanctioned by the artist and/or the record company. This includes unofficial
  /// live recordings and pirated releases.
  /// </summary>
  Bootleg = 1 << 0,

  /// <summary>
  /// Any release officially sanctioned by the artist and/or their record company. Most releases will fit into this category.
  /// </summary>
  Official = 1 << 1,

  /// <summary>
  /// A give-away release or a release intended to promote an upcoming official release (e.g. pre-release versions, releases
  /// included with a magazine, versions supplied to radio DJs for air-play).
  /// </summary>
  Promotion = 1 << 2,

  /// <summary>
  /// An alternate version of a release where the titles have been changed. These don't correspond to any real release and should be
  /// linked to the original release using the translation and/or transliteration relationships.
  /// </summary>
  PseudoRelease = 1 << 3,

  /// <summary>
  /// A previously official release that was actively withdrawn from circulation by the artist and/or their record company after
  /// being released, whether to replace it with a new version with some changes or to just retire it altogether (e.g. because of
  /// legal issues).
  /// </summary>
  Withdrawn = 1 << 4,

  /// <summary>
  /// A planned official release that was cancelled before being released, but for which enough info is known to still confidently
  /// list it (e.g. it was available for preorder).
  /// </summary>
  Cancelled = 1 << 5,

  // The MusicBrainz documentation pages used to call the status "Promotional", which matches "Official", but the release editor
  // (and web service) used "Promotion". This has since been fixed, making "Promotional" obsolete.
  /// <inheritdoc cref="Promotion"/>
  [Obsolete($"Use {nameof(ReleaseStatus.Promotion)} instead.")]
  Promotional = ReleaseStatus.Promotion,

}
