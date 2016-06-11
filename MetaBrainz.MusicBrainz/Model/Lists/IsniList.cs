using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class IsniList : Item {

    [XmlElement("isni")] public string[] Items;

  }

}
