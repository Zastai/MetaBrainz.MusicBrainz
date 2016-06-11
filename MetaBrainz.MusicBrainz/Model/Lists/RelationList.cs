using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class RelationList : ItemList {

    [XmlAttribute("target-type")] public string TargetType;

    [XmlElement("relation")] public Relation[] Items;

  }

}
