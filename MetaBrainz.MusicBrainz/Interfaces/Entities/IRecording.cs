using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities;

/// <summary>A MusicBrainz recording.</summary>
[PublicAPI]
public interface IRecording : IAliasedEntity, IAnnotatedEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITitledEntity {

  /// <summary>The artist credit for the recording.</summary>
  IReadOnlyList<INameCredit> ArtistCredit { get; }

  /// <summary>The earliest date of release for this recording.</summary>
  PartialDate? FirstReleaseDate { get; }

  /// <summary>The ISRC (International Standard Recording Code) values associated with this release.</summary>
  IReadOnlyList<string> Isrcs { get; }

  /// <summary>The length of the recording.</summary>
  TimeSpan? Length { get; }

  /// <summary>The releases that include the recording.</summary>
  IReadOnlyList<IRelease> Releases { get; }

  /// <summary>Flag indicating whether this recording includes visual content.</summary>
  bool Video { get; }

}
