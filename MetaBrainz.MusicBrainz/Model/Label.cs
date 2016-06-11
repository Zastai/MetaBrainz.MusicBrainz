using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Label : MBEntity {

    [XmlAttribute("type")]    public string Type;
    [XmlAttribute("type-id")] public Guid   TypeId;
    [XmlIgnore]               public bool   TypeIdSpecified;

    [XmlElement("alias-list")]     public AliasList      AliasList;
    [XmlElement("annotation")]     public Annotation     Annotation;
    [XmlElement("area")]           public Area           Area;
    [XmlElement("country")]        public string         Country;
    [XmlElement("disambiguation")] public string         Disambiguation;
    [XmlElement("ipi")]            public string         Ipi;
    [XmlElement("ipi-list")]       public IpiList        IpiList;
    [XmlElement("label-code")]     public uint           LabelCode;
    [XmlElement("life-span")]      public LifeSpan       Lifespan;
    [XmlElement("name")]           public string         Name;
    [XmlElement("rating")]         public Rating         Rating;
    [XmlElement("relation-list")]  public RelationList[] RelationList;
    [XmlElement("release-list")]   public ReleaseList    ReleaseList;
    [XmlElement("sort-name")]      public string         SortName;
    [XmlElement("tag-list")]       public TagList        TagList;
    [XmlElement("user-rating")]    public byte           UserRating;
    [XmlIgnore]                    public bool           UserRatingSpecified;
    [XmlElement("user-tag-list")]  public UserTagList    UserTagList;

  }

}
