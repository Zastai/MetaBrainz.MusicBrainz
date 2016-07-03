using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Recording : MbEntity {

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

  }

}
