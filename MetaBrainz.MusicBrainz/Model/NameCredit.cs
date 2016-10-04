using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  internal sealed class NameCredit : Item, INameCredit {

    #region XML Attributes

    [XmlAttribute("joinphrase")] public string JoinPhrase;

    #endregion

    #region XML Elements

    [XmlElement("artist")] public Artist Artist;
    [XmlElement("name")]   public string Name;

    #endregion

    #region INameCredit

    IArtist INameCredit.Artist => this.Artist;

    string INameCredit.JoinPhrase => this.JoinPhrase;

    string INameCredit.Name => this.Name;

    #endregion

  }

}
