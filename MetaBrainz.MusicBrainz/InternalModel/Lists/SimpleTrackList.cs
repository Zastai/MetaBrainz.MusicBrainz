using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Serializable]
  internal sealed class SimpleTrackList : ItemList, IResourceList<ISimpleTrackInfo> {

    [XmlElement("track")] public SimpleTrackInfo[] Items;

    #region IResourceList<ISimpleTrackInfo>

    uint? IResourceList<ISimpleTrackInfo>.Count => this.ListCount;

    uint? IResourceList<ISimpleTrackInfo>.Offset => this.ListOffset;

    IEnumerable<ISimpleTrackInfo> IResourceList<ISimpleTrackInfo>.Items => this.Items;

    #endregion

  }

}
