using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Tag : Item, ITag {

    #region XML Attributes

    [XmlAttribute("count")] public uint VoteCount;

    #endregion

    #region XML Elements

    [XmlElement("name")] public string Name;

    #endregion

    #region ITag

    uint ITag.VoteCount => this.VoteCount;

    string ITag.Name => this.Name;

    #endregion

  }

}
