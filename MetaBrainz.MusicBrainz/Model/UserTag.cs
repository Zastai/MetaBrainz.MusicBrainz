using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  internal sealed class UserTag : Item, IUserTag {

    #region XML Elements

    [XmlElement("name")] public string Name;

    #endregion

    #region IUserTag

    string IUserTag.Name => this.Name;

    #endregion

  }

}
