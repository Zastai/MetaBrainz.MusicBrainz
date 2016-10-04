using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.InternalModel.Lists;
using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Serializable]
  [XmlRoot("metadata", Namespace = "http://musicbrainz.org/ns/mmd-2.0#", IsNullable = false)]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  public sealed class Metadata : Item, IMetadata {

    #region XML Attributes

    [XmlAttribute("generator")] public string   Generator;
    [XmlAttribute("created"  )] public DateTime Created;
    [XmlIgnore]                 public bool     CreatedSpecified;

    #endregion

    #region XML Elements: Resources

    [XmlElement("area")]          public Area         Area;
    [XmlElement("artist")]        public Artist       Artist;
    [XmlElement("cdstub")]        public CdStub       CdStub;
    [XmlElement("collection")]    public Collection   Collection;
    [XmlElement("disc")]          public Disc         Disc;
    [XmlElement("editor")]        public Editor       Editor;
    [XmlElement("event")]         public Event        Event;
    [XmlElement("instrument")]    public Instrument   Instrument;
    [XmlElement("isrc")]          public Isrc         Isrc;
    [XmlElement("label")]         public Label        Label;
    [XmlElement("place")]         public Place        Place;
    [XmlElement("rating")]        public Rating       Rating;
    [XmlElement("recording")]     public Recording    Recording;
    [XmlElement("release")]       public Release      Release;
    [XmlElement("release-group")] public ReleaseGroup ReleaseGroup;
    [XmlElement("series")]        public Series       Series;
    [XmlElement("url")]           public Url          Url;
    [XmlElement("user-rating")]   public byte         UserRating;
    [XmlIgnore]                   public bool         UserRatingSpecified;
    [XmlElement("work")]          public Work         Work;

    [Obsolete] [XmlElement("puid")] public Puid Puid;

    #endregion

    #region XML Elements: Lists

    [XmlElement("annotation-list")]    public AnnotationList   AnnotationList;
    [XmlElement("area-list")]          public AreaList         AreaList;
    [XmlElement("artist-list")]        public ArtistList       ArtistList;
    [XmlElement("cdstub-list")]        public CdStubList       CdStubList;
    [XmlElement("collection-list")]    public CollectionList   CollectionList;
    [XmlElement("editor-list")]        public EditorList       EditorList;
    [XmlElement("entity-list")]        public EntityList       EntityList;
    [XmlElement("event-list")]         public EventList        EventList;
    [XmlElement("freedb-disc-list")]   public FreeDbDiscList   FreeDbDiscList;
    [XmlElement("instrument-list")]    public InstrumentList   InstrumentList;
    [XmlElement("isrc-list")]          public IsrcList         IsrcList;
    [XmlElement("label-list")]         public LabelList        LabelList;
    [XmlElement("place-list")]         public PlaceList        PlaceList;
    [XmlElement("recording-list")]     public RecordingList    RecordingList;
    [XmlElement("release-list")]       public ReleaseList      ReleaseList;
    [XmlElement("release-group-list")] public ReleaseGroupList ReleaseGroupList;
    [XmlElement("series-list")]        public SeriesList       SeriesList;
    [XmlElement("tag-list")]           public TagList          TagList;
    [XmlElement("url-list")]           public UrlList          UrlList;
    [XmlElement("user-tag-list")]      public UserTagList      UserTagList;
    [XmlElement("work-list")]          public WorkList         WorkList;

    #endregion

    #region IMetadata

    string IMetadata.Generator => this.Generator;

    DateTime? IMetadata.Created => this.CreatedSpecified ? (DateTime?) this.Created : null;

    IArea IMetadata.Area => this.Area;

    IArtist IMetadata.Artist => this.Artist;

    ICdStub IMetadata.CdStub => this.CdStub;

    ICollection IMetadata.Collection => this.Collection;

    IDisc IMetadata.Disc => this.Disc;

    IEditor IMetadata.Editor => this.Editor;

    IEvent IMetadata.Event => this.Event;

    IInstrument IMetadata.Instrument => this.Instrument;

    IIsrc IMetadata.Isrc => this.Isrc;

    ILabel IMetadata.Label => this.Label;

    IPlace IMetadata.Place => this.Place;

    IRating IMetadata.Rating => this.Rating;

    IRecording IMetadata.Recording => this.Recording;

    IRelease IMetadata.Release => this.Release;

    IReleaseGroup IMetadata.ReleaseGroup => this.ReleaseGroup;

    ISeries IMetadata.Series => this.Series;

    IUrl IMetadata.Url => this.Url;

    byte? IMetadata.UserRating => this.UserRatingSpecified ? (byte?) this.UserRating : null;

    IWork IMetadata.Work => this.Work;

    [Obsolete] IPuid IMetadata.Puid => this.Puid;

    IResourceList<IAnnotation> IMetadata.AnnotationList => this.AnnotationList;

    IResourceList<IArea> IMetadata.AreaList => this.AreaList;

    IResourceList<IArtist> IMetadata.ArtistList => this.ArtistList;

    IResourceList<ICdStub> IMetadata.CdStubList => this.CdStubList;

    IResourceList<ICollection> IMetadata.CollectionList => this.CollectionList;

    IResourceList<IEditor> IMetadata.EditorList => this.EditorList;

    IResourceList<IMbEntity> IMetadata.EntityList => this.EntityList;

    IResourceList<IEvent> IMetadata.EventList => this.EventList;

    IResourceList<IFreeDbDisc> IMetadata.FreeDbDiscList => this.FreeDbDiscList;

    IResourceList<IInstrument> IMetadata.InstrumentList => this.InstrumentList;

    IResourceList<IIsrc> IMetadata.IsrcList => this.IsrcList;

    IResourceList<ILabel> IMetadata.LabelList => this.LabelList;

    IResourceList<IPlace> IMetadata.PlaceList => this.PlaceList;

    IResourceList<IRecording> IMetadata.RecordingList => this.RecordingList;

    IResourceList<IRelease> IMetadata.ReleaseList => this.ReleaseList;

    IResourceList<IReleaseGroup> IMetadata.ReleaseGroupList => this.ReleaseGroupList;

    IResourceList<ISeries> IMetadata.SeriesList => this.SeriesList;

    IResourceList<ITag> IMetadata.TagList => this.TagList;

    IResourceList<IUrl> IMetadata.UrlList => this.UrlList;

    IResourceList<IUserTag> IMetadata.UserTagList => this.UserTagList;

    IResourceList<IWork> IMetadata.WorkList => this.WorkList;

    #endregion

  }

}
