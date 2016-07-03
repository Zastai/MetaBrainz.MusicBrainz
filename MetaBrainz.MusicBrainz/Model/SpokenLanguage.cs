using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class SpokenLanguage : Item {

    #region XML Attributes

    [XmlAttribute("fluency")] public string Fluency;

    #endregion

    #region XML Elements

    [XmlText] public string Name;

    #endregion

  }

}
