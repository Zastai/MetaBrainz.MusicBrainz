using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  internal sealed class DiscList : ItemList, IResourceList<IDisc> {

    [XmlElement("disc")] public Disc[] Items;

    #region IResourceList<IDisc>

    uint? IResourceList<IDisc>.Count => this.ListCount;

    uint? IResourceList<IDisc>.Offset => this.ListOffset;

    IEnumerable<IDisc> IResourceList<IDisc>.Items => this.Items;

    #endregion

  }

}
