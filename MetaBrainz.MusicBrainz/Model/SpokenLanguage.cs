using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class SpokenLanguage : Item {

    [XmlAttribute("fluency")] public string Fluency;

    [XmlText] public string Name;

  }

}
