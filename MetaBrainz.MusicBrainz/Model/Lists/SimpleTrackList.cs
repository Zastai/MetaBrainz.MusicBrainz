using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class SimpleTrackList : ItemList, IResourceList<ISimpleTrackInfo> {

    [XmlElement("track")] public SimpleTrackInfo[] Items;

    #region IResourceList<ISimpleTrackInfo>

    uint? IResourceList<ISimpleTrackInfo>.Count => this.ListCount;

    uint? IResourceList<ISimpleTrackInfo>.Offset => this.ListOffset;

    IEnumerable<ISimpleTrackInfo> IResourceList<ISimpleTrackInfo>.Items => this.Items;

    #endregion

  }

}
