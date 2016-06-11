using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class Iso31661CodeList : Item {

    [XmlElement("iso-3166-1-code")] public string[] Items;

  }

}
