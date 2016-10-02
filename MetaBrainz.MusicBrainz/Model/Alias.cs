using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Alias : Item, IAlias {

    #region XML Attributes

    [XmlAttribute("begin-date")] public DateTime BeginDate;
    [XmlIgnore]                  public bool     BeginDateSpecified;
    [XmlAttribute("end-date")]   public DateTime EndDate;
    [XmlIgnore]                  public bool     EndDateSpecified;
    [XmlAttribute("locale")]     public string   Locale;
    [XmlAttribute("primary")]    public string   Primary;
    [XmlAttribute("sort-name")]  public string   SortName;
    [XmlAttribute("type")]       public string   Type;
    [XmlAttribute("type-id")]    public Guid     TypeId;
    [XmlIgnore]                  public bool     TypeIdSpecified;

    #endregion

    #region XML Elements

    [XmlText] public string Text;

    #endregion

    #region ITextResource

    string ITextResource.Text => this.Text;

    #endregion

    #region ITypedResource

    string ITypedResource.Type => this.Type;

    Guid? ITypedResource.TypeId => this.TypeIdSpecified ? (Guid?) this.TypeId : null;

    #endregion

    #region IAlias

    DateTime? IAlias.BeginDate => this.BeginDateSpecified ? (DateTime?) this.BeginDate : null;

    DateTime? IAlias.EndDate => this.EndDateSpecified ? (DateTime?) this.EndDate : null;

    string IAlias.Locale => this.Locale;

    string IAlias.Primary => this.Primary;

    string IAlias.SortName => this.SortName;

    #endregion

  }

}
