using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  internal sealed class PlaceList : ItemList, IResourceList<IPlace> {

    [XmlElement("place")] public Place[] Items;

    #region IResourceList<IPlace>

    uint? IResourceList<IPlace>.Count => this.ListCount;

    uint? IResourceList<IPlace>.Offset => this.ListOffset;

    IEnumerable<IPlace> IResourceList<IPlace>.Items => this.Items;

    #endregion

  }

}
