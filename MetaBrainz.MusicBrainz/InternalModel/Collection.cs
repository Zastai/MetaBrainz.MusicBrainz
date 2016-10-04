using System;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

using MetaBrainz.MusicBrainz.InternalModel.Lists;
using MetaBrainz.MusicBrainz.Resources;

#pragma warning disable 649

namespace MetaBrainz.MusicBrainz.InternalModel {

  [Serializable]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  public sealed class Collection : MbEntity, ICollection {

    #region XML Attributes

    [XmlAttribute("entity-type")] public string EntityType;
    [XmlAttribute("type")]        public string Type;
    [XmlAttribute("type-id")]     public Guid   TypeId;
    [XmlIgnore]                   public bool   TypeIdSpecified;

    #endregion

    #region XML Elements

    [XmlElement("editor")] public string Editor;
    [XmlElement("name")]   public string Name;

    [XmlElement("area-list")]          public AreaList         AreaList;
    [XmlElement("artist-list")]        public ArtistList       ArtistList;
    [XmlElement("event-list")]         public EventList        EventList;
    [XmlElement("instrument-list")]    public InstrumentList   InstrumentList;
    [XmlElement("label-list")]         public LabelList        LabelList;
    [XmlElement("place-list")]         public PlaceList        PlaceList;
    [XmlElement("recording-list")]     public RecordingList    RecordingList;
    [XmlElement("release-list")]       public ReleaseList      ReleaseList;
    [XmlElement("release-group-list")] public ReleaseGroupList ReleaseGroupList;
    [XmlElement("series-list")]        public SeriesList       SeriesList;
    [XmlElement("work-list")]          public WorkList         WorkList;

    #endregion

    #region ITypedResource

    string ITypedResource.Type => this.Type;

    Guid? ITypedResource.TypeId => this.TypeIdSpecified ? (Guid?) this.TypeId : null;

    #endregion

    #region ICollection

    string ICollection.Editor => this.Editor;

    string ICollection.EntityType => this.EntityType;

    string ICollection.Name => this.Name;

    IResourceList<IArea> ICollection.Areas => this.AreaList;

    IResourceList<IArtist> ICollection.Artists => this.ArtistList;

    IResourceList<IEvent> ICollection.Events => this.EventList;

    IResourceList<IInstrument> ICollection.Instruments => this.InstrumentList;

    IResourceList<ILabel> ICollection.Labels => this.LabelList;

    IResourceList<IPlace> ICollection.Places => this.PlaceList;

    IResourceList<IRecording> ICollection.Recordings => this.RecordingList;

    IResourceList<IRelease> ICollection.Releases => this.ReleaseList;

    IResourceList<IReleaseGroup> ICollection.ReleaseGroups => this.ReleaseGroupList;

    IResourceList<ISeries> ICollection.Series => this.SeriesList;

    IResourceList<IWork> ICollection.Works => this.WorkList;

    #endregion

  }

}
