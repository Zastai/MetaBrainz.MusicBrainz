using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class UserTag : Item, IUserTag {

    #region XML Elements

    [XmlElement("name")] public string Name;

    #endregion

    #region IUserTag

    string IUserTag.Name => this.Name;

    #endregion

  }

}
