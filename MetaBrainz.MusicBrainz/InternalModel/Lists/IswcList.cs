using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Serializable]
  internal sealed class IswcList : ItemList, IResourceList<ITextResource> {

    [XmlElement("iswc")] public Iswc[] Items;

    #region IResourceList<ITextResource>

    uint? IResourceList<ITextResource>.Count => this.ListCount;

    uint? IResourceList<ITextResource>.Offset => this.ListOffset;

    IEnumerable<ITextResource> IResourceList<ITextResource>.Items => this.Items;

    #endregion

  }

}
