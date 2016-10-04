using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class ReleaseGroupList : ItemList, IResourceList<IReleaseGroup> {

    [XmlElement("release-group")] public ReleaseGroup[] Items;

    #region IResourceList<IReleaseGroup>

    uint? IResourceList<IReleaseGroup>.Count => this.ListCount;

    uint? IResourceList<IReleaseGroup>.Offset => this.ListOffset;

    IEnumerable<IReleaseGroup> IResourceList<IReleaseGroup>.Items => this.Items;

    #endregion

  }

}
