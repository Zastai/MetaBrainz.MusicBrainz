using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class CoverArtArchive : Item {

    #region XML Elements

    [XmlElement("artwork")]  public bool Artwork;
    [XmlElement("back")]     public bool Back;
    [XmlElement("count")]    public int  Count;
    [XmlElement("darkened")] public bool Darkened;
    [XmlIgnore]              public bool DarkenedSpecified;
    [XmlElement("front")]    public bool Front;

    #endregion

  }

}
