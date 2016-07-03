using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Isrc : Entity {

    #region XML Elements

    [XmlElement("recording-list")] public RecordingList RecordingList;

    #endregion

  }

}
