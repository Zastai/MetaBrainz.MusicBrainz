using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Serializable]
  internal sealed class ArtistCredit : Item, IArtistCredit {

    #region XML Elements

    [XmlElement("name-credit")] public NameCredit[] NameCredits;

    #endregion

    #region IArtistCredit

    IEnumerable<INameCredit> IArtistCredit.NameCredits => this.NameCredits;

    #endregion

  }

}
