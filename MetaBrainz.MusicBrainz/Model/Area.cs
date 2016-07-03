using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  [SuppressMessage("ReSharper", "UnassignedField.Global")]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  public sealed class Area : MbEntity {

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

  }

}
