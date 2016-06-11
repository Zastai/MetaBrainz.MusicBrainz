using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class LanguageList : ItemList {

    [XmlElement("language")] public SpokenLanguage[] Items;

  }

}
