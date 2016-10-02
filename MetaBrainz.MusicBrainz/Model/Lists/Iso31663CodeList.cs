using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class Iso31663CodeList : Item, IStringList {

    [XmlElement("iso-3166-3-code")] public string[] Items;

    #region IStringList

    int? IStringList.Count => this.Items?.Length;

    IEnumerable<string> IStringList.Items => this.Items;

    #endregion

  }

}
