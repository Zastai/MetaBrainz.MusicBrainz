using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class IswcList : ItemList {

    [XmlElement("iswc")] public string[] Items;

  }

}
