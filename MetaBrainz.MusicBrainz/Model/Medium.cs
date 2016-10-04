using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;
using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Medium : Item, IMedium {

    #region XML Elements

    [XmlElement("data-track-list")] public TrackList DataTrackList;
    [XmlElement("disc-list")]       public DiscList  DiscList;
    [XmlElement("format")]          public Format    Format;
    [XmlElement("position")]        public uint      Position;
    [XmlElement("pregap")]          public TrackInfo Pregap;
    [XmlElement("title")]           public string    Title;
    [XmlElement("track-list")]      public TrackList TrackList;

    #endregion

    #region Implementation of IMedium

    IResourceList<ITrackInfo> IMedium.DataTrackList => this.DataTrackList;

    IResourceList<IDisc> IMedium.DiscList => this.DiscList;

    ITextResource IMedium.Format => this.Format;

    uint IMedium.Position => this.Position;

    ITrackInfo IMedium.Pregap => this.Pregap;

    string IMedium.Title => this.Title;

    IResourceList<ITrackInfo> IMedium.TrackList => this.TrackList;

    #endregion

  }

}
