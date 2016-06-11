using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class UserTag : Item {

    [XmlElement("name")] public string Name;

  }

}
