using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Obsolete]
  [Serializable]
  public class PuidList : ItemList {

    [XmlElement("puid")] public Puid[] Items;

  }

}
