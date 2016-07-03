using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Offset : Item {

    #region XML Attributes

    [XmlAttribute("position")] public uint Position;

    #endregion

    #region XML Elements

    [XmlText] public string Value;

    #endregion

  }

}
