using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Instrument : MBEntity {

    [XmlAttribute("type")]    public string Type;
    [XmlAttribute("type-id")] public Guid   TypeId;
    [XmlIgnore]               public bool   TypeIdSpecified;

    [XmlElement("alias-list")]     public AliasList      AliasList;
    [XmlElement("annotation")]     public Annotation     Annotation;
    [XmlElement("description")]    public string         Description;
    [XmlElement("disambiguation")] public string         Disambiguation;
    [XmlElement("name")]           public string         Name;
    [XmlElement("relation-list")]  public RelationList[] RelationList;
    [XmlElement("tag-list")]       public TagList        TagList;
    [XmlElement("user-tag-list")]  public UserTagList    UserTagList;

  }

}
