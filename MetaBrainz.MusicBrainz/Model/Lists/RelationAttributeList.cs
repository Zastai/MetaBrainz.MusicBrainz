using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class RelationAttributeList : Item {

    [XmlElement("attribute")] public RelationAttribute[] Items;

  }

}
