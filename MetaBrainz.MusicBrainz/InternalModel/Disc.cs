using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.InternalModel.Lists;
using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Serializable]
  public sealed class Disc : Entity, IDisc {

    #region XML Elements

    [XmlElement("offset-list")]  public OffsetList  OffsetList;
    [XmlElement("release-list")] public ReleaseList ReleaseList;
    [XmlElement("sectors")]      public uint        Sectors;

    #endregion

    #region IDisc

    IResourceList<IOffset> IDisc.OffsetList => this.OffsetList;

    IResourceList<IRelease> IDisc.ReleaseList => this.ReleaseList;

    uint IDisc.Sectors => this.Sectors;

    #endregion

  }

}
