using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class LabelList : ItemList, IResourceList<ILabel> {

    [XmlElement("label")] public Label[] Items;

    #region IResourceList<ILabel>

    uint? IResourceList<ILabel>.Count => this.Count;

    uint? IResourceList<ILabel>.Offset => this.ListOffset;

    IEnumerable<ILabel> IResourceList<ILabel>.Items => this.Items;

    #endregion

  }

}
