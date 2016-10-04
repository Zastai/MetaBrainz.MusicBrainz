using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Serializable]
  public sealed class Tag : Item, ITag {

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
