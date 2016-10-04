using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class SecondaryTypeList : Item, IResourceList<ITextResource> {

    [XmlElement("secondary-type")] public SecondaryType[] Items;

    #region Implementation of IResourceList<out ITextResource>

    uint? IResourceList<ITextResource>.Count  => (this.Items == null) ? null : (uint?) this.Items.Length;

    uint? IResourceList<ITextResource>.Offset => null;

    IEnumerable<ITextResource> IResourceList<ITextResource>.Items => this.Items;

    #endregion

  }

}
