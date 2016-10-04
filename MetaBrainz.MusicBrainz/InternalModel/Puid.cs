using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.InternalModel.Lists;
using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Obsolete]
  [Serializable]
  public sealed class Puid : MbEntity, IPuid {

    #region XML Elements

    [XmlElement("recording-list")] public RecordingList RecordingList;

    #endregion

    #region IPuid

    IResourceList<IRecording> IPuid.RecordingList => this.RecordingList;

    #endregion

  }

}
