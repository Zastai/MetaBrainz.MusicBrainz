using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Serializable]
  internal sealed class EditorList : ItemList, IResourceList<IEditor> {

    [XmlElement("editor")] public Editor[] Items;

    #region IResourceList<IEditor>

    uint? IResourceList<IEditor>.Count => this.ListCount;

    uint? IResourceList<IEditor>.Offset => this.ListOffset;

    IEnumerable<IEditor> IResourceList<IEditor>.Items => this.Items;

    #endregion

  }

}
