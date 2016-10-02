namespace MetaBrainz.MusicBrainz.Resources {

  public interface IWork : IMbEntity, IAnnotatedResource, ITypedResource, IRatedResource, IRelatableResource, ITaggedResource {

    IResourceList<IAlias> AliasList { get; }

    IArtistCredit ArtistCredit { get; }

    IResourceList<IWorkAttribute> AttributeList { get; }

    string Disambiguation { get; }

    string IswcCode { get; }

    IResourceList<ITextResource> IswcList { get; }

    string Language { get; }

    string Title { get; }

  }

}
