using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  /// <summary>An item identified by some identifier.</summary>
  [Serializable]
  internal abstract class Entity : Item, IEntity {

    #region XML Attributes

    /// <summary>The identifier that identifies this entity.</summary>
    [XmlAttribute("id")] public string Id;

    #endregion

    #region IEntity

    string IEntity.Id => this.Id;

    #endregion

  }

}
