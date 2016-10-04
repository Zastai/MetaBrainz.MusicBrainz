using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Serializable]
  internal sealed class TagList : ItemList, IResourceList<ITag> {

    [XmlElement("tag")] public Tag[] Items;

    #region IResourceList<ITag>

    uint? IResourceList<ITag>.Count => this.ListCount;

    uint? IResourceList<ITag>.Offset => this.ListOffset;

    IEnumerable<ITag> IResourceList<ITag>.Items => this.Items;

    #endregion

  }

}
