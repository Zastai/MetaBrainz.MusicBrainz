using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;
using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Isrc : Entity, IIsrc {

    #region XML Elements

    [XmlElement("recording-list")] public RecordingList RecordingList;

    #endregion

    #region IIsrc

    IResourceList<IRecording> IIsrc.RecordingList => this.RecordingList;

    #endregion

  }

}
