using System;

using JetBrains.Annotations;

namespace MetaBrainz.MusicBrainz;

/// <summary>Enumeration of the scopes available through MusicBrainz OAuth2 authentication.</summary>
[Flags]
[PublicAPI]
public enum AuthorizationScope {

  /// <summary>No authorization requested.</summary>
  None          = 0,

  /// <summary>Request all available permissions (not recommended).</summary>
  Everything    = -1,

  /// <summary>View the user's public profile information (username, age, country, homepage).</summary>
  Profile       = 1 << 0,

  /// <summary>View the user's email address.</summary>
  EMail         = 1 << 1,

  /// <summary>View and modify the user's private tags.</summary>
  Tag           = 1 << 2,

  /// <summary>View and modify the user's private ratings.</summary>
  Rating        = 1 << 3,

  /// <summary>View and modify the user's private collections.</summary>
  Collection    = 1 << 4,

  /// <summary>Submit new ISRCs to the database.</summary>
  SubmitIsrc    = 1 << 5,

  /// <summary>Submit barcodes to the database.</summary>
  SubmitBarcode = 1 << 6,

}
