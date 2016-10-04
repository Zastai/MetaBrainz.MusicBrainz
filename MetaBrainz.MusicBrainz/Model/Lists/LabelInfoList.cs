using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  internal sealed class LabelInfoList : ItemList, IResourceList<ILabelInfo> {

    [XmlElement("label-info")] public LabelInfo[] Items;

    #region IResourceList<ILabelInfo>

    uint? IResourceList<ILabelInfo>.Count => this.ListCount;

    uint? IResourceList<ILabelInfo>.Offset => this.ListOffset;

    IEnumerable<ILabelInfo> IResourceList<ILabelInfo>.Items => this.Items;

    #endregion

  }

}
