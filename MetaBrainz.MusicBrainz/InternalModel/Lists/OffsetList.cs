using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Serializable]
  public sealed class OffsetList : ItemList, IResourceList<IOffset> {

    [XmlElement("offset")] public Offset[] Items;

    #region IResourceList<IOffset>

    uint? IResourceList<IOffset>.Count => this.ListCount;

    uint? IResourceList<IOffset>.Offset => this.ListOffset;

    IEnumerable<IOffset> IResourceList<IOffset>.Items => this.Items;

    #endregion

  }

}
