using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Event : MbEntity {

    #region XML Elements

    [XmlAttribute("type")]    public string Type;
    [XmlAttribute("type-id")] public Guid   TypeId;
    [XmlIgnore]               public bool   TypeIdSpecified;

    #endregion

    #region XML Elements

    [XmlElement("alias-list")]     public AliasList      AliasList;
    [XmlElement("annotation")]     public Annotation     Annotation;
    [XmlElement("cancelled")]      public byte           Cancelled;
    [XmlIgnore]                    public bool           CancelledSpecified;
    [XmlElement("disambiguation")] public string         Disambiguation;
    [XmlElement("life-span")]      public LifeSpan       LifeSpan;
    [XmlElement("name")]           public string         Name;
    [XmlElement("rating")]         public Rating         Rating;
    [XmlElement("relation-list")]  public RelationList[] RelationList;
    [XmlElement("setlist")]        public string         Setlist;
    [XmlElement("tag-list")]       public TagList        TagList;
    [XmlElement("time")]           public string         Time;
    [XmlElement("user-rating")]    public byte           UserRating;
    [XmlIgnore]                    public bool           UserRatingSpecified;
    [XmlElement("user-tag-list")]  public UserTagList    UserTagList;

    #endregion

  }

}
