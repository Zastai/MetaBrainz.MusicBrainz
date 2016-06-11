using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class LabelInfoList : ItemList {

    [XmlElement("label-info")] public LabelInfo[] Items;

  }

}
