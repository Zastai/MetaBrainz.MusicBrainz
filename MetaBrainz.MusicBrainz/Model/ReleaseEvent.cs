using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class ReleaseEvent : Item {

    [XmlElement("area")] public Area   Area;
    [XmlElement("date")] public string Date;

  }

}
