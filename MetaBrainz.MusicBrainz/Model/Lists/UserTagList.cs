using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class UserTagList : ItemList {

    [XmlElement("user-tag")] public UserTag[] Items;

  }

}
