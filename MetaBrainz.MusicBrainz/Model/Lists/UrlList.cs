using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class UrlList : ItemList {

    [XmlElement("url")] public Url[] Items;

  }

}
