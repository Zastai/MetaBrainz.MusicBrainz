# MetaBrainz.MusicBrainz User Guide

## Getting Started

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

### Software Suggestion

When playing around with these APIs and familiarizing yourself with the various objects involved, it can be very useful to get a
nice overview of the objects' structures. Rather than writing some code and relying on the debugger's interface to browse the
contents, I can strongly recommend [LINQPad][LINQPad]. It's free (although there is a premium version with more advanced features).

With it, you just set up a query that references the `MetaBrainz.MusicBrainz` assembly and the corresponding namespace.
Then you write the code to set up a query object (like above), and call one of its methods, chaining a call to `Dump()` to those
for which you want the result to be formatted in the output window. Pressing `F5` will then run that code, and provide you with
a nice view of the object(s) in question.

For example:
```c#
var q = new Query("Red Stapler", "19.99", "mailto:milton.waddams@initech.com");
q.FindReleaseGroups("releasegroup:\"Office Space\"").Dump();
```

[LINQPad]: https://www.linqpad.net/

## Accessing Data

### Lookup: By MBID

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

### Browse: Via a Related Entity

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

### Find: Text Search

You can also search for entities using a textual search query, using the `FindXxx()` methods. When you pass `true` as the `simple`
parameter, the query is a simple bit of text that gets matched against the main fields for an entity. Otherwise, the more complex
[Indexed Search syntax][ISSyntax] applies (note: the list of fields for each entity is also included in the corresponding `Find`
method's XML docs).

Calls to the Find API methods return objects of type `ISearchResults<ISearchResult<T>>`, where `T` is the specific entity type
you're looking for. `ISearchResults` is more or less identical to `IBrowseResults` (see above); it just adds a `Created` property
indicating when that result set was generated by the search server. `ISearchResult` combines the found entity (in its `Item`)
property with a search score (in its `Score` property) indicating how close a match it is to your query.

Note that unlike Find or Browse, searches return cached subsets of information; to get more detailed results, use the returned
MBIDs to perform an additional Lookup.

For example:
```c#
var elvises = q.FindArtist("Elvis", simple: true); // at the time of writing, TotalResults is 248 for this query
var elvisesFromTupelo = q.FindArtist("name:Elvis AND beginarea:Tupelo"); // but for this one it's 1 
```

[ISSyntax]: https://musicbrainz.org/doc/Indexed_Search_Syntax

### Unhandled Information

Almost all objects returned by the API implement `IJsonBasedObject`. Its `UnhandledProperties` property provides access to a
(read-only) dictionary containing any and all JSON properties that were returned by the server but not recognized by the library.
Under normal circumstances, this dictionary should be `null`; if it isn't, please [file a ticket][GHIssues] to add support for the
field(s) in question.

[GHIssues]: https://github.com/Zastai/MetaBrainz.MusicBrainz/issues

## Authentication

When you want to submit data or retrieve tags/genres/ratings/collections added by a user, you need to set up authentication for
your requests. This can be done using the `OAuth2` class to generate an access token, and assigning it to the `BearerToken`
property of a `Query` object.

### Initial Permissions

As a first step, you will need to go to [your MusicBrainz account page][MBAccount] and register an application. That provides you
with both a client ID and a client secret.

The second is to get the user to provide your application with the required authorization. You do this by calling the
`CreateAuthorizationRequest` method, passing the callback URI you configured for your application in step one. If you set up an
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

[MBAccount]: https://musicbrainz.org/account/applications

### Refreshing Permissions

An access token is typically only valid for an hour. However, you can use the refresh token (obtained at the same time as the
access token) and your client secret to generate a new access token without user interaction. This is done by calling the
`RefreshBearerToken` method.

```c#
var at = oa.RefreshBearerToken(refreshToken, clientSecret);
q.BearerToken = at.AccessToken;
```

Note that if this method fails, it may be required to use `GetBearerToken` again, to have the user re-confirm your access
privileges.
