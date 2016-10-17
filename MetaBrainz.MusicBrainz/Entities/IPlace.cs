namespace MetaBrainz.MusicBrainz.Entities {

  public interface IPlace : IMbEntity, IAnnotatedEntity, INamedEntity, IRelatableEntity, ITaggedEntity, ITypedEntity {

    string Address { get; }

    IArea Area { get; }

    ICoordinates Coordinates { get; }

    ILifeSpan LifeSpan { get; }

  }

}
