using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Serializable]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  public sealed class WorkAttribute : Item, IWorkAttribute {

    #region XML Attributes

    [XmlAttribute("type")]     public string Type;
    [XmlAttribute("type-id")]  public Guid   TypeId;
    [XmlIgnore]                public bool   TypeIdSpecified;
    [XmlAttribute("value-id")] public Guid   ValueId;
    [XmlIgnore]                public bool   ValueIdSpecified;

    #endregion

    #region XML Elements

    [XmlText] public string Text;

    #endregion

    #region ITypedResource

    string ITypedResource.Type => this.Type;

    Guid? ITypedResource.TypeId => this.TypeIdSpecified ? (Guid?) this.TypeId : null;

    #endregion

    #region IWorkAttribute

    Guid? IWorkAttribute.ValueId => this.ValueIdSpecified ? (Guid?) this.ValueId : null;

    string IWorkAttribute.Text => this.Text;

    #endregion

  }

}
