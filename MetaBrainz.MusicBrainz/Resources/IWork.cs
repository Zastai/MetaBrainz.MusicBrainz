namespace MetaBrainz.MusicBrainz.Resources {

  public interface IWork : IMbEntity, IAnnotatedResource, IRatedResource, IRelatableResource, ITaggedResource, ITitledResource, ITypedResource {

    IArtistCredit ArtistCredit { get; }

    IResourceList<IWorkAttribute> AttributeList { get; }

    string IswcCode { get; }

    IResourceList<ITextResource> IswcList { get; }

    string Language { get; }

  }

}
