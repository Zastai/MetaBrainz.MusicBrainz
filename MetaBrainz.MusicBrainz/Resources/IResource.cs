using System.Collections.Generic;
using System.Xml;

namespace MetaBrainz.MusicBrainz.Resources {

  /// <summary>A resource.</summary>
  public interface IResource {

    /// <summary>Iterates over any XML attributes in the metadata for this resource that are not part of the standard schema.</summary>
    IEnumerable<XmlAttribute> ExtensionAttributes { get; }

    /// <summary>Iterates over any XML elements in the metadata for this resource that are not part of the standard schema.</summary>
    IEnumerable<XmlElement> ExtensionElements { get; }

  }

}
