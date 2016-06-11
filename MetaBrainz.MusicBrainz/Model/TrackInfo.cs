using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class TrackInfo : Entity {

    [XmlElement("artist-credit")] public ArtistCredit ArtistCredit;
    [XmlElement("length")]        public uint         Length;
    [XmlIgnore]                   public bool         LengthSpecified;
    [XmlElement("number")]        public string       Number;
    [XmlElement("position")]      public uint         Position;
    [XmlIgnore]                   public bool         PositionSpecified;
    [XmlElement("recording")]     public Recording    Recording;
    [XmlElement("title")]         public string       Title;

  }

}
