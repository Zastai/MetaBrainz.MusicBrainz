using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Tag : Item {

    #region XML Attributes

    [XmlAttribute("count")] public uint VoteCount;

    #endregion

    #region XML Elements

    [XmlElement("name")] public string Name;

    #endregion

  }

}
