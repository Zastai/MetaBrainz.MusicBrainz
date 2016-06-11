using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class FreeDbDiscList : ItemList {

    [XmlElement("freedb-disc")] public FreeDbDisc[] Items;

  }

}
