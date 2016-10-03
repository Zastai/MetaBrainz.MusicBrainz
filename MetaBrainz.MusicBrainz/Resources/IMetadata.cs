using System;

namespace MetaBrainz.MusicBrainz.Resources {

  public interface IMetadata : IResource {

    #region Attributes

    string Generator { get; }

    DateTime? Created { get; }

    #endregion

    #region Resources

    IArea Area { get; }

    IArtist Artist { get; }

    ICdStub CdStub { get; }

    ICollection Collection { get; }

    IDisc Disc { get; }

    IEditor Editor { get; }

    IEvent Event { get; }

    IInstrument Instrument { get; }

    IIsrc Isrc { get; }

    ILabel Label { get; }

    IPlace Place { get; }

    IRating Rating { get; }

    IRecording Recording { get; }

    IRelease Release { get; }

    IReleaseGroup ReleaseGroup { get; }

    ISeries Series { get; }

    IUrl Url { get; }

    byte? UserRating { get; }

    IWork Work { get; }

    [Obsolete] IPuid Puid { get; }

    #endregion

    #region Resource Lists

    IResourceList<IAnnotation> AnnotationList { get; }

    IResourceList<IArea> AreaList { get; }

    IResourceList<IArtist> ArtistList { get; }

    IResourceList<ICdStub> CdStubList { get; }

    IResourceList<ICollection> CollectionList { get; }

    IResourceList<IEditor> EditorList { get; }

    IResourceList<IMbEntity> EntityList { get; }

    IResourceList<IEvent> EventList { get; }

    IResourceList<IFreeDbDisc> FreeDbDiscList { get; }

    IResourceList<IInstrument> InstrumentList { get; }

    IResourceList<IIsrc> IsrcList { get; }

    IResourceList<ILabel> LabelList { get; }

    IResourceList<IPlace> PlaceList { get; }

    IResourceList<IRecording> RecordingList { get; }

    IResourceList<IRelease> ReleaseList { get; }

    IResourceList<IReleaseGroup> ReleaseGroupList { get; }

    IResourceList<ISeries> SeriesList { get; }

    IResourceList<ITag> TagList { get; }

    IResourceList<IUrl> UrlList { get; }

    IResourceList<IUserTag> UserTagList { get; }

    IResourceList<IWork> WorkList { get; }

    #endregion

  }

}
