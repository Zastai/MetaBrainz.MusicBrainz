using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  internal sealed class AliasList : ItemList, IResourceList<IAlias> {

    [XmlElement("alias")] public Alias[] Items;

    #region IResourceList<IAlias>

    uint? IResourceList<IAlias>.Count => this.ListCount;

    uint? IResourceList<IAlias>.Offset => this.ListOffset;

    IEnumerable<IAlias> IResourceList<IAlias>.Items => this.Items;

    #endregion

  }

}
