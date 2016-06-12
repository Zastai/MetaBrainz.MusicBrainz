using System;

namespace MetaBrainz.MusicBrainz {

  /// <summary>Enumeration of the scopes available through MusicBrainz OAuth2 authentication.</summary>
  [Flags]
  public enum AuthorizationScope : int {

    /// <summary>No authorization requested.</summary>
    None          = 0,

    /// <summary>Request all available permissions (not recommended).</summary>
    Everything    = -1,

    /// <summary>View the user's public profile information (username, age, country, homepage).</summary>
    Profile       = 0x00000001,

    /// <summary>View the user's email address.</summary>
    EMail         = 0x00000002,

    /// <summary>View and modify the user's private tags.</summary>
    Tag           = 0x00000004,

    /// <summary>View and modify the user's private ratings.</summary>
    Rating        = 0x00000008,

    /// <summary>View and modify the user's private collections.</summary>
    Collection    = 0x00000010,

    /// <summary>Submit new ISRCs to the database.</summary>
    SubmitIsrc    = 0x00000020,

    /// <summary>Submit barcodes to the database.</summary>
    SubmitBarcode = 0x00000040,

  }

}
