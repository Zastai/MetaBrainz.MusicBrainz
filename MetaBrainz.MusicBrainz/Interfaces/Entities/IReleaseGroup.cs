using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  #if NETFX_LT_4_5
  using GuidList       = IEnumerable<Guid>;
  using NameCreditList = IEnumerable<INameCredit>;
  using ReleaseList    = IEnumerable<IRelease>;
  using StringList     = IEnumerable<string>;
  #else
  using GuidList       = IReadOnlyList<Guid>;
  using NameCreditList = IReadOnlyList<INameCredit>;
  using ReleaseList    = IReadOnlyList<IRelease>;
  using StringList     = IReadOnlyList<string>;
  #endif

  /// <summary>A MusicBrainz release group.</summary>
  [SuppressMessage("ReSharper", "RedundantExtendsListEntry")]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface IReleaseGroup : IEntity, IAnnotatedEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITitledEntity {

    /// <summary>The artist credit for the release group.</summary>
    NameCreditList ArtistCredit { get; }

    /// <summary>The earliest date of release for the releases in the group.</summary>
    PartialDate FirstReleaseDate { get; }

    /// <summary>The primary type of the release group, expressed as text.</summary>
    string PrimaryType { get; }

    /// <summary>The primary type of the release group, expressed as an MBID.</summary>
    Guid? PrimaryTypeId { get; }

    /// <summary>The releases associated with the release group.</summary>
    ReleaseList Releases { get; }

    /// <summary>The secondary types of the release group (if any), expressed as text.</summary>
    StringList SecondaryTypes { get; }

    /// <summary>The secondary types of the release group (if any), expressed as MBIDs.</summary>
    GuidList SecondaryTypeIds { get; }

  }

}
