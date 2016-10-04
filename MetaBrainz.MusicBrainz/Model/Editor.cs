using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;
using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Editor : Item, IEditor {

    #region XML Attributes

    [XmlAttribute("id")] public uint Id;

    #endregion

    #region XML Elements

    [XmlElement("age")]              public string          Age;
    [XmlElement("area")]             public Area            Area;
    [XmlElement("bio")]              public string          Bio;
    [XmlElement("edit-information")] public EditInformation EditInformation;
    [XmlElement("gender")]           public Gender          Gender;
    [XmlElement("homepage")]         public string          HomePage;
    [XmlElement("language-list")]    public LanguageList    LanguageList;
    [XmlElement("member-since")]     public DateTime        MemberSince;
    [XmlIgnore]                      public bool            MemberSinceSpecified;
    [XmlElement("name")]             public string          Name;
    [XmlElement("privs")]            public uint            Privileges;
    [XmlIgnore]                      public bool            PrivilegesSpecified;

    #endregion

    #region Implementation of IEditor

    uint IEditor.Id => this.Id;

    string IEditor.Age => this.Age;

    IArea IEditor.Area => this.Area;

    string IEditor.Bio => this.Bio;

    IEditInformation IEditor.EditInformation => this.EditInformation;

    ITextResource IEditor.Gender => this.Gender;

    string IEditor.HomePage => this.HomePage;

    IResourceList<ISpokenLanguage> IEditor.LanguageList => this.LanguageList;

    DateTime? IEditor.MemberSince => this.MemberSinceSpecified ? (DateTime?) this.MemberSince : null;

    string IEditor.Name => this.Name;

    uint? IEditor.Privileges => this.PrivilegesSpecified ? (uint?) this.Privileges : null;

    #endregion

  }

}
