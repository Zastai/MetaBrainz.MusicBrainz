using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class TagList : ItemList {

    [XmlElement("tag")] public Tag[] Items;

  }

}
