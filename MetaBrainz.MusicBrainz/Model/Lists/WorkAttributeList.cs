using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class WorkAttributeList : Item, IResourceList<IWorkAttribute> {

    [XmlElement("attribute")] public WorkAttribute[] Items;

    #region IResourceList<IWorkAttribute>

    uint? IResourceList<IWorkAttribute>.Count => (this.Items == null) ? null : (uint?) this.Items.Length;

    uint? IResourceList<IWorkAttribute>.Offset => null;

    IEnumerable<IWorkAttribute> IResourceList<IWorkAttribute>.Items => this.Items;

    #endregion

  }

}
