using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class NameCredit : Item {

    [XmlAttribute("joinphrase")] public string JoinPhrase;

    [XmlElement("artist")] public Artist Artist;
    [XmlElement("name")]   public string Name;

  }

}
