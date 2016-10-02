using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class RelationAttribute : Item, IRelationAttribute {

    #region XML Attributes

    [XmlAttribute("credited-as")] public string CreditedAs;
    [XmlAttribute("value")]       public string Value;

    #endregion

    #region XML Elements

    [XmlText] public string Text;

    #endregion

    #region ITextResource

    string ITextResource.Text => this.Text;

    #endregion

    #region IRelationAttribute

    string IRelationAttribute.CreditedAs => this.CreditedAs;

    string IRelationAttribute.Value => this.Value;

    #endregion

  }

}
