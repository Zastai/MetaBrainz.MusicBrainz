using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class ReleaseGroupList : ItemList {

    [XmlElement("release-group")] public ReleaseGroup[] Items;

  }

}
