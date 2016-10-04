using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Serializable]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  public abstract class ItemList : Item {

    [XmlAttribute("count")]  public uint Count;
    [XmlIgnore]              public bool CountSpecified;
    [XmlAttribute("offset")] public uint Offset;
    [XmlIgnore]              public bool OffsetSpecified;

    protected uint? ListCount => this.CountSpecified ? (uint?) this.Count : null;

    protected uint? ListOffset => this.OffsetSpecified ? (uint?) this.Offset : null;

  }

}
