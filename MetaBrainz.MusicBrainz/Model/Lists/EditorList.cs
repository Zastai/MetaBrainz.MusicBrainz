using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class EditorList : ItemList, IResourceList<IEditor> {

    [XmlElement("editor")] public Editor[] Items;

    #region IResourceList<IEditor>

    uint? IResourceList<IEditor>.Count => this.ListCount;

    uint? IResourceList<IEditor>.Offset => this.ListOffset;

    IEnumerable<IEditor> IResourceList<IEditor>.Items => this.Items;

    #endregion

  }

}
