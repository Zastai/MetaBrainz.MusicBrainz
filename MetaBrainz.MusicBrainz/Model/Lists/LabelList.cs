using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class LabelList : ItemList {

    [XmlElement("label")] public Label[] Items;

  }

}
