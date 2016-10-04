using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

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
