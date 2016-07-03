using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class UserTag : Item {

    #region XML Elements

    [XmlElement("name")] public string Name;

    #endregion

  }

}
