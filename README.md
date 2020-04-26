# MetaBrainz.MusicBrainz [![Build Status](https://img.shields.io/appveyor/build/zastai/metabrainz-musicbrainz)](https://ci.appveyor.com/project/Zastai/metabrainz-musicbrainz) [![NuGet Version](https://img.shields.io/nuget/v/MetaBrainz.MusicBrainz)](https://www.nuget.org/packages/MetaBrainz.MusicBrainz)

This is a .NET implementation of libmusicbrainz, wrapping the
[MusicBrainz v2 API](https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2).
An attempt has been made to keep the same basic class hierarchy, but this library is based on the JSON interface, not the XML one,
so there will be differences.
In addition, interfaces, not classes, are used for the public API (to allow more flexibility for the internals).

This also contains OAuth2 functionality.

## Release Notes

### v3.0.0 (2020-04-26)

This release has a completely rewritten JSON backend, using custom converters for flexibility.

Search processing was changed significantly. Instead of every searchable entity having a Score property, searches now return lists
of `ISearchResult<T>`, which contain the found item and its score. In addition, all the `FindXXX()` methods now take an optional
`simple` parameter, to disable the advanced query syntax.

This release also adds proper support for genres, including lookups, using a new `IGenre` interface.

Note that there are several breaking API changes in this release (hence the major version bump).

#### API Additions

- Entity Interface: `IAliasedEntity`
  - this contains the `Aliases` property, moved from `INamedEntity` and `ITitledEntity`
- Entity Interface: `IGenre` (derives from `ITag`)
- Property: `IArea.SortName`
- Property: `ILabel.SortName`
- Property: `IRelationship.AttributeIds`
- Search Interface: `ISearchAttribute<T>`
  - this adds an `Item` property of type `T` to the `Score` provided by the base `ISearchResult`
- Method: `Query.LookupGenre()`
- Method: `Query.LookupGenreAsync()`

#### API Removals

- Entity Interface: `IUserRating`
- Entity Interface: `IUserTag`
- Property: `ICollection.ContentTypeText`
  - `ContentType` will be set to `EntityType.Unknown` when an unknown value was found (which will then be stored in
    `UnhandledProperties`)
- Property: `IDisc.OffsetCount` (the count is already available via `Offsets`)
- Property: `IDiscIdLookupResult.Id`
- Property: `INamedEntity.Aliases` (moved to `IAliasedEntity`)
- Property: `IRelationship.TargetTypeText`
  - `TargetType` will be set to `EntityType.Unknown` when an unknown value was found (which will then be stored in
    `UnhandledProperties`)
- Property: `ITitledEntity.Aliases` (moved to `IAliasedEntity`)
- Search Interface: `IFoundAnnotation`
- Search Interface: `IFoundArea`
- Search Interface: `IFoundArtist`
- Search Interface: `IFoundCdStub`
- Search Interface: `IFoundEvent`
- Search Interface: `IFoundInstrument`
- Search Interface: `IFoundLabel`
- Search Interface: `IFoundPlace`
- Search Interface: `IFoundRecording`
- Search Interface: `IFoundRelease`
- Search Interface: `IFoundReleaseGroup`
- Search Interface: `IFoundSeries`
- Search Interface: `IFoundTag`
- Search Interface: `IFoundUrl`
- Search Interface: `IFoundWork`

#### API Changes

- Global:
  - the `MbId` property was renamed to just `Id`
  - the `Genres` and `UserGenres` properties now return objects of type `IGenre`
  - the `UserRating` property now returns objects of type `IRating` (with a null vote count)
  - the `UserTags` property now returns objects of type `ITag` (with a null vote count)
- `IAlias`:
  - the `Name` and `Primary` properties are no longer nullable
- `IAnnotation`:
  - moved from `MetaBrainz.MusicBrainz.Interfaces.Searches` to `MetaBrainz.MusicBrainz.Interfaces.Entities`
  - the `Type` property now has type `EntityType`
    - it will be set to `EntityType.Unknown` when an unknown value was found (which will then be stored in `UnhandledProperties`)
- `IAuthorizationToken`:
  - the `AccessToken`, `RefreshToken` and `TokenType` properties are no longer nullable
- `ICdStub`:
  - the `Id`, `Title` and `TrackCount` properties are no longer nullable
- `IDisc`:
  - the `Id` and `Offsets` properties are no longer nullable
- `IIsrc`:
  - the `Recordings` and `Value` properties are no longer nullable
- `IRating`:
  - the `VoteCount` property is now nullable
- `IRecording`:
  - the `Length` property is now a (nullable) `TimeSpan` instead of an `int`
- `IRelationship`:
  - the `TargetType` property is now nullable
- renamed `IRelease.BarCode` to `Barcode`
- `ISearchResult` is now an `IJsonBasedObject`
- `ISearchResults<T>.Created` is now a (nullable) `DateTimeOffset`
- `ISimpleTrack`:
  - the `Length` property is now a (nullable) `TimeSpan` instead of an `int`
- `ITag`:
  - the `Name` property is no longer nullable
  - the `VoteCount` property is now nullable
- `ITrack`:
  - the `Length` property is now a (nullable) `TimeSpan` instead of an `int`
- the `Query.FindXxx()` methods:
  - return value is now `ISearchResults<ISearchResult<IXxx>>`
    - in other words, the score is now moved up into a wrapper object
  - extra optional parameter `simple`
    - if passed as `true`, the advanced query syntax is disabled, making for a simpler use case
  - the doc comments were updated and tweaked

#### Other Changes

- a workaround for [SEARCH-444](https://tickets.metabrainz.org/browse/SEARCH-444) was added for URL entities too
- minor tweaks to the `OAuth2` class, slightly improving its async processing

#### Dependency Updates

- JetBrainz.Annotations → 2020.1.0
- MetaBrainz.Common.Json → 3.0.0
- System.Text.Json → 4.7.1


### v2.0.1 (2020-04-17)

#### GitHub Issues

- [#1](https://github.com/Zastai/MetaBrainz.MusicBrainz/issues/1): a workaround has been applied for
  [SEARCH-579](https://tickets.metabrainz.org/browse/SEARCH-579).

#### Other Changes

- `QueryException` now has its `HelpLink` field set to the contents of the `help` field in the JSON error response body.
- Fixed a build issue causing the XML documentation to be missing from the NuGet package.


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
- Fixed [issue #5](https://github.com/Zastai/MusicBrainz/issues/5).
- Added a `TargetId` property to `IRelationship`.
- [MBS-10072](https://tickets.metabrainz.org/browse/MBS-10072): Adjust "begin/end area" handling for Artist.
- Adjusted the stringification of `INameCredit`.

### v1.1.3 (2019-11-08)

Added support for genres (fixing [issue #4](https://github.com/Zastai/MusicBrainz/issues/4)), and updated Newtonsoft.JSON to the
latest version.

### v1.1.2 (2018-12-09)

Fixed [issue #3](https://github.com/Zastai/MusicBrainz/issues/3), and disabled most JSON required/nullable validation to be more
futureproof.

### v1.1.1 (2018-11-15)

Corrected the build so that the IntelliSense XML documentation is property built and packaged.

### v1.1 (2018-08-14)

- [Issue #2](https://github.com/Zastai/MusicBrainz/issues/2): Added Position field to Track entities.
- Allow artist-credit fields to be null.
- Bumped Newtonsoft.Json to the latest version (11.0.2).

### v1.0 (2018-01-21)

First official release.

- Dropped support for .NET framework versions before 4.0 (and 4.0 may be dropped in a later version); this allows for builds using
  .NET Core (which cannot target 2.0/3.5).
- Added support for .NET Standard 2.0; the only unsupported API is RawImage.Decode() (because System.Drawing.Image is not
  available).
