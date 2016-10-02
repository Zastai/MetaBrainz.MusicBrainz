using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  /// <summary>A generic item. Serves as a container for any otherwise unmatched attributes and elements.</summary>
  [Serializable]
  public abstract class Item : IResource {

    #region XML Attributes

    /// <summary>Attributes not matched by the standard schema; these will be either extensions, or new schema contents not yet supported by this library.</summary>
    [XmlAnyAttribute] public XmlAttribute[] ExtensionAttributes;

    #endregion

    #region XML Elements

    /// <summary>Elements not matched by the standard schema; these will be either extensions, or new schema contents not yet supported by this library.</summary>
    [XmlAnyElement] public XmlElement[] ExtensionElements;

    #endregion

    #region IResource

    IEnumerable<XmlAttribute> IResource.ExtensionAttributes => this.ExtensionAttributes;

    IEnumerable<XmlElement> IResource.ExtensionElements => this.ExtensionElements;

    #endregion

  }

}
