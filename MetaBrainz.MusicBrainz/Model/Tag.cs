using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Tag : Item {

    [XmlAttribute("count")] public uint VoteCount;

    [XmlElement("name")] public string Name;

  }

}
