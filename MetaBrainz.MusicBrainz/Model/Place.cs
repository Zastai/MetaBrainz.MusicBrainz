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
  internal sealed class Place : MbEntity, IPlace {

    #region XML Attributes

    [XmlAttribute("type")]    public string Type;
    [XmlAttribute("type-id")] public Guid   TypeId;
    [XmlIgnore]               public bool   TypeIdSpecified;

    #endregion

    #region XML Elements

    [XmlElement("address")]        public string         Address;
    [XmlElement("alias-list")]     public AliasList      AliasList;
    [XmlElement("annotation")]     public Annotation     Annotation;
    [XmlElement("area")]           public Area           Area;
    [XmlElement("coordinates")]    public Coordinates    Coordinates;
    [XmlElement("disambiguation")] public string         Disambiguation;
    [XmlElement("life-span")]      public LifeSpan       LifeSpan;
    [XmlElement("name")]           public string         Name;
    [XmlElement("relation-list")]  public RelationList[] RelationList;
    [XmlElement("tag-list")]       public TagList        TagList;
    [XmlElement("user-tag-list")]  public UserTagList    UserTagList;

    #endregion

    #region IAnnotatedResource

    IAnnotation IAnnotatedResource.Annotation => this.Annotation;

    #endregion

    #region INamedResource

    IResourceList<IAlias> INamedResource.AliasList => this.AliasList;

    string INamedResource.Disambiguation => this.Disambiguation;

    string INamedResource.Name => this.Name;

    string INamedResource.SortName => this.Name;

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

    #region IPlace

    string IPlace.Address => this.Address;

    IArea IPlace.Area => this.Area;

    ICoordinates IPlace.Coordinates => this.Coordinates;

    ILifeSpan IPlace.LifeSpan => this.LifeSpan;

    #endregion

  }

}
