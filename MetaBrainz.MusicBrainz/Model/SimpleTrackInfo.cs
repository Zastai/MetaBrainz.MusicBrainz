using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class SimpleTrackInfo : Item, ISimpleTrackInfo {

    #region XML Elements

    [XmlElement("artist")] public string Artist;
    [XmlElement("length")] public uint   Length;
    [XmlElement("title")]  public string Title;

    #endregion

    #region ISimpleTrackInfo

    string ISimpleTrackInfo.Artist => this.Artist;

    uint ISimpleTrackInfo.Length => this.Length;

    string ISimpleTrackInfo.Title => this.Title;

    #endregion

  }

}
