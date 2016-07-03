using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Annotation : Item {

    #region XML Attributes

    [XmlAttribute("type")] public string Type;

    #endregion

    #region XML Elements

    [XmlElement("entity")] public string Entity;
    [XmlElement("name")]   public string Name;
    [XmlElement("text")]   public string Text;

    #endregion

  }

}
