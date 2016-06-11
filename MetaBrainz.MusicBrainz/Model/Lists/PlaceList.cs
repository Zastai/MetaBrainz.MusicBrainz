using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class PlaceList : ItemList {

    [XmlElement("place")] public Place[] Items;

  }

}
