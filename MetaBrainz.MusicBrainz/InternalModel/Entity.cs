using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.InternalModel {

  /// <summary>An item identified by some identifier.</summary>
  [Serializable]
  public abstract class Entity : Item, IEntity {

    #region XML Attributes

    /// <summary>The identifier that identifies this entity.</summary>
    [XmlAttribute("id")] public string Id;

    #endregion

    #region IEntity

    string IEntity.Id => this.Id;

    #endregion

  }

}
