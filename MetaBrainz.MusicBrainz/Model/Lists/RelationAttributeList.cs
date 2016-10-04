using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  internal sealed class RelationAttributeList : Item, IResourceList<IRelationAttribute> {

    [XmlElement("attribute")] public RelationAttribute[] Items;

    #region IResourceList<IRelationAttribute>

    uint? IResourceList<IRelationAttribute>.Count => (this.Items == null) ? null : (uint?) this.Items.Length;

    uint? IResourceList<IRelationAttribute>.Offset => null;

    IEnumerable<IRelationAttribute> IResourceList<IRelationAttribute>.Items => this.Items;

    #endregion

  }

}
