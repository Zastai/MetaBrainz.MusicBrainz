using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class IsrcList : ItemList {

    [XmlElement("isrc")] public Isrc[] Items;

  }

}
