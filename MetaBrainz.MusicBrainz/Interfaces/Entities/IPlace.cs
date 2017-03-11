using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz.Interfaces.Entities {

  /// <summary>A MusicBrainz place.</summary>
  [SuppressMessage("ReSharper", "RedundantExtendsListEntry")]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public interface IPlace : IEntity, IAnnotatedEntity, INamedEntity, IRelatableEntity, ITaggableEntity, ITypedEntity {

    /// <summary>The address for the place.</summary>
    string Address { get; }

    /// <summary>The area where the place is located.</summary>
    IArea Area { get; }

    /// <summary>The coordinates for the place.</summary>
    ICoordinates Coordinates { get; }

    /// <summary>The place's lifespan.</summary>
    ILifeSpan LifeSpan { get; }

  }

}
