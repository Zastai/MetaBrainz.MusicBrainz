using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class OffsetList : ItemList, IResourceList<IOffset> {

    [XmlElement("offset")] public Offset[] Items;

    #region IResourceList<IOffset>

    uint? IResourceList<IOffset>.Count => this.ListCount;

    uint? IResourceList<IOffset>.Offset => this.ListOffset;

    IEnumerable<IOffset> IResourceList<IOffset>.Items => this.Items;

    #endregion

  }

}
