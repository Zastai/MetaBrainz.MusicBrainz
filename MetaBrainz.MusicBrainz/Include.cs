using System;
using System.Diagnostics.CodeAnalysis;

namespace MetaBrainz.MusicBrainz {

  /// <summary>Enumeration of additional information that can be requested to be included in query results.</summary>
  [Flags]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public enum Include : long {

    /// <summary>No extra information requested.</summary>
    None = 0,

    #region Linked Entities

    /// <summary>Include information about directly-linked artists.</summary>
    Artists       = 1L << 0,

    /// <summary>Include information about directly-linked collections.</summary>
    Collections   = 1L << 1,

    /// <summary>Include information about directly-linked labels.</summary>
    Labels        = 1L << 2,

    /// <summary>Include information about directly-linked recordings.</summary>
    Recordings    = 1L << 3,

    /// <summary>Include information about directly-linked release groups.</summary>
    ReleaseGroups = 1L << 4,

    /// <summary>Include information about directly-linked releases.</summary>
    Releases      = 1L << 5,

    /// <summary>Include information about directly-linked works.</summary>
    Works         = 1L << 6,

    #endregion

    #region Special Cases

    /// <summary>Include artist credits for all releases and recordings.</summary>
    ArtistCredits      = 1L << 16,

    /// <summary>Include Disc IDs for all media in the release.</summary>
    /// <remarks>Only valid on queries for releases; implies <see cref="Media"/>.</remarks>
    DiscIds            = 1L << 17,

    /// <summary>Include ISRC values for all recordings.</summary>
    /// <remarks>Only valid in combination with <see cref="Recordings"/>.</remarks>
    Isrcs              = 1L << 18,

    /// <summary>Include information about media for all releases (track count and format).</summary>
    /// <remarks>Only valid on queries for releases; implied by either <see cref="DiscIds"/> or <see cref="Recordings"/>.</remarks>
    Media              = 1L << 19,

    /// <summary>Include private collections of the authenticated user.</summary>
    /// <remarks>Requires authentication. Applies only to queries for collections.</remarks>
    UserCollections    = 1L << 20,

    /// <summary>Only include releases where the artist is credited on one or more tracks, but not on the release itself.</summary>
    /// <remarks>Only valid on artist queries, and only in combination with <see cref="Releases"/>.</remarks>
    VariousArtists     = 1L << 21,

    #endregion

    #region Optional Info

    /// <summary>Include aliases.</summary>
    /// <remarks>Aliases are not ordered, and only valid on queries for areas, artists, labels or works.</remarks>
    Aliases     = 1L << 28,

    /// <summary>Include the annotation.</summary>
    Annotation  = 1L << 29,

    /// <summary>Include ratings.</summary>
    Ratings     = 1L << 30,

    /// <summary>Include tags.</summary>
    Tags        = 1L << 31,

    /// <summary>Like <see cref="Ratings"/>, but only returns the rating(s) set by the authenticated user.</summary>
    /// <remarks>Requires authentication.</remarks>
    UserRatings = 1L << 32,

    /// <summary>Like <see cref="Tags"/>, but only returns the tag(s) set by the authenticated user.</summary>
    /// <remarks>Requires authentication.</remarks>
    UserTags    = 1L << 33,

    #endregion

    #region Relationships

    /// <summary>Include information about relationships with areas.</summary>
    AreaRelationships           = 1L << 40,

    /// <summary>Include information about relationships with artists.</summary>
    ArtistRelationships         = 1L << 41,

    /// <summary>Include relationships with events.</summary>
    EventRelationships          = 1L << 42,

    /// <summary>Include relationships with instruments.</summary>
    InstrumentRelationships     = 1L << 43,

    /// <summary>Include information about relationships with labels.</summary>
    LabelRelationships          = 1L << 44,

    /// <summary>Include information about relationships with places.</summary>
    PlaceRelationships          = 1L << 45,

    /// <summary>Include information about relationships involving the recordings on the release.</summary>
    /// <remarks>
    ///   Only valid on queries for releases. Will have no real effect unless information about the release's recordings (via <see cref="Recordings"/>)
    ///   and one or more relationships (via <see cref="ArtistRelationships"/>, <see cref="WorkRelationships"/>, ...) is being requested at the same time.
    /// </remarks>
    RecordingLevelRelationships = 1L << 46,

    /// <summary>Include relationships with recordings.</summary>
    RecordingRelationships      = 1L << 47,

    /// <summary>Include information about relationships with release groups.</summary>
    ReleaseGroupRelationships   = 1L << 48,

    /// <summary>Include information about relationships with releases.</summary>
    ReleaseRelationships        = 1L << 49,

    /// <summary>Include information about relationships with releases.</summary>
    SeriesRelationships         = 1L << 50,

    /// <summary>Include information about relationships with URLs.</summary>
    UrlRelationships            = 1L << 51,

    /// <summary>Include information about relationships involving the works associated with the recordings.</summary>
    /// <remarks>
    ///   Only valid on queries for releases or recordings. Will have no real effect unless information about related works (<see cref="WorkRelationships"/>)
    ///   and one or more relationships (<see cref="ArtistRelationships"/>, ...) is being requested at the same time.
    /// </remarks>
    WorkLevelRelationships      = 1L << 52,

    /// <summary>Include information about relationships with works.</summary>
    WorkRelationships           = 1L << 53,

    #endregion

  }

}
