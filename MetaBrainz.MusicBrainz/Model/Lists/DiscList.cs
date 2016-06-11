using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class DiscList : ItemList {

    [XmlElement("disc")] public Disc[] Items;

  }

}
