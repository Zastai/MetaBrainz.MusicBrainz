using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class ReleaseEventList : ItemList {

    [XmlElement("release-event")] public ReleaseEvent[] Items;

  }

}
