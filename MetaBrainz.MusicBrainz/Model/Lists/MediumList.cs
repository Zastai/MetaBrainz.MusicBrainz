using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class MediumList : ItemList {

    [XmlElement("medium")]      public Medium[] Items;
    [XmlElement("track-count")] public uint     TrackCount;
    [XmlIgnore]                 public bool     TrackCountSpecified;

  }

}
