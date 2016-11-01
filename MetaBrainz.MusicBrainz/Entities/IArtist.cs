using System;
using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IArtist : IMbEntity, IAnnotatedEntity, INamedEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITypedEntity {

    IArea Area { get; }

    IArea BeginArea { get; }

    string Country { get; }

    IArea EndArea { get; }

    /// <summary>The artist's gender.</summary>
    string Gender { get; }

    /// <summary>The artist's gender, expressed as an MBID.</summary>
    Guid? GenderId { get; }

    /// <summary>The IPI (Interested Parties Information) codes associated with this artist.</summary>
    IEnumerable<string> Ipis { get; }

    /// <summary>The ISNI (International Standard Name Identifier, ISO 27729) codes associated with this artist.</summary>
    IEnumerable<string> Isnis { get; }

    IEnumerable<ILabel> Labels { get; }

    ILifeSpan LifeSpan { get; }

    IEnumerable<IRecording> Recordings { get; }

    IEnumerable<IReleaseGroup> ReleaseGroups { get; }

    IEnumerable<IRelease> Releases { get; }

    IEnumerable<IWork> Works { get; }

  }

}
