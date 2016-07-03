using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public abstract class MbEntity : Item {

    #region XML Attributes

    [XmlAttribute("id")] public Guid Id;

    #endregion

  }

}
