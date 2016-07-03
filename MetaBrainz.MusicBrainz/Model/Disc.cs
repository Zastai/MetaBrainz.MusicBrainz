using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Disc : Entity {

    #region XML Elements

    [XmlElement("offset-list")]  public OffsetList  OffsetList;
    [XmlElement("release-list")] public ReleaseList ReleaseList;
    [XmlElement("sectors")]      public uint        Sectors;

    #endregion

  }

}
