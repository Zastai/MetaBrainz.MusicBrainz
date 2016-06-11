using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class CdStubList : ItemList {

    [XmlElement("cdstub")] public CdStub[] Items;

  }

}
