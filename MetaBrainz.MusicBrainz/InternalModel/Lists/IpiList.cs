using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Serializable]
  public sealed class IpiList : Item, IStringList {

    [XmlElement("ipi")] public string[] Items;

    #region IStringList

    int? IStringList.Count => this.Items?.Length;

    IEnumerable<string> IStringList.Items => this.Items;

    #endregion

  }

}
