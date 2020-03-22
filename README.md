# MetaBrainz.MusicBrainz [![Build Status](https://img.shields.io/appveyor/build/zastai/metabrainz-musicbrainz)](https://ci.appveyor.com/project/Zastai/metabrainz-musicbrainz) [![NuGet Version](https://img.shields.io/nuget/v/MetaBrainz.MusicBrainz)](https://www.nuget.org/packages/MetaBrainz.MusicBrainz)

This is a .NET implementation of libmusicbrainz, wrapping the [MusicBrainz v2 API](https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2).
An attempt has been made to keep the same basic class hierarchy, but this library is based on the JSON interface, not the XML one, so there will be differences.
In addition, interfaces, not classes, are used for the public API (to allow more flexibility for the internals).

This also contains OAuth2 functionality.

## Release Notes

### v2.0 (2020-03-22)

- Target .NET Standard 2.0 and 2.1, .NET Core 2.1 and 3.1 (the current LTS releases) and .NET Framework 4.6.1, 4.7.2 and 4.8.
- Switched to `System.Text.Json` (instead of `NewtonSoft.Json`).
- Use `MetaBrainz.Common.Json`.
- Doc fixes.
- Use nullable reference types.
- OAuth2:
  - Split `AuthorizationToken` into `Interfaces.IAuthorizationToken` and `Objects.AuthorizationToken`.
  - Added async versions of `GetBearerToken()` and `RefreshBearerToken()`.
  - These now also return non-null values (an exception is thrown when no data is received).
- Fixed Zastai/MusicBrainz#5.
- Added a `TargetId` property to `IRelationship`.
- [MBS-10072](https://tickets.metabrainz.org/browse/MBS-10072): Adjust "begin/end area" handling for Artist.
- Adjusted the stringification of `INameCredit`.

### v1.1.3 (2019-11-08)

Added support for genres (fixing Zastai/MusicBrainz#4), and updated Newtonsoft.JSON to the latest version.

### v1.1.2 (2018-12-09)

Fixed Zastai/MusicBrainz#3, and disabled most JSON required/nullable validation to be more futureproof.

### v1.1.1 (2018-11-15)

Corrected the build so that the IntelliSense XML documentation is property built and packaged.

### v1.1 (2018-08-14)

- Zastai/MusicBrainz#2: Added Position field to Track entities.
- Allow artist-credit fields to be null.
- Bumped Newtonsoft.Json to the latest version (11.0.2).

### v1.0 (2018-01-21)

First official release.

- Dropped support for .NET framework versions before 4.0 (and 4.0 may be dropped in a later version); this allows for builds using .NET Core (which cannot target 2.0/3.5).
- Added support for .NET Standard 2.0; the only unsupported API is RawImage.Decode() (because System.Drawing.Image is not available).
