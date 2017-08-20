using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A MusicBrainz artist.</summary>
  [SuppressMessage("ReSharper", "RedundantExtendsListEntry")]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public interface IArtist : IEntity, IAnnotatedEntity, INamedEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITypedEntity {

    /// <summary>The main area associated with the artist.</summary>
    IArea Area { get; }

    /// <summary>The starting area for the artist.</summary>
    IArea BeginArea { get; }

    /// <summary>The ISO 3166-1 code for the (primary) country associated with the artist.</summary>
    string Country { get; }

    /// <summary>The ending area for the artist.</summary>
    IArea EndArea { get; }

    /// <summary>The artist's gender.</summary>
    string Gender { get; }

    /// <summary>The artist's gender, expressed as an MBID.</summary>
    Guid? GenderId { get; }

    /// <summary>The IPI (Interested Parties Information) codes associated with this artist.</summary>
    IReadOnlyList<string> Ipis { get; }

    /// <summary>The ISNI (International Standard Name Identifier, ISO 27729) codes associated with this artist.</summary>
    IReadOnlyList<string> Isnis { get; }

    /// <summary>The artist's lifespan.</summary>
    ILifeSpan LifeSpan { get; }

    /// <summary>The labels associated with the artist, if any.</summary>
    IReadOnlyList<IRecording> Recordings { get; }

    /// <summary>The release groups associated with the artist, if any.</summary>
    IReadOnlyList<IReleaseGroup> ReleaseGroups { get; }

    /// <summary>The releases associated with the artist, if any.</summary>
    IReadOnlyList<IRelease> Releases { get; }

    /// <summary>The atist's sort name.</summary>
    string SortName { get; }

    /// <summary>The works associated with the artist, if any.</summary>
    IReadOnlyList<IWork> Works { get; }

  }

}
