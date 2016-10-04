using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Obsolete]
  [Serializable]
  public class PuidList : ItemList, IResourceList<IPuid> {

    [XmlElement("puid")] public Puid[] Items;

    #region IResourceList<IPuid>

    uint? IResourceList<IPuid>.Count => this.ListCount;

    uint? IResourceList<IPuid>.Offset => this.ListOffset;

    IEnumerable<IPuid> IResourceList<IPuid>.Items => this.Items;

    #endregion

  }

}
