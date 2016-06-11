using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Offset : Item {

    [XmlAttribute("position")] public uint Position;

    [XmlText] public string Value;

  }

}
