using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class AliasList : ItemList {

    [XmlElement("alias")] public Alias[] Items;

  }

}
