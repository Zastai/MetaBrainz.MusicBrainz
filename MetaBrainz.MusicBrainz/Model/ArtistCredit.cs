using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class ArtistCredit : Item {

    [XmlElement("name-credit")] public NameCredit[] NameCredits;

  }

}
