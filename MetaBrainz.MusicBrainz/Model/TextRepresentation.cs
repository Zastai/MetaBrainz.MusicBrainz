using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class TextRepresentation : Item {

    [XmlElement("language")] public string Language;
    [XmlElement("script")]   public string Script;

  }

}
