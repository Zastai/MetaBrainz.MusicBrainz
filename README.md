# MetaBrainz.MusicBrainz [![Build Status](https://img.shields.io/appveyor/build/zastai/metabrainz-musicbrainz)](https://ci.appveyor.com/project/Zastai/metabrainz-musicbrainz) [![NuGet Version](https://img.shields.io/nuget/v/MetaBrainz.MusicBrainz)](https://www.nuget.org/packages/MetaBrainz.MusicBrainz)

This is a .NET implementation of libmusicbrainz, wrapping the
[MusicBrainz v2 API](https://musicbrainz.org/doc/Development/XML_Web_Service/Version_2).
An attempt has been made to keep the same basic class hierarchy, but this library is based on the JSON interface, not the XML one,
so there will be differences.
In addition, interfaces, not classes, are used for the public API (to allow more flexibility for the internals).

This also contains OAuth2 functionality.

## Examples of Use

### Getting started

All the real functionality is exposed by the `Query` class. There are some static properties to set up defaults for the web service
you want to access. If you are accessing the official MusicBrainz site, no changes are needed.

One such static property is `DelayBetweenRequests`, which defaults to 1.0. It ensures that any request made through a `Query`
object is issued at least that many seconds after the last. Setting it below 1.0 when accessing an official MusicBrainz server may
result in rate limiting getting applied to your requests (or, in case of continued abuse, IP bans). So avoid setting this too low,
except when accessing your own local server instance.

To start querying data, you construct a `Query` object, passing in information about your code to be used as the user agent for
requests (combined with information about this library).

```c#
var q = new Query("Red Stapler", "19.99", "mailto:milton.waddams@initech.com");
```

If you intend to create multiple `Query` objects, you can also set up a default user agent string as the static `DefaultUserAgent`
property, so you can just use `new Query()` to create instances. You must ensure that it's a valid user agent string
(`Name/Version (Contact)`); requests without one may be subject to rate limiting.

### Authentication

When you want to submit data or retrieve tags/genres/ratings/collections added by a user, you need to set up authentication for
your requests. This can be done using the `OAuth2` class to generate an access token, and assigning it to the `BearerToken`
property of a `Query` object.

#### Initial Permissions

As a first step, you will need to go to [your MusicBrainz account page](https://musicbrainz.org/account/applications) and register
an application. That provides you with both a client ID and a client secret.

The second is to get the user to provide your application with the required authorization. You do this by calling the
`OCreateAuthorizationRequest` method, passing the callback URI you configured for your application in step one. If you set up an
installed application without callback, pass `OAuth2.OutOfBandUri`. The other thing to pass is the scopes you want to request
permission for. This will return the URL to send the user to. If they confirm access, the callback URI will be accessed to provide
the authorization token; if there is no callback, they will be asked to copy the token off the page, to pass to your application
themselves.

```c#
var oa = new OAuth2()
// If using a local MusicBrainz server instance, make sure to set up the correct address and port.
var url = oa.CreateAuthorizationRequest(OAuth2.OutOfBandUri, AuthorizationScope.Ratings | AuthorizationScope.Tags);
```

Finally, you need to use the authorization token (which you should store along with the user's ID) to generate an access token.
The `GetBearerToken` is used for that; you pass in the token and your callback URI, to be given 3 pieces of information:

1. an access token
2. the access token's lifetime
3. a refresh token

The access token is what you need; assign it to the `BearerToken` property of your `Query` object. You may want to store the
refresh token with the rest of the user information, for later use.

```c#
var at = await oa.GetBearerTokenAsync(authorizationToken, clientSecret, OAuth2.OutOfBandUri);
q.BearerToken = at.AccessToken;
```

#### Refreshing Permissions

An access token is typically only valid for an hour. However, you can use the refresh token (obtained at the same time as the
access token) and your client secret to generate a new access token without user interaction. This is done by calling the
`RefreshBearerToken` method.

```c#
var at = oa.RefreshBearerToken(refreshToken, clientSecret);
q.BearerToken = at.AccessToken;
```

Note that if this method fails, it may be required to use `GetBearerToken` again, to have the user re-confirm your access
privileges.

### Accessing Data

#### Lookup: By MBID

When you know the MusicBrainz ID (MBID) of the entity (artist, recording, &hellip;), you can just use the lookup methods to get
information about it.

```c#
var artist = q.LookupArtist(mbid);
```

By default, only the main information about an entity is included. To get information about other entities, pass values for the
`inc` parameter. If this includes information about release groups, you can apply additional filtering using the `type` parameter;
if releases are requested, the same goes for the `status` parameter.

For example, to get information about Metallica, including all their live bootlegs, you would use:
```c#
var metallica = q.LookupArtist(new Guid("65f4f0c5-ef9e-490c-aee3-909e7ae6b2ab"), Include.Releases, ReleaseType.Live, ReleaseStatus.Bootleg);
```

And to include their EPs, you would use:
```c#
var metallica = q.LookupArtist(new Guid("65f4f0c5-ef9e-490c-aee3-909e7ae6b2ab"), Include.Releases, ReleaseType.Live, ReleaseStatus.Bootleg);
```

Note that included related information is always limited to 25 items. To get everything, you will need to use a Browse (see below).

#### Browse: Via a Related Entity

When you know the MBID of a entity (or have obtained an appropriate object via previous API calls), you can use one of the Browse
methods to obtain all related entities of a particular type.
These entities are returned in batches; you specify the number of items you want per batch on the original API call via the `limit`
parameter (1-100; default is 25). You can also use the `offset` parameter to skip results. The API call will return an object of
type `IBrowseResults<T>`, where `T` is the specific entity type you're browsing.

A browse result provides:

1. a `Limit` property; this contains the limit to use when scrolling further
2. a `NextOffset` property; this can be set to force a specific offset to scroll to (only used once)
3. an `Offset` property (read-only); this contains the current offset
4. a `Results` property (read-only); a read-only list containing the current set of matched entities
5. a `TotalResults` property (read-only); this contains the total number of matching results
6. `Next()` and `NextAsync()` methods, to scroll to the next page of results
6. `Previous()` and `PreviousAsync()` methods, to scroll to the previous page of results

For example:
```c#
var works = q.BrowseArtistWorks(new Guid("24f1766e-9635-4d58-a4d4-9413f9f98a4c"), limit: 30, offset: 1000);
// At the time of writing, works.TotalResults is 6911
foreach (var work in works.Results) {
  // Process Works 1001-1030
}
q.Limit = 70;
works = works.Next();
foreach (var work in works.Results) {
  // Process Works 1031-1100
}
works = works.Previous();
foreach (var work in works.Results) {
  // Process Works 961-1030
}
```


#### Find: Text Search

You can also search for entities using a textual search query, using the `FindXxx()` methods. When you pass `true` as the `simple`
parameter, the query is a simple bit of text that gets matched against the main fields for an entity. Otherwise, the more complex
[Indexed Search syntax](https://musicbrainz.org/doc/Indexed_Search_Syntax) applies (note: the list of fields for each entity is
also included in the corresponding Find method's XML docs).

Calls to the Find API methods return objects of type `ISearchResults<ISearchResult<T>>`, where `T` is the specific entity type
you're looking for. `ISearchResults` is more or less identical to `IBrowseResults`; it just adds a `Created` property indicating
when that result set was generated by the search server. `ISearchResult` combines the found entity (in its `Item`) property with
a search score (in its `Score` property) indicating how close a match it is to your query.

Note that unlike Find or Browse, searches return cached subsets of information; to get more detailed results, use the returned
MBIDs to perform an additional Lookup.

For example:
```c#
var elvises = q.FindArtist("Elvis", simple: true); // at the time of writing, TotalResults is 248 for this query
var elvisesFromTupelo = q.FindArtist("name:Elvis AND beginarea:Tupelo"); // but for this one it's 1 
```


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
