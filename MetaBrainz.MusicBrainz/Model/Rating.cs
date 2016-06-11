using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Rating : Item {

    [XmlAttribute("votes-count")] public uint VoteCount;

    [XmlText] public string Text;

  }

}
