using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Serializable]
  internal sealed class RecordingList : ItemList, IResourceList<IRecording> {

    [XmlElement("recording")] public Recording[] Items;

    #region IResourceList<IRecording>

    uint? IResourceList<IRecording>.Count => this.ListCount;

    uint? IResourceList<IRecording>.Offset => this.ListOffset;

    IEnumerable<IRecording> IResourceList<IRecording>.Items => this.Items;

    #endregion

  }

}
