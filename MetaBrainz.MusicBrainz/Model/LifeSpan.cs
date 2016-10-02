﻿using System;
using System.Xml.Serialization;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class LifeSpan : Item {

    #region XML Elements

    [XmlElement("begin")] public string Begin;
    [XmlElement("end")]   public string End;
    [XmlElement("ended")] public bool   Ended;
    [XmlIgnore]           public bool   EndedSpecified;

    #endregion

  }

}