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
  internal sealed class Label : MbEntity, ILabel {

    #region XML Attributes

    [XmlAttribute("type")]    public string Type;
    [XmlAttribute("type-id")] public Guid   TypeId;
    [XmlIgnore]               public bool   TypeIdSpecified;

    #endregion

    #region XML Elements

    [XmlElement("alias-list")]     public AliasList      AliasList;
    [XmlElement("annotation")]     public Annotation     Annotation;
    [XmlElement("area")]           public Area           Area;
    [XmlElement("country")]        public string         Country;
    [XmlElement("disambiguation")] public string         Disambiguation;
    [XmlElement("ipi")]            public string         Ipi;
    [XmlElement("ipi-list")]       public IpiList        IpiList;
    [XmlElement("label-code")]     public uint           LabelCode;
    [XmlElement("life-span")]      public LifeSpan       Lifespan;
    [XmlElement("name")]           public string         Name;
    [XmlElement("rating")]         public Rating         Rating;
    [XmlElement("relation-list")]  public RelationList[] RelationList;
    [XmlElement("release-list")]   public ReleaseList    ReleaseList;
    [XmlElement("sort-name")]      public string         SortName;
    [XmlElement("tag-list")]       public TagList        TagList;
    [XmlElement("user-rating")]    public byte           UserRating;
    [XmlIgnore]                    public bool           UserRatingSpecified;
    [XmlElement("user-tag-list")]  public UserTagList    UserTagList;

    #endregion

    #region IAnnotatedResource

    IAnnotation IAnnotatedResource.Annotation => this.Annotation;

    #endregion

    #region INamedResource

    IResourceList<IAlias> INamedResource.AliasList => this.AliasList;

    string INamedResource.Disambiguation => this.Disambiguation;

    string INamedResource.Name => this.Name;

    string INamedResource.SortName => this.SortName;

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

    #region ITypedResource

    string ITypedResource.Type => this.Type;

    Guid? ITypedResource.TypeId => this.TypeIdSpecified ? (Guid?) this.TypeId : null;

    #endregion

    #region ILabel

    IArea ILabel.Area => this.Area;

    string ILabel.Country => this.Country;

    string ILabel.Ipi => this.Ipi;

    IStringList ILabel.IpiList => this.IpiList;

    uint ILabel.LabelCode => this.LabelCode;

    ILifeSpan ILabel.Lifespan => this.Lifespan;

    IResourceList<IRelease> ILabel.ReleaseList => this.ReleaseList;

    #endregion

  }

}
