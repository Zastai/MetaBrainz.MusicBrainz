using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Serializable]
  internal sealed class Rating : Item, IRating {

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
