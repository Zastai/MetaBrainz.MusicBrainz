using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Place : MbEntity {

    #region XML Attributes

    [XmlAttribute("type")]    public string Type;
    [XmlAttribute("type-id")] public Guid   TypeId;
    [XmlIgnore]               public bool   TypeIdSpecified;

    #endregion

    #region XML Elements

    [XmlElement("address")]        public string         Address;
    [XmlElement("alias-list")]     public AliasList      AliasList;
    [XmlElement("annotation")]     public Annotation     Annotation;
    [XmlElement("area")]           public Area           Area;
    [XmlElement("coordinates")]    public Coordinates    Coordinates;
    [XmlElement("disambiguation")] public string         Disambiguation;
    [XmlElement("life-span")]      public LifeSpan       LifeSpan;
    [XmlElement("name")]           public string         Name;
    [XmlElement("relation-list")]  public RelationList[] RelationList;
    [XmlElement("tag-list")]       public TagList        TagList;
    [XmlElement("user-tag-list")]  public UserTagList    UserTagList;

    #endregion

  }

}
