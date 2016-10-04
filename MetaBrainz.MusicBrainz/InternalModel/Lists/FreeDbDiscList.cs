using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Serializable]
  public sealed class FreeDbDiscList : ItemList, IResourceList<IFreeDbDisc> {

    [XmlElement("freedb-disc")] public FreeDbDisc[] Items;

    #region IResourceList<IFreeDbDisc>

    uint? IResourceList<IFreeDbDisc>.Count => this.ListCount;

    uint? IResourceList<IFreeDbDisc>.Offset => this.ListOffset;

    IEnumerable<IFreeDbDisc> IResourceList<IFreeDbDisc>.Items => this.Items;

    #endregion

  }

}
