using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class WorkAttributeList : Item {

    [XmlElement("attribute")] public WorkAttribute[] Items;

  }

}
