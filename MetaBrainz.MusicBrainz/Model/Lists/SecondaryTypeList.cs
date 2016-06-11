using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class SecondaryTypeList : Item {

    [XmlElement("secondary-type")] public SecondaryType[] Items;

  }

}
