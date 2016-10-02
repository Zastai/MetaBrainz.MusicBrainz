using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class RelationTarget : Entity, IRelationTarget {

    #region XML Elements

    [XmlText] public string Value;

    #endregion

    #region IRelationTarget

    string IRelationTarget.Value => this.Value;

    #endregion

  }

}
