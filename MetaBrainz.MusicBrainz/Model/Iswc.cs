using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  internal sealed class Iswc : Item, ITextResource {

    #region XML Elements

    [XmlText] public string Text;

    #endregion

    #region ITextResource

    string ITextResource.Text => this.Text;

    #endregion

  }

}
