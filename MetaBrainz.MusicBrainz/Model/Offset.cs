using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Offset : Item, IOffset {

    #region XML Attributes

    [XmlAttribute("position")] public uint Position;

    #endregion

    #region XML Elements

    [XmlText] public string Value;

    #endregion

    #region IOffset

    uint IOffset.Position => this.Position;

    string IOffset.Value => this.Value;

    #endregion

  }

}
