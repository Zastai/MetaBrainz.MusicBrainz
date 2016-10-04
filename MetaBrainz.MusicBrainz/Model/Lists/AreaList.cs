using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  internal sealed class AreaList : ItemList, IResourceList<IArea> {

    [XmlElement("area")] public Area[] Items;

    #region IResourceList<IArea>

    uint? IResourceList<IArea>.Count => this.ListCount;

    uint? IResourceList<IArea>.Offset => this.ListOffset;

    IEnumerable<IArea> IResourceList<IArea>.Items => this.Items;

    #endregion

  }

}
