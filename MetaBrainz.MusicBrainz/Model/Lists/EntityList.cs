using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.Resources;

namespace MetaBrainz.MusicBrainz.Model.Lists {

  [Serializable]
  public class EntityList : ItemList, IResourceList<IMbEntity> {

    [XmlElement("area",          typeof(Area))]
    [XmlElement("artist",        typeof(Artist))]
    [XmlElement("event",         typeof(Event))]
    [XmlElement("instrument",    typeof(Instrument))]
    [XmlElement("label",         typeof(Label))]
    [XmlElement("place",         typeof(Place))]
    [XmlElement("recording",     typeof(Recording))]
    [XmlElement("release",       typeof(Release))]
    [XmlElement("release-group", typeof(ReleaseGroup))]
    [XmlElement("series",        typeof(Series))]
    [XmlElement("work",          typeof(Work))]
    public MbEntity[] Items;

    #region IResourceList<IMbEntity>

    uint? IResourceList<IMbEntity>.Count => this.ListCount;

    uint? IResourceList<IMbEntity>.Offset => this.ListOffset;

    IEnumerable<IMbEntity> IResourceList<IMbEntity>.Items => this.Items;

    #endregion

  }

}
