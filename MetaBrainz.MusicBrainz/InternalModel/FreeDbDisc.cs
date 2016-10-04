using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.InternalModel.Lists;
using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Serializable]
  internal sealed class FreeDbDisc : Item, IFreeDbDisc {

    #region XML Elements

    [XmlElement("artist")]     public string          Artist;
    [XmlElement("category")]   public string          Category;
    [XmlElement("comment")]    public string          Comment;
    [XmlElement("title")]      public string          Title;
    [XmlElement("track-list")] public SimpleTrackList TrackList;
    [XmlElement("year")]       public string          Year;

    #endregion

    #region IFreeDbDisc

    string IFreeDbDisc.Artist => this.Artist;

    string IFreeDbDisc.Category => this.Category;

    string IFreeDbDisc.Comment => this.Comment;

    string IFreeDbDisc.Title => this.Title;

    IResourceList<ISimpleTrackInfo> IFreeDbDisc.TrackList => this.TrackList;

    string IFreeDbDisc.Year => this.Year;

    #endregion

  }

}
