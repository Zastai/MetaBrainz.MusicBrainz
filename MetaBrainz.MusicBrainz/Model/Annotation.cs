using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Annotation : Item {

    [XmlAttribute("type")] public string Type;

    [XmlElement("entity")] public string Entity;
    [XmlElement("name")]   public string Name;
    [XmlElement("text")]   public string Text;

  }

}
