using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Medium : Item {

    [XmlElement("data-track-list")] public TrackList DataTrackList;
    [XmlElement("disc-list")]       public DiscList  DiscList;
    [XmlElement("format")]          public Format    Format;
    [XmlElement("position")]        public uint      Position;
    [XmlElement("pregap")]          public TrackInfo Pregap;
    [XmlElement("title")]           public string    Title;
    [XmlElement("track-list")]      public TrackList TrackList;

  }

}
