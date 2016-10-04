using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Serializable]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  public sealed class CoverArtArchive : Item, ICoverArtArchive {

    #region XML Elements

    [XmlElement("artwork")]  public bool Artwork;
    [XmlElement("back")]     public bool Back;
    [XmlElement("count")]    public int  Count;
    [XmlElement("darkened")] public bool Darkened;
    [XmlIgnore]              public bool DarkenedSpecified;
    [XmlElement("front")]    public bool Front;

    #endregion

    #region ICoverArtArchive

    bool ICoverArtArchive.Artwork => this.Artwork;

    bool ICoverArtArchive.Back => this.Back;

    int ICoverArtArchive.Count => this.Count;

    bool? ICoverArtArchive.Darkened => this.DarkenedSpecified ? (bool?) this.Darkened : null;

    bool ICoverArtArchive.Front => this.Front;

    #endregion

  }

}
