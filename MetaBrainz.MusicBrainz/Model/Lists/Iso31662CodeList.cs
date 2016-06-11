using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class Iso31662CodeList : Item {

    [XmlElement("iso-3166-2-code")] public string[] Items;

  }

}
