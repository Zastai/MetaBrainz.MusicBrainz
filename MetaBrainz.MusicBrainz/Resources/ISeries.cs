namespace MetaBrainz.MusicBrainz.Resources {

  public interface ISeries : IMbEntity, IAnnotatedResource, INamedResource, IRelatableResource, ITaggedResource, ITypedResource {

    string OrderingAttribute { get; }

  }

}
