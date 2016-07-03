using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Work : MbEntity {

    #region XML Attributes

    [XmlAttribute("type")]    public string Type;
    [XmlAttribute("type-id")] public Guid   TypeId;
    [XmlIgnore]               public bool   TypeIdSpecified;

    #endregion

    #region XML Elements

    [XmlElement("alias-list")]     public AliasList         AliasList;
    [XmlElement("annotation")]     public Annotation        Annotation;
    [XmlElement("artist-credit")]  public ArtistCredit      ArtistCredit;
    [XmlElement("attribute-list")] public WorkAttributeList AttributeList;
    [XmlElement("disambiguation")] public string            Disambiguation;
    [XmlElement("iswc")]           public string            IswcCode;
    [XmlElement("iswc-list")]      public IswcList          IswcList;
    [XmlElement("language")]       public string            Language;
    [XmlElement("rating")]         public Rating            Rating;
    [XmlElement("relation-list")]  public RelationList[]    RelationList;
    [XmlElement("tag-list")]       public TagList           TagList;
    [XmlElement("title")]          public string            Title;
    [XmlElement("user-rating")]    public byte              UserRating;
    [XmlIgnore]                    public bool              UserRatingSpecified;
    [XmlElement("user-tag-list")]  public UserTagList       UserTagList;

    #endregion

  }

}
