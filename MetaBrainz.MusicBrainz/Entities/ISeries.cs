using System;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Entities {

  /// <summary>A MusicBrainz series.</summary>
  [SuppressMessage("ReSharper", "RedundantExtendsListEntry")]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public interface ISeries : IEntity, IAnnotatedEntity, INamedEntity, IRelatableEntity, ITaggableEntity, ITypedEntity {

    /// <summary>The ordering type for the series, expressed as text.</summary>
    string OrderingType { get; }

    /// <summary>The ordering type for the series, expressed as an MBID.</summary>
    Guid? OrderingTypeId { get; }

  }

}
