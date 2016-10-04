using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Serializable]
  public sealed class LabelInfo : Item, ILabelInfo {

    #region XML Elements

    [XmlElement("catalog-number")] public string CatalogNumber;
    [XmlElement("label")]          public Label  Label;

    #endregion

    #region ILabelInfo

    string ILabelInfo.CatalogNumber => this.CatalogNumber;

    ILabel ILabelInfo.Label => this.Label;

    #endregion

  }

}
