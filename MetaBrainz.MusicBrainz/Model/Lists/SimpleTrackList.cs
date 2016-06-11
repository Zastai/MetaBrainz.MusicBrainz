using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class SimpleTrackList : ItemList {

    [XmlElement("track")] public SimpleTrackInfo[] Items;

  }

}
