using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class RelationTarget : Entity {

    #region XML Elements

    [XmlText] public string Value;

    #endregion

  }

}
