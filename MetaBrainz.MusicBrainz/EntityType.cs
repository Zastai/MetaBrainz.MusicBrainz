namespace MetaBrainz.MusicBrainz {

  /// <summary>A MusicBrainz entity type.</summary>
  public enum EntityType {

    /// <summary>An unknown entity.</summary>
    Unknown,

    /// <summary>An area (i.e. an object implementing <see cref="Entities.IArea"/>).</summary>
    Area,

    /// <summary>An artist (i.e. an object implementing <see cref="Entities.IArtist"/>).</summary>
    Artist,

    /// <summary>A collection (i.e. an object implementing <see cref="Entities.ICollection"/>).</summary>
    Collection,

    /// <summary>An event (i.e. an object implementing <see cref="Entities.IEvent"/>).</summary>
    Event,

    /// <summary>An instrument (i.e. an object implementing <see cref="Entities.IInstrument"/>).</summary>
    Instrument,

    /// <summary>A label (i.e. an object implementing <see cref="Entities.ILabel"/>).</summary>
    Label,

    /// <summary>A place (i.e. an object implementing <see cref="Entities.IPlace"/>).</summary>
    Place,

    /// <summary>A recording (i.e. an object implementing <see cref="Entities.IRecording"/>).</summary>
    Recording,

    /// <summary>A release (i.e. an object implementing <see cref="Entities.IRelease"/>).</summary>
    Release,

    /// <summary>A release group (i.e. an object implementing <see cref="Entities.IReleaseGroup"/>).</summary>
    ReleaseGroup,

    /// <summary>A series (i.e. an object implementing <see cref="Entities.ISeries"/>).</summary>
    Series,

    /// <summary>A URL (i.e. an object implementing <see cref="Entities.IUrl"/>).</summary>
    Url,

    /// <summary>A work (i.e. an object implementing <see cref="Entities.IWork"/>).</summary>
    Work,

  }

}
