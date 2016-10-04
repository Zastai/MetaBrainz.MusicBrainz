using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class ArtistCredit : Item, IArtistCredit {

    #region XML Elements

    [XmlElement("name-credit")] public NameCredit[] NameCredits;

    #endregion

    #region IArtistCredit

    IEnumerable<INameCredit> IArtistCredit.NameCredits => this.NameCredits;

    #endregion

  }

}
