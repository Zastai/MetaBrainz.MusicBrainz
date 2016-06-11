using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class ReleaseList : ItemList {

    [XmlElement("release")] public Release[] Items;

  }

}
