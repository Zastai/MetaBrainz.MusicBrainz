using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class EditorList : ItemList {

    [XmlElement("editor")] public Editor[] Items;

  }

}
