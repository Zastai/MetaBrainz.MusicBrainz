using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class RecordingList : ItemList, IResourceList<IRecording> {

    [XmlElement("recording")] public Recording[] Items;

    #region IResourceList<IRecording>

    uint? IResourceList<IRecording>.Count => this.ListCount;

    uint? IResourceList<IRecording>.Offset => this.ListOffset;

    IEnumerable<IRecording> IResourceList<IRecording>.Items => this.Items;

    #endregion

  }

}
