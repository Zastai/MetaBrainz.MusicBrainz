using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class LanguageList : ItemList, IResourceList<ISpokenLanguage> {

    [XmlElement("language")] public SpokenLanguage[] Items;

    #region IResourceList<ISpokenLanguage>

    uint? IResourceList<ISpokenLanguage>.Count => this.ListCount;

    uint? IResourceList<ISpokenLanguage>.Offset => this.ListOffset;

    IEnumerable<ISpokenLanguage> IResourceList<ISpokenLanguage>.Items => this.Items;

    #endregion

  }

}
