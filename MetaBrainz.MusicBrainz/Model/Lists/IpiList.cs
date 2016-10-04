using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class IpiList : Item, IStringList {

    [XmlElement("ipi")] public string[] Items;

    #region Implementation of IStringList

    int? IStringList.Count => this.Items?.Length;

    IEnumerable<string> IStringList.Items => this.Items;

    #endregion

  }

}
