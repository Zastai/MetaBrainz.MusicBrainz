using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;
using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Disc : Entity, IDisc {

    #region XML Elements

    [XmlElement("offset-list")]  public OffsetList  OffsetList;
    [XmlElement("release-list")] public ReleaseList ReleaseList;
    [XmlElement("sectors")]      public uint        Sectors;

    #endregion

    #region Implementation of IDisc

    IResourceList<IOffset> IDisc.OffsetList => this.OffsetList;

    IResourceList<IRelease> IDisc.ReleaseList => this.ReleaseList;

    uint IDisc.Sectors => this.Sectors;

    #endregion

  }

}
