using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  [XmlRoot("metadata", Namespace = "http://musicbrainz.org/ns/mmd-2.0#", IsNullable = false)]
  public sealed class Metadata : Item {

    [XmlAttribute("generator")] public string   Generator;
    [XmlAttribute("created"  )] public DateTime Created;
    [XmlIgnore]                 public bool     CreatedSpecified;

    #region Resources

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

    #region Lists

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

  }

}
