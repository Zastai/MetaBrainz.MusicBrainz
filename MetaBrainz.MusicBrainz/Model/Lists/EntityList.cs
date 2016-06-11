using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class EntityList : ItemList {

    [XmlElement("area")]          public Area        [] Areas;
    [XmlElement("artist")]        public Artist      [] Artists;
    [XmlElement("event")]         public Event       [] Events;
    [XmlElement("instrument")]    public Instrument  [] Instruments;
    [XmlElement("label")]         public Label       [] Labels;
    [XmlElement("place")]         public Place       [] Places;
    [XmlElement("recording")]     public Recording   [] Recordings;
    [XmlElement("release")]       public Release     [] Releases;
    [XmlElement("release-group")] public ReleaseGroup[] ReleaseGroups;
    [XmlElement("series")]        public Series      [] Series;
    [XmlElement("work")]          public Work        [] Works;

  }

}
