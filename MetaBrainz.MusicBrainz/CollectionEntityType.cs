namespace MetaBrainz.MusicBrainz {

  /// <summary>The type of entity contained in a collection.</summary>
  public enum CollectionEntityType {

    /// <summary>The collection's entity type is not known.</summary>
    /// <remarks>Will normally only be the case when a new collection type is supported by the server, and this library has not been updated accordingly.</remarks>
    Unknown,

    /// <summary>The collection contains areas.</summary>
    Area,

    /// <summary>The collection contains artists.</summary>
    Artist,

    /// <summary>The collection contains events.</summary>
    Event,

    /// <summary>The collection contains instruments.</summary>
    Instrument,

    /// <summary>The collection contains labels.</summary>
    Label,

    /// <summary>The collection contains places.</summary>
    Place,

    /// <summary>The collection contains recordings.</summary>
    Recording,

    /// <summary>The collection contains releases.</summary>
    Release,

    /// <summary>The collection contains release groups.</summary>
    ReleaseGroup,

    /// <summary>The collection contains series.</summary>
    Series,

    /// <summary>The collection contains works.</summary>
    Work,

  }

}
