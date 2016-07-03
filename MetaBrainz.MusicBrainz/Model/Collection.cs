using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Collection : MbEntity {

    #region XML Attributes

    [XmlAttribute("entity-type")] public string EntityType;
    [XmlAttribute("type")]        public string Type;
    [XmlAttribute("type-id")]     public Guid   TypeId;
    [XmlIgnore]                   public bool   TypeIdSpecified;

    #endregion

    #region XML Elements

    [XmlElement("editor")] public string Editor;
    [XmlElement("name")]   public string Name;

    [XmlElement("area-list")]          public AreaList         AreaList;
    [XmlElement("artist-list")]        public ArtistList       ArtistList;
    [XmlElement("event-list")]         public EventList        EventList;
    [XmlElement("instrument-list")]    public InstrumentList   InstrumentList;
    [XmlElement("label-list")]         public LabelList        LabelList;
    [XmlElement("place-list")]         public PlaceList        PlaceList;
    [XmlElement("recording-list")]     public RecordingList    RecordingList;
    [XmlElement("release-list")]       public ReleaseList      ReleaseList;
    [XmlElement("release-group-list")] public ReleaseGroupList ReleaseGroupList;
    [XmlElement("series-list")]        public SeriesList       SeriesList;
    [XmlElement("work-list")]          public WorkList         WorkList;

    #endregion

  }

}
