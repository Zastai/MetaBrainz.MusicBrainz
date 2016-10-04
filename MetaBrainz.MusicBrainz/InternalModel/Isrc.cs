using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.InternalModel.Lists;
using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Serializable]
  internal sealed class Isrc : Entity, IIsrc {

    #region XML Elements

    [XmlElement("recording-list")] public RecordingList RecordingList;

    #endregion

    #region IIsrc

    IResourceList<IRecording> IIsrc.RecordingList => this.RecordingList;

    #endregion

  }

}
