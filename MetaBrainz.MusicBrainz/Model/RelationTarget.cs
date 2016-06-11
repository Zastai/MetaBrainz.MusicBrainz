using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class RelationTarget : Entity {

    [XmlText] public string Value;

  }

}
