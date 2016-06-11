using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  /// <summary>An item identified by an MBID.</summary>
  [Serializable]
  public abstract class MBEntity : Item {

    /// <summary>The MBID that identifies this entity.</summary>
    [XmlAttribute("id")] public Guid Id;

  }

}
