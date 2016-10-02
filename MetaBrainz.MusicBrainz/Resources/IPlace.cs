namespace MetaBrainz.MusicBrainz.Resources {

  public interface IPlace : IMbEntity, IAnnotatedResource, INamedResource, IRelatableResource, ITaggedResource, ITypedResource {

    string Address { get; }

    IArea Area { get; }

    ICoordinates Coordinates { get; }

    ILifeSpan LifeSpan { get; }

  }

}
