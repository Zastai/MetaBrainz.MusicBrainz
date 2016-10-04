using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Serializable]
  public abstract class MbEntity : Item, IMbEntity {

    #region XML Attributes

    [XmlAttribute("id")] public Guid Id;

    #endregion

    #region IMbEntity

    string IEntity.Id => this.Id.ToString("D");

    Guid IMbEntity.Id => this.Id;

    #endregion

  }

}
