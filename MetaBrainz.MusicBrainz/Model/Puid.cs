using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;
using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.Model {

  [Obsolete]
  [Serializable]
  internal sealed class Puid : MbEntity, IPuid {

    #region XML Elements

    [XmlElement("recording-list")] public RecordingList RecordingList;

    #endregion

    #region IPuid

    IResourceList<IRecording> IPuid.RecordingList => this.RecordingList;

    #endregion

  }

}
