using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Iswc : Item, ITextResource {

    #region XML Elements

    [XmlText] public string Text;

    #endregion

    #region ITextResource

    string ITextResource.Text => this.Text;

    #endregion

  }

}
