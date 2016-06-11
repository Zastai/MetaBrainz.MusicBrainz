using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class TrackList : ItemList {

    [XmlElement("track")] public TrackInfo[] Items;

  }

}
