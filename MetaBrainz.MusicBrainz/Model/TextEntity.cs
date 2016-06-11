using System;
using System.Xml;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  public abstract class TextEntity : MBEntity {

    [XmlText] public string Text;

  }

}
