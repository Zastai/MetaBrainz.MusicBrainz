using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class ArtistList : ItemList {

    [XmlElement("artist")] public Artist[] Items;

  }

}
