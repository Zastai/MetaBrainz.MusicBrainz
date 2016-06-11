using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Relation : Item {

    [XmlAttribute("type")]    public string Type;
    [XmlAttribute("type-id")] public Guid   TypeId;
    [XmlIgnore]               public bool   TypeIdSpecified;

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
    public MBEntity Item;

  }

}
