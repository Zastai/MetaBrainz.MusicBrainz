using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Serializable]
  public sealed class LabelList : ItemList, IResourceList<ILabel> {

    [XmlElement("label")] public Label[] Items;

    #region IResourceList<ILabel>

    uint? IResourceList<ILabel>.Count => this.Count;

    uint? IResourceList<ILabel>.Offset => this.ListOffset;

    IEnumerable<ILabel> IResourceList<ILabel>.Items => this.Items;

    #endregion

  }

}
