using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  internal sealed class SecondaryTypeList : Item, IResourceList<ITextResource> {

    [XmlElement("secondary-type")] public SecondaryType[] Items;

    #region IResourceList<ITextResource>

    uint? IResourceList<ITextResource>.Count  => (this.Items == null) ? null : (uint?) this.Items.Length;

    uint? IResourceList<ITextResource>.Offset => null;

    IEnumerable<ITextResource> IResourceList<ITextResource>.Items => this.Items;

    #endregion

  }

}
