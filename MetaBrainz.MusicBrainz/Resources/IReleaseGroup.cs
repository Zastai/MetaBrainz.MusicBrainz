namespace MetaBrainz.MusicBrainz.Resources {

  public interface IReleaseGroup : IMbEntity, IAnnotatedResource, IRatedResource, IRelatableResource, ITaggedResource, ITitledResource, ITypedResource {

    IArtistCredit ArtistCredit { get; }

    string FirstReleaseDate { get; }

    ITextResource PrimaryType { get; }

    IResourceList<IRelease> ReleaseList { get; }

    IResourceList<ITextResource> SecondaryTypeList { get; }

  }

}
