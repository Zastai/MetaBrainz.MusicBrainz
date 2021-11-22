﻿using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities;

/// <summary>A MusicBrainz release group.</summary>
[PublicAPI]
public interface IReleaseGroup : IAliasedEntity, IAnnotatedEntity, IRatableEntity, IRelatableEntity, ITaggableEntity,
                                 ITitledEntity {

  /// <summary>The artist credit for the release group.</summary>
  IReadOnlyList<INameCredit>? ArtistCredit { get; }

  /// <summary>The earliest date of release for the releases in the group.</summary>
  PartialDate? FirstReleaseDate { get; }

  /// <summary>The primary type of the release group, expressed as text.</summary>
  string? PrimaryType { get; }

  /// <summary>The primary type of the release group, expressed as an MBID.</summary>
  Guid? PrimaryTypeId { get; }

  /// <summary>The releases associated with the release group.</summary>
  IReadOnlyList<IRelease>? Releases { get; }

  /// <summary>The secondary types of the release group (if any), expressed as text.</summary>
  IReadOnlyList<string>? SecondaryTypes { get; }

  /// <summary>The secondary types of the release group (if any), expressed as MBIDs.</summary>
  IReadOnlyList<Guid>? SecondaryTypeIds { get; }

}
