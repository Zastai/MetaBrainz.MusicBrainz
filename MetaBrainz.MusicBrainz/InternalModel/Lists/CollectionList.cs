using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Serializable]
  internal sealed class CollectionList : ItemList, IResourceList<ICollection> {

    [XmlElement("collection")] public Collection[] Items;

    #region IResourceList<ICollection>

    uint? IResourceList<ICollection>.Count => this.ListCount;

    uint? IResourceList<ICollection>.Offset => this.ListOffset;

    IEnumerable<ICollection> IResourceList<ICollection>.Items => this.Items;

    #endregion

  }

}
