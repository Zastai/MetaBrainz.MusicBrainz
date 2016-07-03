using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Alias : Item {

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

  }

}
