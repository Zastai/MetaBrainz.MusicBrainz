using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class TextRepresentation : Item, ITextRepresentation {

    #region XML Elements

    [XmlElement("language")] public string Language;
    [XmlElement("script")]   public string Script;

    #endregion

    #region ITextRepresentation

    string ITextRepresentation.Language => this.Language;

    string ITextRepresentation.Script => this.Script;

    #endregion

  }

}
