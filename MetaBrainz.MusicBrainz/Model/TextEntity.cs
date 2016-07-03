using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  public abstract class TextEntity : MbEntity {

    #region XML Elements

    [XmlText] public string Text;

    #endregion

  }

}
