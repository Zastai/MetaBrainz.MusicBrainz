using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;
using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Instrument : MbEntity, IInstrument {

    #region XML Attributes

    [XmlAttribute("type")]    public string Type;
    [XmlAttribute("type-id")] public Guid   TypeId;
    [XmlIgnore]               public bool   TypeIdSpecified;

    #endregion

    #region XML Elements

    [XmlElement("alias-list")]     public AliasList      AliasList;
    [XmlElement("annotation")]     public Annotation     Annotation;
    [XmlElement("description")]    public string         Description;
    [XmlElement("disambiguation")] public string         Disambiguation;
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

    #region IInstrument

    string IInstrument.Description => this.Description;

    #endregion

  }

}
