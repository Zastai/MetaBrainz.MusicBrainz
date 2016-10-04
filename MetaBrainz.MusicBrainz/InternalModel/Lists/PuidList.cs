using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Obsolete]
  [Serializable]
  public sealed class PuidList : ItemList, IResourceList<IPuid> {

    [XmlElement("puid")] public Puid[] Items;

    #region IResourceList<IPuid>

    uint? IResourceList<IPuid>.Count => this.ListCount;

    uint? IResourceList<IPuid>.Offset => this.ListOffset;

    IEnumerable<IPuid> IResourceList<IPuid>.Items => this.Items;

    #endregion

  }

}
