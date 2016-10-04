using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class CdStubList : ItemList, IResourceList<ICdStub> {

    [XmlElement("cdstub")] public CdStub[] Items;

    #region IResourceList<ICdStub>

    uint? IResourceList<ICdStub>.Count => this.ListCount;

    uint? IResourceList<ICdStub>.Offset => this.ListOffset;

    IEnumerable<ICdStub> IResourceList<ICdStub>.Items => this.Items;

    #endregion
  }

}
