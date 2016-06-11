using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Obsolete]
  [Serializable]
  public class Puid : MBEntity {

    [XmlElement("recording-list")] public RecordingList RecordingList;

  }

}
