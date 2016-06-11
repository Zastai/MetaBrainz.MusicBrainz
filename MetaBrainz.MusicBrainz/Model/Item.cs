using System;
using System.Xml;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  /// <summary>A generic item. Serves as a container for any otherwise unmatched attributes and elements.</summary>
  [Serializable]
  public abstract class Item {

    /// <summary>Attributes not matched by the standard schema; these will be either extensions, or new schema contents not yet supported by this library.</summary>
    [XmlAnyAttribute] public XmlAttribute[] ExtensionAttributes;

    /// <summary>Elements not matched by the standard schema; these will be either extensions, or new schema contents not yet supported by this library.</summary>
    [XmlAnyElement]   public XmlElement  [] ExtensionElements;

  }

}
