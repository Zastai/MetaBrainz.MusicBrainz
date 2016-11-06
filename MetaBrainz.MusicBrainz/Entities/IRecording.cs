using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A MusicBrainz recording.</summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
  public interface IRecording : IEntity, IAnnotatedEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITitledEntity {

    /// <summary>The artist credit for the recording.</summary>
    IEnumerable<INameCredit> ArtistCredit { get; }

    /// <summary>The ISRC (International Standard Recording Code) values associated with this release.</summary>
    IEnumerable<string> Isrcs { get; }

    /// <summary>The length of the recording, in milliseconds.</summary>
    int? Length { get; }

    /// <summary>The releases that include the recording.</summary>
    IEnumerable<IRelease> Releases { get; }

    /// <summary>Flag indicating whether or not this recording includes visual content.</summary>
    bool Video { get; }

  }

}
