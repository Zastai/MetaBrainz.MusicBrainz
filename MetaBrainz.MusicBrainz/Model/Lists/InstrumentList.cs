using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class InstrumentList : ItemList {

    [XmlElement("instrument")] public Instrument[] Items;

  }

}
