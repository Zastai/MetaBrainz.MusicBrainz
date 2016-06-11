using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class CollectionList : ItemList {

    [XmlElement("collection")] public Collection[] Items;

  }

}
