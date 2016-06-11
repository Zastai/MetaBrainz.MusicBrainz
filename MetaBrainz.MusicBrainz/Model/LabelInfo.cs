using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class LabelInfo : Item {

    [XmlElement("catalog-number")] public string CatalogNumber;
    [XmlElement("label")]          public Label  Label;

  }

}
