using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Serializable]
  internal sealed class ReleaseList : ItemList, IResourceList<IRelease> {

    [XmlElement("release")] public Release[] Items;

    #region IResourceList<IRelease>

    uint? IResourceList<IRelease>.Count => this.ListCount;

    uint? IResourceList<IRelease>.Offset => this.ListOffset;

    IEnumerable<IRelease> IResourceList<IRelease>.Items => this.Items;

    #endregion

  }

}
