namespace MetaBrainz.MusicBrainz.Entities {

  public interface IPlace : IMbEntity, IAnnotatedEntity, INamedEntity, IRelatableEntity, ITaggableEntity, ITypedEntity {

    string Address { get; }

    IArea Area { get; }

    ICoordinates Coordinates { get; }

    ILifeSpan LifeSpan { get; }

  }

}
