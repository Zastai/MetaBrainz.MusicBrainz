using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class Iso31663CodeList : Item {

    [XmlElement("iso-3166-3-code")] public string[] Items;

  }

}
