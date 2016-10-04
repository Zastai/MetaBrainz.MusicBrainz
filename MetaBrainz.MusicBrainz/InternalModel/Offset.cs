using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Serializable]
  public sealed class Offset : Item, IOffset {

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
