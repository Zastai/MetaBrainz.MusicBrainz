using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  internal sealed class ArtistList : ItemList, IResourceList<IArtist> {

    [XmlElement("artist")] public Artist[] Items;

    #region IResourceList<IArtist>

    uint? IResourceList<IArtist>.Count => this.ListCount;

    uint? IResourceList<IArtist>.Offset => this.ListOffset;

    IEnumerable<IArtist> IResourceList<IArtist>.Items => this.Items;

    #endregion

  }

}
