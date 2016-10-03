using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  public abstract class TextEntity : MbEntity, ITextResource {

    #region XML Elements

    [XmlText] public string Text;

    #endregion

    #region ITextResource

    string ITextResource.Text => this.Text;

    #endregion

  }

}
