using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Serializable]
  public sealed class RelationTarget : Entity, IRelationTarget {

    #region XML Elements

    [XmlText] public string Value;

    #endregion

    #region IRelationTarget

    string IRelationTarget.Value => this.Value;

    #endregion

  }

}
