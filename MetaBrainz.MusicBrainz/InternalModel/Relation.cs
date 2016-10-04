using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.InternalModel.Lists;
using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Serializable]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  internal sealed class Relation : Item, IRelation {

    #region XML Attributes

    [XmlAttribute("type")]    public string Type;
    [XmlAttribute("type-id")] public Guid   TypeId;
    [XmlIgnore]               public bool   TypeIdSpecified;

    #endregion

    #region XML Elements

    [XmlElement("attribute-list")] public RelationAttributeList AttributeList;
    [XmlElement("begin")]          public string                Begin;
    [XmlElement("direction")]      public string                Direction;
    [XmlElement("end")]            public string                End;
    [XmlElement("ended")]          public bool                  Ended;
    [XmlIgnore]                    public bool                  EndedSpecified;
    [XmlElement("ordering-key")]   public string                OrderingKey;
    [XmlElement("source-credit")]  public string                SourceCredit;
    [XmlElement("target")]         public RelationTarget        Target;
    [XmlElement("target-credit")]  public string                TargetCredit;

    [XmlElement("area",          typeof(Area))]
    [XmlElement("artist",        typeof(Artist))]
    [XmlElement("event",         typeof(Event))]
    [XmlElement("instrument",    typeof(Instrument))]
    [XmlElement("label",         typeof(Label))]
    [XmlElement("place",         typeof(Place))]
    [XmlElement("recording",     typeof(Recording))]
    [XmlElement("release",       typeof(Release))]
    [XmlElement("release-group", typeof(ReleaseGroup))]
    [XmlElement("series",        typeof(Series))]
    [XmlElement("work",          typeof(Work))]
    public MbEntity Item;

    #endregion

    #region ITypedResource

    string ITypedResource.Type => this.Type;

    Guid? ITypedResource.TypeId => this.TypeIdSpecified ? (Guid?) this.TypeId : null;

    #endregion

    #region IRelation

    IResourceList<IRelationAttribute> IRelation.AttributeList => this.AttributeList;

    string IRelation.Begin => this.Begin;

    string IRelation.Direction => this.Direction;

    string IRelation.End => this.End;

    bool? IRelation.Ended => this.EndedSpecified ? (bool?) this.Ended : null;

    IMbEntity IRelation.Item => this.Item;

    string IRelation.OrderingKey => this.OrderingKey;

    string IRelation.SourceCredit => this.SourceCredit;

    IRelationTarget IRelation.Target => this.Target;

    string IRelation.TargetCredit => this.TargetCredit;

    #endregion

  }

}
