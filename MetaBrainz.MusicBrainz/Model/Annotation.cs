using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Annotation : Item, IAnnotation {

    #region XML Attributes

    [XmlAttribute("type")] public string Type;

    #endregion

    #region XML Elements

    [XmlElement("entity")] public string Entity;
    [XmlElement("name")]   public string Name;
    [XmlElement("text")]   public string Text;

    #endregion

    #region ITextResource

    string ITextResource.Text => this.Text;

    #endregion

    #region IAnnotation

    string IAnnotation.Entity => this.Entity;

    string IAnnotation.Name => this.Name;

    string IAnnotation.Type => this.Type;

    #endregion

  }

}
