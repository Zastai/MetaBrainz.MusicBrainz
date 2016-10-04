using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class ReleaseList : ItemList, IResourceList<IRelease> {

    [XmlElement("release")] public Release[] Items;

    #region IResourceList<IRelease>

    uint? IResourceList<IRelease>.Count => this.ListCount;

    uint? IResourceList<IRelease>.Offset => this.ListOffset;

    IEnumerable<IRelease> IResourceList<IRelease>.Items => this.Items;

    #endregion

  }

}
