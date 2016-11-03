namespace MetaBrainz.MusicBrainz.Entities {

  public interface IPlace : IEntity, IAnnotatedEntity, INamedEntity, IRelatableEntity, ITaggableEntity, ITypedEntity {

    string Address { get; }

    IArea Area { get; }

    ICoordinates Coordinates { get; }

    ILifeSpan LifeSpan { get; }

  }

}
