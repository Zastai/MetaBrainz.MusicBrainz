using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class SeriesList : ItemList {

    [XmlElement("series")] public Series[] Items;

  }

}
