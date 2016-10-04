using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Serializable]
  internal sealed class SpokenLanguage : Item, ISpokenLanguage {

    #region XML Attributes

    [XmlAttribute("fluency")] public string Fluency;

    #endregion

    #region XML Elements

    [XmlText] public string Name;

    #endregion

    #region ISpokenLanguage

    string ISpokenLanguage.Fluency => this.Fluency;

    string ISpokenLanguage.Name => this.Name;

    #endregion

  }

}
