using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Serializable]
  internal sealed class SeriesList : ItemList, IResourceList<ISeries> {

    [XmlElement("series")] public Series[] Items;

    #region IResourceList<ISeries>

    uint? IResourceList<ISeries>.Count => this.ListCount;

    uint? IResourceList<ISeries>.Offset => this.ListOffset;

    IEnumerable<ISeries> IResourceList<ISeries>.Items => this.Items;

    #endregion

  }

}
