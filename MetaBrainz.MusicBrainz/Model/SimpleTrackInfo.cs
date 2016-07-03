using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class SimpleTrackInfo : Item {

    #region XML Elements

    [XmlElement("artist")] public string Artist;
    [XmlElement("length")] public uint   Length;
    [XmlElement("title")]  public string Title;

    #endregion

  }

}
