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
  internal sealed class Event : MbEntity, IEvent {

    #region XML Elements

    [XmlAttribute("type")]    public string Type;
    [XmlAttribute("type-id")] public Guid   TypeId;
    [XmlIgnore]               public bool   TypeIdSpecified;

    #endregion

    #region XML Elements

    [XmlElement("alias-list")]     public AliasList      AliasList;
    [XmlElement("annotation")]     public Annotation     Annotation;
    [XmlElement("cancelled")]      public bool           Cancelled;
    [XmlIgnore]                    public bool           CancelledSpecified;
    [XmlElement("disambiguation")] public string         Disambiguation;
    [XmlElement("life-span")]      public LifeSpan       LifeSpan;
    [XmlElement("name")]           public string         Name;
    [XmlElement("rating")]         public Rating         Rating;
    [XmlElement("relation-list")]  public RelationList[] RelationList;
    [XmlElement("setlist")]        public string         Setlist;
    [XmlElement("tag-list")]       public TagList        TagList;
    [XmlElement("time")]           public string         Time;
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

    string INamedResource.SortName => this.Name;

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

    #region IEvent

    bool? IEvent.Cancelled => this.CancelledSpecified ? (bool?) this.Cancelled : null;

    ILifeSpan IEvent.LifeSpan => this.LifeSpan;

    string IEvent.Setlist => this.Setlist;

    string IEvent.Time => this.Time;

    #endregion

  }

}
