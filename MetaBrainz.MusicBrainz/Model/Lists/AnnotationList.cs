using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class AnnotationList : ItemList {

    [XmlElement("annotation")] public Annotation[] Items;

  }

}
