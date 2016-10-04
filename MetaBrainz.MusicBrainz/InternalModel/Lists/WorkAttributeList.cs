using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Serializable]
  internal sealed class WorkAttributeList : Item, IResourceList<IWorkAttribute> {

    [XmlElement("attribute")] public WorkAttribute[] Items;

    #region IResourceList<IWorkAttribute>

    uint? IResourceList<IWorkAttribute>.Count => (this.Items == null) ? null : (uint?) this.Items.Length;

    uint? IResourceList<IWorkAttribute>.Offset => null;

    IEnumerable<IWorkAttribute> IResourceList<IWorkAttribute>.Items => this.Items;

    #endregion

  }

}
