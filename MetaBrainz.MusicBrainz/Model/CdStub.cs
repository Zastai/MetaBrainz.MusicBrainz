using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;
using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  internal sealed class CdStub : Entity, ICdStub {

    #region XML Elements

    [XmlElement("artist")]         public string          Artist;
    [XmlElement("barcode")]        public string          Barcode;
    [XmlElement("disambiguation")] public string          Comment;
    [XmlElement("title")]          public string          Title;
    [XmlElement("track-list")]     public SimpleTrackList TrackList;

    #endregion

    #region ICdStub

    string ICdStub.Artist => this.Artist;

    string ICdStub.Barcode => this.Barcode;

    string ICdStub.Comment => this.Comment;

    string ICdStub.Title => this.Title;

    IResourceList<ISimpleTrackInfo> ICdStub.TrackList => this.TrackList;

    #endregion

  }

}
