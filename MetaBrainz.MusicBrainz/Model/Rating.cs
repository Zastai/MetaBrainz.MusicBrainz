using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Rating : Item, IRating {

    #region XML Attributes

    [XmlAttribute("votes-count")] public uint VoteCount;

    #endregion

    #region XML Elements

    [XmlText] public string Text;

    #endregion

    #region IRating

    string IRating.Text => this.Text;

    uint IRating.VoteCount => this.VoteCount;

    #endregion

  }

}
