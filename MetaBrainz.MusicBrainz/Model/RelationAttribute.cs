using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class RelationAttribute : Item {

    #region XML Attributes

    [XmlAttribute("credited-as")] public string CreditedAs;
    [XmlAttribute("value")]       public string Value;

    #endregion

    #region XML Elements

    [XmlText] public string Text;

    #endregion

  }

}
