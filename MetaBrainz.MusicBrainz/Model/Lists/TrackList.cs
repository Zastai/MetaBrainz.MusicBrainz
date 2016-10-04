using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class TrackList : ItemList, IResourceList<ITrackInfo> {

    [XmlElement("track")] public TrackInfo[] Items;

    #region IResourceList<ITrackInfo>

    uint? IResourceList<ITrackInfo>.Count => this.ListCount;

    uint? IResourceList<ITrackInfo>.Offset => this.ListOffset;

    IEnumerable<ITrackInfo> IResourceList<ITrackInfo>.Items => this.Items;

    #endregion

  }

}
