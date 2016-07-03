using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class NameCredit : Item {

    #region XML Attributes

    [XmlAttribute("joinphrase")] public string JoinPhrase;

    #endregion

    #region XML Elements

    [XmlElement("artist")] public Artist Artist;
    [XmlElement("name")]   public string Name;

    #endregion

  }

}
