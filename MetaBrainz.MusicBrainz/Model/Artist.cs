using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;
using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Artist : MbEntity, IArtist {

    #region XML Attributes

    [XmlAttribute("type")]    public string Type;
    [XmlAttribute("type-id")] public Guid   TypeId;
    [XmlIgnore]               public bool   TypeIdSpecified;

    #endregion

    #region XML Elements

    [XmlElement("alias-list")]         public AliasList        AliasList;
    [XmlElement("annotation")]         public Annotation       Annotation;
    [XmlElement("area")]               public Area             Area;
    [XmlElement("begin-area")]         public Area             BeginArea;
    [XmlElement("country")]            public string           Country;
    [XmlElement("disambiguation")]     public string           Disambiguation;
    [XmlElement("end-area")]           public Area             EndArea;
    [XmlElement("gender")]             public Gender           Gender;
    [XmlElement("ipi")]                public string           Ipi;
    [XmlElement("ipi-list")]           public IpiList          IpiList;
    [XmlElement("isni-list")]          public IsniList         IsniList;
    [XmlElement("label-list")]         public LabelList        LabelList;
    [XmlElement("life-span")]          public LifeSpan         Lifespan;
    [XmlElement("name")]               public string           Name;
    [XmlElement("rating")]             public Rating           Rating;
    [XmlElement("recording-list")]     public RecordingList    RecordingList;
    [XmlElement("relation-list")]      public RelationList[]   RelationList;
    [XmlElement("release-list")]       public ReleaseList      ReleaseList;
    [XmlElement("release-group-list")] public ReleaseGroupList ReleaseGroupList;
    [XmlElement("sort-name")]          public string           SortName;
    [XmlElement("tag-list")]           public TagList          TagList;
    [XmlElement("user-rating")]        public byte             UserRating;
    [XmlIgnore]                        public bool             UserRatingSpecified;
    [XmlElement("user-tag-list")]      public UserTagList      UserTagList;
    [XmlElement("work-list")]          public WorkList         WorkList;

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

    #region IArtist

    IArea IArtist.Area => this.Area;

    IArea IArtist.BeginArea => this.BeginArea;

    string IArtist.Country => this.Country;

    IArea IArtist.EndArea => this.EndArea;

    ITextResource IArtist.Gender => this.Gender;

    string IArtist.Ipi => this.Ipi;

    IStringList IArtist.IpiList => this.IpiList;

    IStringList IArtist.IsniList => this.IsniList;

    IResourceList<ILabel> IArtist.LabelList => this.LabelList;

    ILifeSpan IArtist.Lifespan => this.Lifespan;

    IResourceList<IRecording> IArtist.RecordingList => this.RecordingList;

    IResourceList<IRelease> IArtist.ReleaseList => this.ReleaseList;

    IResourceList<IReleaseGroup> IArtist.ReleaseGroupList => this.ReleaseGroupList;

    IResourceList<IWork> IArtist.WorkList => this.WorkList;

    #endregion

  }

}
