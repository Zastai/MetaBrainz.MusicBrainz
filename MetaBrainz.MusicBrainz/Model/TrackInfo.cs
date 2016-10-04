using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  internal sealed class TrackInfo : Entity, ITrackInfo {

    #region XML Elements

    [XmlElement("artist-credit")] public ArtistCredit ArtistCredit;
    [XmlElement("length")]        public uint         Length;
    [XmlIgnore]                   public bool         LengthSpecified;
    [XmlElement("number")]        public string       Number;
    [XmlElement("position")]      public uint         Position;
    [XmlIgnore]                   public bool         PositionSpecified;
    [XmlElement("recording")]     public Recording    Recording;
    [XmlElement("title")]         public string       Title;

    #endregion

    #region ITrackInfo

    IArtistCredit ITrackInfo.ArtistCredit => this.ArtistCredit;

    uint? ITrackInfo.Length => this.LengthSpecified ? (uint?) this.Length : null;

    string ITrackInfo.Number => this.Number;

    uint? ITrackInfo.Position => this.PositionSpecified ? (uint?) this.Position : null;

    IRecording ITrackInfo.Recording => this.Recording;

    string ITrackInfo.Title => this.Title;

    #endregion

  }

}
