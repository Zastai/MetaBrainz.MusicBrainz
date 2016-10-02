using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class UserTagList : ItemList, IResourceList<IUserTag> {

    [XmlElement("user-tag")] public UserTag[] Items;

    #region IResourceList<IUserTag>

    uint? IResourceList<IUserTag>.Count => this.ListCount;

    uint? IResourceList<IUserTag>.Offset => this.ListOffset;

    IEnumerable<IUserTag> IResourceList<IUserTag>.Items => this.Items;

    #endregion

  }

}
