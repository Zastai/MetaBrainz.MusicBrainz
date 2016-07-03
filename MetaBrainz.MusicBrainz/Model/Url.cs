using System;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Model.Lists;

namespace MetaBrainz.MusicBrainz.Model {

  [Serializable]
  public class Url : MbEntity {

    #region XML Elements

    [XmlElement("relation-list")]       public RelationList[] RelationList;
    [XmlElement("resource")]            public string         Resource;

    #endregion

  }

}
