using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel.Lists {

  [Serializable]
  internal sealed class LanguageList : ItemList, IResourceList<ISpokenLanguage> {

    [XmlElement("language")] public SpokenLanguage[] Items;

    #region IResourceList<ISpokenLanguage>

    uint? IResourceList<ISpokenLanguage>.Count => this.ListCount;

    uint? IResourceList<ISpokenLanguage>.Offset => this.ListOffset;

    IEnumerable<ISpokenLanguage> IResourceList<ISpokenLanguage>.Items => this.Items;

    #endregion

  }

}
