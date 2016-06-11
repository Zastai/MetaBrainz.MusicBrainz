using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class IpiList : Item {

    [XmlElement("ipi")] public string[] Items;

  }

}
