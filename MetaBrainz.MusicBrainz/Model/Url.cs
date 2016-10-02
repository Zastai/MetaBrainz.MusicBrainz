using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;
using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Url : MbEntity, IUrl {

    #region XML Elements

    [XmlElement("relation-list")] public RelationList[] RelationList;
    [XmlElement("resource")]      public string         Resource;

    #endregion

    #region IRelatableResource

    IEnumerable<IRelationList> IRelatableResource.RelationList => this.RelationList;

    #endregion

    #region IUrl

    Uri IUrl.Resource => new Uri(this.Resource);

    #endregion

  }

}
