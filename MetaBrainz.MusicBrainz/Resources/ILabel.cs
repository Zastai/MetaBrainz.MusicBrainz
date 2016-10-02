namespace MetaBrainz.MusicBrainz.Resources {

  public interface ILabel : IMbEntity, IAnnotatedResource, INamedResource, IRatedResource, IRelatableResource, ITaggedResource, ITypedResource {

    IArea Area { get; }

    string Country { get; }

    string Ipi { get; }

    IResourceList<ITextResource> IpiList { get; }

    uint LabelCode { get; }

    ILifeSpan Lifespan { get; }

    IResourceList<IRelease> ReleaseList { get; }

  }

}
