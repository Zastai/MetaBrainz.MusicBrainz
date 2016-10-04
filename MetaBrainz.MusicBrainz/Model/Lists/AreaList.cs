using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class AreaList : ItemList, IResourceList<IArea> {

    [XmlElement("area")] public Area[] Items;

    #region Implementation of IResourceList<out IArea>

    uint? IResourceList<IArea>.Count => this.ListCount;

    uint? IResourceList<IArea>.Offset => this.ListOffset;

    IEnumerable<IArea> IResourceList<IArea>.Items => this.Items;

    #endregion

  }

}
