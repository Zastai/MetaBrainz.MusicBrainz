using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class CdStub : Entity {

    [XmlElement("artist")]         public string          Artist;
    [XmlElement("barcode")]        public string          Barcode;
    [XmlElement("disambiguation")] public string          Comment;
    [XmlElement("title")]          public string          Title;
    [XmlElement("track-list")]     public SimpleTrackList TrackList;

  }

}
