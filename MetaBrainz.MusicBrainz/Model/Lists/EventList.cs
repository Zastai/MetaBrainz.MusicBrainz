using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class EventList : ItemList, IResourceList<IEvent> {

    [XmlElement("event")] public Event[] Items;

    #region IResourceList<IEvent>

    uint? IResourceList<IEvent>.Count => this.ListCount;

    uint? IResourceList<IEvent>.Offset => this.ListOffset;

    IEnumerable<IEvent> IResourceList<IEvent>.Items => this.Items;

    #endregion

  }

}
