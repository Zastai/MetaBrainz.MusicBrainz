using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Editor : Item {

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

  }

}
