namespace MetaBrainz.MusicBrainz.Resources {

  public interface IEvent : IMbEntity, IAnnotatedResource, INamedResource, IRatedResource, IRelatableResource, ITaggedResource, ITypedResource {

    bool? Cancelled { get; }

    ILifeSpan LifeSpan { get; }

    string Setlist { get; }

    string Time { get; }

  }

}
