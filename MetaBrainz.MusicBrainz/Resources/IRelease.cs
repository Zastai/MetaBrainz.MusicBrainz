namespace MetaBrainz.MusicBrainz.Resources {

  public interface IRelease : IMbEntity, IAnnotatedResource, IRelatableResource, ITaggedResource, ITitledResource {

    IArtistCredit ArtistCredit { get; }

    string Asin { get; }

    string BarCode { get; }

    IResourceList<ICollection> CollectionList { get; }

    string Country { get; }

    ICoverArtArchive CoverArtArchive { get; }

    string Date { get; }

    IResourceList<ILabelInfo> LabelInfoList { get; }

    IMediumList MediumList { get; }

    ITextResource Packaging { get; }

    string Quality { get; }

    IResourceList<IReleaseEvent> ReleaseEventList { get; }

    IReleaseGroup ReleaseGroup { get; }

    ITextResource Status { get; }

    ITextRepresentation TextRepresentation { get; }

  }

}
