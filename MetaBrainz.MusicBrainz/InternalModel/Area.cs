using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.InternalModel.Lists;
using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Serializable]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  public sealed class Area : MbEntity, IArea {

    #region XML Attributes

    [XmlAttribute("type")]    public string Type;
    [XmlAttribute("type-id")] public Guid   TypeId;
    [XmlIgnore]               public bool   TypeIdSpecified;

    #endregion

    #region XML Elements

    [XmlElement("alias-list")]           public AliasList        AliasList;
    [XmlElement("annotation")]           public Annotation       Annotation;
    [XmlElement("disambiguation")]       public string           Disambiguation;
    [XmlElement("iso-3166-1-code-list")] public Iso31661CodeList Iso31661CodeList;
    [XmlElement("iso-3166-2-code-list")] public Iso31662CodeList Iso31662CodeList;
    [XmlElement("iso-3166-3-code-list")] public Iso31663CodeList Iso31663CodeList;
    [XmlElement("life-span")]            public LifeSpan         LifeSpan;
    [XmlElement("name")]                 public string           Name;
    [XmlElement("relation-list")]        public RelationList[]   RelationList;
    [XmlElement("sort-name")]            public string           SortName;
    [XmlElement("tag-list")]             public TagList          TagList;
    [XmlElement("user-tag-list")]        public UserTagList      UserTagList;

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

    #region IArea

    IStringList IArea.Iso31661CodeList => this.Iso31661CodeList;

    IStringList IArea.Iso31662CodeList => this.Iso31662CodeList;

    IStringList IArea.Iso31663CodeList => this.Iso31663CodeList;

    ILifeSpan IArea.LifeSpan => this.LifeSpan;

    #endregion

  }

}
