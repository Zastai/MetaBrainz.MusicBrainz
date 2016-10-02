using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public abstract class ItemList : Item {

    [XmlAttribute("count")]  public uint Count;
    [XmlIgnore]              public bool CountSpecified;
    [XmlAttribute("offset")] public uint Offset;
    [XmlIgnore]              public bool OffsetSpecified;

    protected uint? ListCount => this.CountSpecified ? (uint?) this.Count : null;

    protected uint? ListOffset => this.OffsetSpecified ? (uint?) this.Offset : null;

  }

}
