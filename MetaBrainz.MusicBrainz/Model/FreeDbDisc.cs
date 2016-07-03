using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class FreeDbDisc : Item {

    #region XML Elements

    [XmlElement("artist")]     public string          Artist;
    [XmlElement("category")]   public string          Category;
    [XmlElement("comment")]    public string          Comment;
    [XmlElement("title")]      public string          Title;
    [XmlElement("track-list")] public SimpleTrackList TrackList;
    [XmlElement("year")]       public string          Year;

    #endregion

  }

}
