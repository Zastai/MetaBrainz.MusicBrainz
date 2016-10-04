using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  internal sealed class ReleaseEventList : ItemList, IResourceList<IReleaseEvent> {

    [XmlElement("release-event")] public ReleaseEvent[] Items;

    #region IResourceList<IReleaseEvent>

    uint? IResourceList<IReleaseEvent>.Count => this.ListCount;

    uint? IResourceList<IReleaseEvent>.Offset => this.ListOffset;

    IEnumerable<IReleaseEvent> IResourceList<IReleaseEvent>.Items => this.Items;

    #endregion

  }

}
