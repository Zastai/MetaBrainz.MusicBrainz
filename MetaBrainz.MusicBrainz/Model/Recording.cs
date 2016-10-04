using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;
using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  internal sealed class Recording : MbEntity, IRecording {

    #region XML Elements

    [XmlElement("alias-list")]     public AliasList      AliasList;
    [XmlElement("annotation")]     public Annotation     Annotation;
    [XmlElement("artist-credit")]  public ArtistCredit   ArtistCredit;
    [XmlElement("disambiguation")] public string         Disambiguation;
    [XmlElement("isrc-list")]      public IsrcList       IsrcList;
    [XmlElement("length")]         public uint           Length;
    [XmlIgnore]                    public bool           LengthSpecified;
    [XmlElement("rating")]         public Rating         Rating;
    [XmlElement("relation-list")]  public RelationList[] RelationList;
    [XmlElement("release-list")]   public ReleaseList    ReleaseList;
    [XmlElement("tag-list")]       public TagList        TagList;
    [XmlElement("title")]          public string         Title;
    [XmlElement("user-rating")]    public byte           UserRating;
    [XmlIgnore]                    public bool           UserRatingSpecified;
    [XmlElement("user-tag-list")]  public UserTagList    UserTagList;
    [XmlElement("video")]          public bool           Video;
    [XmlIgnore]                    public bool           VideoSpecified;

    [Obsolete] [XmlElement("puid-list")] public PuidList PuidList;

    #endregion

    #region IAnnotatedResource

    IAnnotation IAnnotatedResource.Annotation => this.Annotation;

    #endregion

    #region IRatedResource

    IRating IRatedResource.Rating => this.Rating;

    byte? IRatedResource.UserRating => this.UserRatingSpecified ? (byte?) this.UserRating : null;

    #endregion

    #region IRelatableResource

    IEnumerable<IRelationList> IRelatableResource.RelationList => this.RelationList;

    #endregion

    #region ITaggedResource

    IResourceList<ITag> ITaggedResource.TagList => this.TagList;

    IResourceList<IUserTag> ITaggedResource.UserTagList => this.UserTagList;

    #endregion

    #region ITitledResource

    IResourceList<IAlias> ITitledResource.AliasList => this.AliasList;

    string ITitledResource.Disambiguation => this.Disambiguation;

    string ITitledResource.Title => this.Title;

    #endregion

    #region IRecording

    IArtistCredit IRecording.ArtistCredit => this.ArtistCredit;

    IResourceList<IIsrc> IRecording.IsrcList => this.IsrcList;

    uint? IRecording.Length => this.LengthSpecified ? (uint?) this.Length : null;

    IResourceList<IRelease> IRecording.ReleaseList => this.ReleaseList;

    bool? IRecording.Video => this.VideoSpecified ? (bool?) this.Video : null;

    [Obsolete] IResourceList<IPuid> IRecording.PuidList => this.PuidList;

    #endregion

  }

}
