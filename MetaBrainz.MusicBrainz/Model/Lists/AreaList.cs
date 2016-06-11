using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class AreaList : ItemList {

    [XmlElement("area")] public Area[] Items;

  }

}
