using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  internal sealed class InstrumentList : ItemList, IResourceList<IInstrument> {

    [XmlElement("instrument")] public Instrument[] Items;

    #region IResourceList<IInstrument>

    uint? IResourceList<IInstrument>.Count => this.ListCount;

    uint? IResourceList<IInstrument>.Offset => this.ListOffset;

    IEnumerable<IInstrument> IResourceList<IInstrument>.Items => this.Items;

    #endregion

  }

}
