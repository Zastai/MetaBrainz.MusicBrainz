using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  /// <summary>An item identified by an some identifier.</summary>
  [Serializable]
  public abstract class Entity : Item {

    #region XML Attributes

    /// <summary>The identifier that identifies this entity.</summary>
    [XmlAttribute("id")] public string Id;

    #endregion

  }

}
