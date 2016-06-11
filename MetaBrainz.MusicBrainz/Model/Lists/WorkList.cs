using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class WorkList : ItemList {

    [XmlElement("work")] public Work[] Items;

  }

}
