using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Serializable]
  public sealed class UrlList : ItemList, IResourceList<IUrl> {

    [XmlElement("url")] public Url[] Items;

    #region IResourceList<IUrl>

    uint? IResourceList<IUrl>.Count => this.ListCount;

    uint? IResourceList<IUrl>.Offset => this.ListOffset;

    IEnumerable<IUrl> IResourceList<IUrl>.Items => this.Items;

    #endregion

  }

}
