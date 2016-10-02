using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class LifeSpan : Item, ILifeSpan {

    #region XML Elements

    [XmlElement("begin")] public string Begin;
    [XmlElement("end")]   public string End;
    [XmlElement("ended")] public bool   Ended;
    [XmlIgnore]           public bool   EndedSpecified;

    #endregion

    #region ILifeSpan

    string ILifeSpan.Begin => this.Begin;

    string ILifeSpan.End => this.End;

    bool? ILifeSpan.Ended => this.EndedSpecified ? (bool?) this.Ended : null;

    #endregion

  }

}
