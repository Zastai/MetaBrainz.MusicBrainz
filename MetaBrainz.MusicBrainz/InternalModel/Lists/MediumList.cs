using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Serializable]
  public sealed class MediumList : ItemList, IMediumList {

    [XmlElement("medium")]      public Medium[] Items;
    [XmlElement("track-count")] public uint     TrackCount;
    [XmlIgnore]                 public bool     TrackCountSpecified;

    #region IResourceList<IMedium>

    uint? IResourceList<IMedium>.Count => this.ListCount;

    uint? IResourceList<IMedium>.Offset => this.ListOffset;

    IEnumerable<IMedium> IResourceList<IMedium>.Items => this.Items;

    #endregion

    #region IMediumList

    uint? IMediumList.TrackCount => this.TrackCountSpecified ? (uint?) this.TrackCount : null;

    #endregion

  }

}
