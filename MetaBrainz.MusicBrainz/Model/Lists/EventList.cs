using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class EventList : ItemList {

    [XmlElement("event")] public Event[] Items;

  }

}
