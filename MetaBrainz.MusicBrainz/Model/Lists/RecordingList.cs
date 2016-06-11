using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class RecordingList : ItemList {

    [XmlElement("recording")] public Recording[] Items;

  }

}
