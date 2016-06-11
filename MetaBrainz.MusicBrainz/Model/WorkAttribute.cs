using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class WorkAttribute : Item {

    [XmlAttribute("type")]     public string Type;
    [XmlAttribute("type-id")]  public Guid   TypeId;
    [XmlIgnore]                public bool   TypeIdSpecified;
    [XmlAttribute("value-id")] public Guid   ValueId;
    [XmlIgnore]                public bool   ValueIdSpecified;

    [XmlText] public string Text;

  }

}
