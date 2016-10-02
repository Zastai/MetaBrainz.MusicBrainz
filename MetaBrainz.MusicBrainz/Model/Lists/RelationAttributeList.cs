using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class RelationAttributeList : Item, IResourceList<IRelationAttribute> {

    [XmlElement("attribute")] public RelationAttribute[] Items;

    #region IResourceList<IRelationAttribute>

    uint? IResourceList<IRelationAttribute>.Count => (this.Items == null) ? null : (uint?) this.Items.Length;

    uint? IResourceList<IRelationAttribute>.Offset => null;

    IEnumerable<IRelationAttribute> IResourceList<IRelationAttribute>.Items => this.Items;

    #endregion

  }

}
