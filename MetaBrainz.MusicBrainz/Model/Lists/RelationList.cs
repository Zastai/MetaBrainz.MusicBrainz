using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class RelationList : ItemList, IRelationList {

    [XmlAttribute("target-type")] public string TargetType;

    [XmlElement("relation")] public Relation[] Items;

    #region IResourceList<IRelation>

    uint? IResourceList<IRelation>.Count => this.ListCount;

    uint? IResourceList<IRelation>.Offset => this.ListOffset;

    IEnumerable<IRelation> IResourceList<IRelation>.Items => this.Items;

    #endregion

    #region IRelationList

    string IRelationList.TargetType => this.TargetType;

    #endregion

  }

}
