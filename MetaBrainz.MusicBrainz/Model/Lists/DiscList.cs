using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class DiscList : ItemList, IResourceList<IDisc> {

    [XmlElement("disc")] public Disc[] Items;

    #region IResourceList<IDisc>

    uint? IResourceList<IDisc>.Count => this.ListCount;

    uint? IResourceList<IDisc>.Offset => this.ListOffset;

    IEnumerable<IDisc> IResourceList<IDisc>.Items => this.Items;

    #endregion

  }

}
