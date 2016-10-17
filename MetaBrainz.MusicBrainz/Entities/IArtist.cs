using System;
using System.Collections.Generic;

namespace MetaBrainz.MusicBrainz.Entities {

  public interface IArtist : IMbEntity, IAnnotatedEntity, INamedEntity, IRatedEntity, IRelatableEntity, ITaggedEntity, ITypedEntity {

    IArea Area { get; }

    IArea BeginArea { get; }

    string Country { get; }

    IArea EndArea { get; }

    /// <summary>The artist's gender.</summary>
    string Gender { get; }

    /// <summary>The artist's gender, expressed as an MBID.</summary>
    Guid? GenderId { get; }

    IEnumerable<string> Ipis { get; }

    IEnumerable<string> Isnis { get; }

    IEnumerable<ILabel> Labels { get; }

    ILifeSpan LifeSpan { get; }

    IEnumerable<IRecording> Recordings { get; }

    IEnumerable<IReleaseGroup> ReleaseGroups { get; }

    IEnumerable<IRelease> Releases { get; }

    IEnumerable<IWork> Works { get; }

  }

}
