using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Rating : Item {

    #region XML Attributes

    [XmlAttribute("votes-count")] public uint VoteCount;

    #endregion

    #region XML Elements

    [XmlText] public string Text;

    #endregion

  }

}
