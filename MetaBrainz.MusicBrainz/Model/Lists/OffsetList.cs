using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class OffsetList : ItemList {

    [XmlElement("offset")] public Offset[] Items;

  }

}
