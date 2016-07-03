using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class ReleaseEvent : Item {

    #region XML Elements

    [XmlElement("area")] public Area   Area;
    [XmlElement("date")] public string Date;

    #endregion

  }

}
