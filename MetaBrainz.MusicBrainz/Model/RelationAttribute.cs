using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class RelationAttribute : Item {

    [XmlAttribute("credited-as")] public string CreditedAs;
    [XmlAttribute("value")]       public string Value;

    [XmlText] public string Text;

  }

}
