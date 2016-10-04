using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class IsrcList : ItemList, IResourceList<IIsrc> {

    [XmlElement("isrc")] public Isrc[] Items;

    #region IResourceList<IIsrc>

    uint? IResourceList<IIsrc>.Count => this.ListCount;

    uint? IResourceList<IIsrc>.Offset => this.ListOffset;

    IEnumerable<IIsrc> IResourceList<IIsrc>.Items => this.Items;

    #endregion

  }

}
