using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Release : MbEntity {

    #region XML Elements

    [XmlElement("alias-list")]          public AliasList          AliasList;
    [XmlElement("annotation")]          public Annotation         Annotation;
    [XmlElement("artist-credit")]       public ArtistCredit       ArtistCredit;
    [XmlElement("asin")]                public string             Asin;
    [XmlElement("barcode")]             public string             BarCode;
    [XmlElement("collection-list")]     public CollectionList     CollectionList;
    [XmlElement("country")]             public string             Country;
    [XmlElement("cover-art-archive")]   public CoverArtArchive    CoverArtArchive;
    [XmlElement("date")]                public string             Date;
    [XmlElement("disambiguation")]      public string             Disambiguation;
    [XmlElement("label-info-list")]     public LabelInfoList      LabelInfoList;
    [XmlElement("medium-list")]         public MediumList         MediumList;
    [XmlElement("packaging")]           public Packaging          Packaging;
    [XmlElement("quality")]             public string             Quality;
    [XmlElement("relation-list")]       public RelationList[]     RelationList;
    [XmlElement("release-event-list")]  public ReleaseEventList   ReleaseEventList;
    [XmlElement("release-group")]       public ReleaseGroup       ReleaseGroup;
    [XmlElement("status")]              public Status             Status;
    [XmlElement("tag-list")]            public TagList            TagList;
    [XmlElement("text-representation")] public TextRepresentation TextRepresentation;
    [XmlElement("title")]               public string             Title;
    [XmlElement("user-tag-list")]       public UserTagList        UserTagList;

    #endregion

  }

}
