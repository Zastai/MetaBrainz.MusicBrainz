using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;
using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Work : MbEntity, IWork {

    #region XML Attributes

    [XmlAttribute("type")]    public string Type;
    [XmlAttribute("type-id")] public Guid   TypeId;
    [XmlIgnore]               public bool   TypeIdSpecified;

    #endregion

    #region XML Elements

    [XmlElement("alias-list")]     public AliasList         AliasList;
    [XmlElement("annotation")]     public Annotation        Annotation;
    [XmlElement("artist-credit")]  public ArtistCredit      ArtistCredit;
    [XmlElement("attribute-list")] public WorkAttributeList AttributeList;
    [XmlElement("disambiguation")] public string            Disambiguation;
    [XmlElement("iswc")]           public string            IswcCode;
    [XmlElement("iswc-list")]      public IswcList          IswcList;
    [XmlElement("language")]       public string            Language;
    [XmlElement("rating")]         public Rating            Rating;
    [XmlElement("relation-list")]  public RelationList[]    RelationList;
    [XmlElement("tag-list")]       public TagList           TagList;
    [XmlElement("title")]          public string            Title;
    [XmlElement("user-rating")]    public byte              UserRating;
    [XmlIgnore]                    public bool              UserRatingSpecified;
    [XmlElement("user-tag-list")]  public UserTagList       UserTagList;

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

    #region ITypedResource

    string ITypedResource.Type => this.Type;

    Guid? ITypedResource.TypeId => this.TypeIdSpecified ? (Guid?) this.TypeId : null;

    #endregion

    #region IWork

    IArtistCredit IWork.ArtistCredit => this.ArtistCredit;

    IResourceList<IWorkAttribute> IWork.AttributeList => this.AttributeList;

    string IWork.IswcCode => this.IswcCode;

    IResourceList<ITextResource> IWork.IswcList => this.IswcList;

    string IWork.Language => this.Language;

    #endregion

  }

}
