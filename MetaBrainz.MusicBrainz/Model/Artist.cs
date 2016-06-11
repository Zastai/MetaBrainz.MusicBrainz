using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Artist : MBEntity {

    [XmlAttribute("type")]    public string Type;
    [XmlAttribute("type-id")] public Guid   TypeId;
    [XmlIgnore]               public bool   TypeIdSpecified;

    [XmlElement("alias-list")]         public AliasList        AliasList;
    [XmlElement("annotation")]         public Annotation       Annotation;
    [XmlElement("area")]               public Area             Area;
    [XmlElement("begin-area")]         public Area             BeginArea;
    [XmlElement("country")]            public string           Country;
    [XmlElement("disambiguation")]     public string           Disambiguation;
    [XmlElement("end-area")]           public Area             EndArea;
    [XmlElement("gender")]             public Gender           Gender;
    [XmlElement("ipi")]                public string           Ipi;
    [XmlElement("ipi-list")]           public IpiList          IpiList;
    [XmlElement("isni-list")]          public IsniList         IsniList;
    [XmlElement("label-list")]         public LabelList        LabelList;
    [XmlElement("life-span")]          public LifeSpan         Lifespan;
    [XmlElement("name")]               public string           Name;
    [XmlElement("rating")]             public Rating           Rating;
    [XmlElement("recording-list")]     public RecordingList    RecordingList;
    [XmlElement("relation-list")]      public RelationList[]   RelationList;
    [XmlElement("release-list")]       public ReleaseList      ReleaseList;
    [XmlElement("release-group-list")] public ReleaseGroupList ReleaseGroupList;
    [XmlElement("sort-name")]          public string           SortName;
    [XmlElement("tag-list")]           public TagList          TagList;
    [XmlElement("user-rating")]        public byte             UserRating;
    [XmlIgnore]                        public bool             UserRatingSpecified;
    [XmlElement("user-tag-list")]      public UserTagList      UserTagList;
    [XmlElement("work-list")]          public WorkList         WorkList;

  }

}
