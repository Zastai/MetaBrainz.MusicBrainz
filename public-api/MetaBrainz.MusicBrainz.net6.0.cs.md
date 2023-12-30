# API Reference: MetaBrainz.MusicBrainz

## Assembly Attributes

```cs
[assembly: System.Runtime.InteropServices.ComVisibleAttribute(false)]
[assembly: System.Runtime.Versioning.TargetFrameworkAttribute(".NETCoreApp,Version=v6.0", FrameworkDisplayName = ".NET 6.0")]
```

## Namespace: MetaBrainz.MusicBrainz

### Type: AuthorizationScope

```cs
[System.FlagsAttribute]
public enum AuthorizationScope {

  Collection = 16,
  Email = 2,
  Everything = -1,
  None = 0,
  Profile = 1,
  Rating = 8,
  SubmitBarcode = 64,
  SubmitIsrc = 32,
  Tag = 4,

}
```

### Type: EntityType

```cs
public enum EntityType {

  Area = 1,
  Artist = 2,
  Collection = 3,
  Event = 4,
  Genre = 5,
  Instrument = 6,
  Label = 7,
  Place = 8,
  Recording = 9,
  Release = 10,
  ReleaseGroup = 11,
  Series = 12,
  Unknown = 0,
  Url = 13,
  Work = 14,

}
```

### Type: Include

```cs
[System.FlagsAttribute]
public enum Include : long {

  Aliases = 268435456L,
  Annotation = 536870912L,
  AreaRelationships = 1099511627776L,
  ArtistCredits = 65536L,
  ArtistRelationships = 2199023255552L,
  Artists = 1L,
  Collections = 2L,
  DiscIds = 131072L,
  EventRelationships = 4398046511104L,
  Genres = 17179869184L,
  InstrumentRelationships = 8796093022208L,
  Isrcs = 262144L,
  LabelRelationships = 17592186044416L,
  Labels = 4L,
  Media = 524288L,
  None = 0L,
  PlaceRelationships = 35184372088832L,
  Ratings = 1073741824L,
  RecordingLevelRelationships = 70368744177664L,
  RecordingRelationships = 140737488355328L,
  Recordings = 8L,
  ReleaseGroupRelationships = 281474976710656L,
  ReleaseGroups = 16L,
  ReleaseRelationships = 562949953421312L,
  Releases = 32L,
  SeriesRelationships = 1125899906842624L,
  Tags = 2147483648L,
  UrlRelationships = 2251799813685248L,
  UserCollections = 1048576L,
  UserGenres = 34359738368L,
  UserRatings = 4294967296L,
  UserTags = 8589934592L,
  VariousArtists = 2097152L,
  WorkLevelRelationships = 4503599627370496L,
  WorkRelationships = 9007199254740992L,
  Works = 64L,

}
```

### Type: OAuth2

```cs
public sealed class OAuth2 : System.IDisposable {

  public const string AuthorizationEndPoint = "/oauth2/authorize";

  public static readonly System.Uri OutOfBandUri;

  public const string RevokeEndPoint = "/oauth2/revoke";

  public const string TokenEndPoint = "/oauth2/token";

  public const string TokenRequestBodyType = "application/x-www-form-urlencoded";

  public static readonly System.Diagnostics.TraceSource TraceSource;

  public const string UserInfoEndPoint = "/oauth2/userinfo";

  string ClientId {
    public get;
    public set;
  }

  string DefaultClientId {
    public static get;
    public static set;
  }

  int DefaultPort {
    public static get;
    public static set;
  }

  string DefaultServer {
    public static get;
    public static set;
  }

  string DefaultUrlScheme {
    public static get;
    public static set;
  }

  int Port {
    public get;
    public set;
  }

  string Server {
    public get;
    public set;
  }

  string UrlScheme {
    public get;
    public set;
  }

  public OAuth2();

  public OAuth2(System.Net.Http.HttpClient client, bool takeOwnership = false);

  public void Close();

  public void ConfigureClient(System.Action<System.Net.Http.HttpClient>? code);

  public void ConfigureClientCreation(System.Func<System.Net.Http.HttpClient>? code);

  public System.Uri CreateAuthorizationRequest(System.Uri redirectUri, AuthorizationScope scope, string? state = null, string? challenge = null, string? challengeMethod = null, bool offlineAccess = false, bool forcePrompt = false);

  public sealed override void Dispose();

  protected override void Finalize();

  public MetaBrainz.MusicBrainz.Interfaces.IAuthorizationToken GetBearerToken(string code, string clientSecret, System.Uri redirectUri, string? verifier = null);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.IAuthorizationToken> GetBearerTokenAsync(string code, string clientSecret, System.Uri redirectUri, string? verifier = null, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.IUserInfo GetUserInfo(string token);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.IUserInfo> GetUserInfoAsync(string token, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.IAuthorizationToken RefreshBearerToken(string refreshToken, string clientSecret);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.IAuthorizationToken> RefreshBearerTokenAsync(string refreshToken, string clientSecret, System.Threading.CancellationToken cancellationToken = default);

  public void RevokeToken(string token, string clientSecret);

  public System.Threading.Tasks.Task RevokeTokenAsync(string token, string clientSecret, System.Threading.CancellationToken cancellationToken = default);

}
```

### Type: PartialDate

```cs
public sealed class PartialDate : System.IComparable<PartialDate>, System.IEquatable<PartialDate> {

  public static readonly PartialDate Empty;

  public static readonly int MaxYear;

  public static readonly int MinYear;

  int? Day {
    public get;
  }

  bool IsEmpty {
    public get;
  }

  int? Month {
    public get;
  }

  System.DateTime NearestDate {
    public get;
  }

  int? Year {
    public get;
  }

  public PartialDate(int? year = default, int? month = default, int? day = default);

  public PartialDate(string? text);

  public sealed override int CompareTo(PartialDate? other);

  public sealed override bool Equals(PartialDate? other);

  public override bool Equals(object? obj);

  public override int GetHashCode();

  public override string ToString();

  public static bool operator !=(PartialDate? lhs, PartialDate? rhs);

  public static bool operator <(PartialDate? lhs, PartialDate? rhs);

  public static bool operator <=(PartialDate? lhs, PartialDate? rhs);

  public static bool operator ==(PartialDate? lhs, PartialDate? rhs);

  public static bool operator >(PartialDate? lhs, PartialDate? rhs);

  public static bool operator >=(PartialDate? lhs, PartialDate? rhs);

}
```

### Type: Query

```cs
public sealed class Query : System.IDisposable {

  public const int DefaultPageSize = 25;

  public const int MaximumPageSize = 100;

  public static readonly System.Diagnostics.TraceSource TraceSource;

  public const string WebServiceRoot = "/ws/2/";

  System.Uri BaseUri {
    public get;
  }

  string? BearerToken {
    public get;
    public set;
  }

  int DefaultPort {
    public static get;
    public static set;
  }

  string DefaultServer {
    public static get;
    public static set;
  }

  string DefaultUrlScheme {
    public static get;
    public static set;
  }

  System.Collections.Generic.IList<System.Net.Http.Headers.ProductInfoHeaderValue> DefaultUserAgent {
    public static get;
  }

  double DelayBetweenRequests {
    public static get;
    public static set;
  }

  int Port {
    public get;
    public set;
  }

  MetaBrainz.Common.RateLimitInfo RateLimitInfo {
    public get;
  }

  string Server {
    public get;
    public set;
  }

  string UrlScheme {
    public get;
    public set;
  }

  System.Collections.Generic.IList<System.Net.Http.Headers.ProductInfoHeaderValue> UserAgent {
    public get;
  }

  public Query();

  public Query(params System.Net.Http.Headers.ProductInfoHeaderValue[] userAgent);

  public Query(System.Net.Http.HttpClient client, bool takeOwnership = false);

  public Query(string application, System.Version? version);

  public Query(string application, System.Version? version, System.Uri contact);

  public Query(string application, System.Version? version, string contact);

  public Query(string application, string? version);

  public Query(string application, string? version, System.Uri contact);

  public Query(string application, string? version, string contact);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArea[] areas);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist[] artists);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent event);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent[] events);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument instrument);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument[] instruments);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel label);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel[] labels);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace place);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace[] places);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording recording);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording[] recordings);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup releaseGroup);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup[] releaseGroups);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease[] releases);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries series);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries[] series);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IWork work);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IWork[] works);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IArea> areas);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> artists);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> events);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument> instruments);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> labels);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace> places);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> recordings);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> releases);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> releaseGroups);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries> series);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork> works);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<System.Guid> items);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Guid item);

  public string AddToCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params System.Guid[] items);

  public string AddToCollection(string client, System.Guid collection, EntityType entityType, System.Collections.Generic.IEnumerable<System.Guid> items);

  public string AddToCollection(string client, System.Guid collection, EntityType entityType, System.Guid item);

  public string AddToCollection(string client, System.Guid collection, EntityType entityType, params System.Guid[] items);

  public string AddToCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area);

  public string AddToCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArea[] areas);

  public string AddToCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist);

  public string AddToCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist[] artists);

  public string AddToCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent event);

  public string AddToCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent[] events);

  public string AddToCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument instrument);

  public string AddToCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument[] instruments);

  public string AddToCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel label);

  public string AddToCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel[] labels);

  public string AddToCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace place);

  public string AddToCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace[] places);

  public string AddToCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording recording);

  public string AddToCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording[] recordings);

  public string AddToCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release);

  public string AddToCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup releaseGroup);

  public string AddToCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup[] releaseGroups);

  public string AddToCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease[] releases);

  public string AddToCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries series);

  public string AddToCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries[] series);

  public string AddToCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IWork work);

  public string AddToCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IWork[] works);

  public string AddToCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IArea> areas);

  public string AddToCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> artists);

  public string AddToCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> events);

  public string AddToCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument> instruments);

  public string AddToCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> labels);

  public string AddToCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace> places);

  public string AddToCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> recordings);

  public string AddToCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> releases);

  public string AddToCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> releaseGroups);

  public string AddToCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries> series);

  public string AddToCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork> works);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArea[] areas);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist[] artists);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent event, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent[] events);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument instrument, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument[] instruments);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel label, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel[] labels);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace place, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace[] places);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording recording, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording[] recordings);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup releaseGroup, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup[] releaseGroups);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease[] releases);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries series, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries[] series);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IWork work, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IWork[] works);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IArea> areas, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> artists, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> events, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument> instruments, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> labels, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace> places, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> recordings, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> releases, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> releaseGroups, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries> series, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork> works, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<System.Guid> items, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Guid item, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params System.Guid[] items);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArea[] areas);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist[] artists);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent[] events);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument[] instruments);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel[] labels);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace[] places);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording[] recordings);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup[] releaseGroups);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease[] releases);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries[] series);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IWork[] works);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params System.Guid[] items);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, EntityType entityType, System.Collections.Generic.IEnumerable<System.Guid> items, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, EntityType entityType, System.Guid item, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, EntityType entityType, params System.Guid[] items);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, EntityType entityType, System.Threading.CancellationToken cancellationToken, params System.Guid[] items);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArea[] areas);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist[] artists);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent event, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent[] events);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument instrument, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument[] instruments);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel label, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel[] labels);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace place, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace[] places);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording recording, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording[] recordings);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup releaseGroup, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup[] releaseGroups);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease[] releases);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries series, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries[] series);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IWork work, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IWork[] works);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IArea> areas, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> artists, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> events, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument> instruments, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> labels, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace> places, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> recordings, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> releases, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> releaseGroups, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries> series, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork> works, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArea[] areas);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist[] artists);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent[] events);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument[] instruments);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel[] labels);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace[] places);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording[] recordings);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup[] releaseGroups);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease[] releases);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries[] series);

  public System.Threading.Tasks.Task<string> AddToCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IWork[] works);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseAllAreaArtists(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllAreaCollections(System.Guid mbid, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> BrowseAllAreaEvents(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> BrowseAllAreaLabels(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace> BrowseAllAreaPlaces(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseAllAreaReleases(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArea> BrowseAllAreas(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllArtistCollections(System.Guid mbid, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> BrowseAllArtistEvents(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> BrowseAllArtistRecordings(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> BrowseAllArtistReleaseGroups(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseAllArtistReleases(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork> BrowseAllArtistWorks(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseAllArtists(MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseAllArtists(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseAllArtists(MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording recording, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseAllArtists(MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseAllArtists(MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup releaseGroup, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseAllArtists(MetaBrainz.MusicBrainz.Interfaces.Entities.IWork work, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArea> BrowseAllCollectionAreas(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseAllCollectionArtists(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> BrowseAllCollectionEvents(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument> BrowseAllCollectionInstruments(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> BrowseAllCollectionLabels(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace> BrowseAllCollectionPlaces(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> BrowseAllCollectionRecordings(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> BrowseAllCollectionReleaseGroups(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseAllCollectionReleases(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries> BrowseAllCollectionSeries(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork> BrowseAllCollectionWorks(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent event, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument instrument, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel label, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace place, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording recording, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup releaseGroup, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries series, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.IWork work, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllEditorCollections(string editor, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllEventCollections(System.Guid mbid, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> BrowseAllEvents(MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> BrowseAllEvents(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> BrowseAllEvents(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> BrowseAllEvents(MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace place, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllInstrumentCollections(System.Guid mbid, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument> BrowseAllInstruments(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllLabelCollections(System.Guid mbid, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseAllLabelReleases(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> BrowseAllLabels(MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> BrowseAllLabels(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> BrowseAllLabels(MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllPlaceCollections(System.Guid mbid, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> BrowseAllPlaceEvents(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace> BrowseAllPlaces(MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace> BrowseAllPlaces(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseAllRecordingArtists(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllRecordingCollections(System.Guid mbid, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseAllRecordingReleases(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> BrowseAllRecordings(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> BrowseAllRecordings(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> BrowseAllRecordings(MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseAllReleaseArtists(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllReleaseCollections(System.Guid mbid, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseAllReleaseGroupArtists(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllReleaseGroupCollections(System.Guid mbid, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseAllReleaseGroupReleases(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> BrowseAllReleaseGroups(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> BrowseAllReleaseGroups(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> BrowseAllReleaseGroups(MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> BrowseAllReleaseLabels(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> BrowseAllReleaseRecordings(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> BrowseAllReleaseReleaseGroups(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseAllReleases(MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseAllReleases(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseAllReleases(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseAllReleases(MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel label, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseAllReleases(MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording recording, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseAllReleases(MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup releaseGroup, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseAllReleases(MetaBrainz.MusicBrainz.Interfaces.Entities.ITrack track, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries> BrowseAllSeries(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllSeriesCollections(System.Guid mbid, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseAllTrackArtistReleases(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseAllTrackArtistReleases(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseAllTrackReleases(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseAllWorkArtists(System.Guid mbid, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAllWorkCollections(System.Guid mbid, int? pageSize = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork> BrowseAllWorks(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork> BrowseAllWorks(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? pageSize = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseAreaArtists(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist>> BrowseAreaArtistsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseAreaCollections(System.Guid mbid, int? limit = default, int? offset = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseAreaCollectionsAsync(System.Guid mbid, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> BrowseAreaEvents(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent>> BrowseAreaEventsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> BrowseAreaLabels(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel>> BrowseAreaLabelsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace> BrowseAreaPlaces(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace>> BrowseAreaPlacesAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseAreaReleases(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>> BrowseAreaReleasesAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArea> BrowseAreas(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArea>> BrowseAreasAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseArtistCollections(System.Guid mbid, int? limit = default, int? offset = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseArtistCollectionsAsync(System.Guid mbid, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> BrowseArtistEvents(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent>> BrowseArtistEventsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> BrowseArtistRecordings(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording>> BrowseArtistRecordingsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> BrowseArtistReleaseGroups(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup>> BrowseArtistReleaseGroupsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseArtistReleases(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>> BrowseArtistReleasesAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork> BrowseArtistWorks(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork>> BrowseArtistWorksAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseArtists(MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, int? limit = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseArtists(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseArtists(MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording recording, int? limit = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseArtists(MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, int? limit = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseArtists(MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup releaseGroup, int? limit = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseArtists(MetaBrainz.MusicBrainz.Interfaces.Entities.IWork work, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist>> BrowseArtistsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist>> BrowseArtistsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist>> BrowseArtistsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording recording, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist>> BrowseArtistsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist>> BrowseArtistsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup releaseGroup, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist>> BrowseArtistsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IWork work, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArea> BrowseCollectionAreas(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArea>> BrowseCollectionAreasAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseCollectionArtists(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist>> BrowseCollectionArtistsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> BrowseCollectionEvents(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent>> BrowseCollectionEventsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument> BrowseCollectionInstruments(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument>> BrowseCollectionInstrumentsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> BrowseCollectionLabels(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel>> BrowseCollectionLabelsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace> BrowseCollectionPlaces(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace>> BrowseCollectionPlacesAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> BrowseCollectionRecordings(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording>> BrowseCollectionRecordingsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> BrowseCollectionReleaseGroups(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup>> BrowseCollectionReleaseGroupsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseCollectionReleases(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>> BrowseCollectionReleasesAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries> BrowseCollectionSeries(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries>> BrowseCollectionSeriesAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork> BrowseCollectionWorks(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork>> BrowseCollectionWorksAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, int? limit = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? limit = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent event, int? limit = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument instrument, int? limit = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel label, int? limit = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace place, int? limit = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording recording, int? limit = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, int? limit = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup releaseGroup, int? limit = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries series, int? limit = default, int? offset = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseCollections(MetaBrainz.MusicBrainz.Interfaces.Entities.IWork work, int? limit = default, int? offset = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseCollectionsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseCollectionsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseCollectionsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent event, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseCollectionsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument instrument, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseCollectionsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel label, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseCollectionsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace place, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseCollectionsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording recording, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseCollectionsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseCollectionsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup releaseGroup, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseCollectionsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries series, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseCollectionsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IWork work, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseEditorCollections(string editor, int? limit = default, int? offset = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseEditorCollectionsAsync(string editor, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseEventCollections(System.Guid mbid, int? limit = default, int? offset = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseEventCollectionsAsync(System.Guid mbid, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> BrowseEvents(MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, int? limit = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> BrowseEvents(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? limit = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> BrowseEvents(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> BrowseEvents(MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace place, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent>> BrowseEventsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent>> BrowseEventsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent>> BrowseEventsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent>> BrowseEventsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace place, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseInstrumentCollections(System.Guid mbid, int? limit = default, int? offset = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseInstrumentCollectionsAsync(System.Guid mbid, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument> BrowseInstruments(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument>> BrowseInstrumentsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseLabelCollections(System.Guid mbid, int? limit = default, int? offset = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseLabelCollectionsAsync(System.Guid mbid, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseLabelReleases(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>> BrowseLabelReleasesAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> BrowseLabels(MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, int? limit = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> BrowseLabels(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> BrowseLabels(MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel>> BrowseLabelsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel>> BrowseLabelsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel>> BrowseLabelsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowsePlaceCollections(System.Guid mbid, int? limit = default, int? offset = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowsePlaceCollectionsAsync(System.Guid mbid, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> BrowsePlaceEvents(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent>> BrowsePlaceEventsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace> BrowsePlaces(MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, int? limit = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace> BrowsePlaces(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace>> BrowsePlacesAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace>> BrowsePlacesAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseRecordingArtists(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist>> BrowseRecordingArtistsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseRecordingCollections(System.Guid mbid, int? limit = default, int? offset = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseRecordingCollectionsAsync(System.Guid mbid, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseRecordingReleases(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>> BrowseRecordingReleasesAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> BrowseRecordings(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? limit = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> BrowseRecordings(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> BrowseRecordings(MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording>> BrowseRecordingsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording>> BrowseRecordingsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording>> BrowseRecordingsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseReleaseArtists(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist>> BrowseReleaseArtistsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseReleaseCollections(System.Guid mbid, int? limit = default, int? offset = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseReleaseCollectionsAsync(System.Guid mbid, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseReleaseGroupArtists(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist>> BrowseReleaseGroupArtistsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseReleaseGroupCollections(System.Guid mbid, int? limit = default, int? offset = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseReleaseGroupCollectionsAsync(System.Guid mbid, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseReleaseGroupReleases(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>> BrowseReleaseGroupReleasesAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> BrowseReleaseGroups(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> BrowseReleaseGroups(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> BrowseReleaseGroups(MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup>> BrowseReleaseGroupsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup>> BrowseReleaseGroupsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup>> BrowseReleaseGroupsAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> BrowseReleaseLabels(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel>> BrowseReleaseLabelsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> BrowseReleaseRecordings(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording>> BrowseReleaseRecordingsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> BrowseReleaseReleaseGroups(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup>> BrowseReleaseReleaseGroupsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseReleases(MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseReleases(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseReleases(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseReleases(MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel label, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseReleases(MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording recording, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseReleases(MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup releaseGroup, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseReleases(MetaBrainz.MusicBrainz.Interfaces.Entities.ITrack track, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>> BrowseReleasesAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>> BrowseReleasesAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>> BrowseReleasesAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>> BrowseReleasesAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel label, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>> BrowseReleasesAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording recording, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>> BrowseReleasesAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup releaseGroup, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>> BrowseReleasesAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.ITrack track, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries> BrowseSeries(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries>> BrowseSeriesAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseSeriesCollections(System.Guid mbid, int? limit = default, int? offset = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseSeriesCollectionsAsync(System.Guid mbid, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseTrackArtistReleases(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseTrackArtistReleases(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>> BrowseTrackArtistReleasesAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>> BrowseTrackArtistReleasesAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> BrowseTrackReleases(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>> BrowseTrackReleasesAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> BrowseWorkArtists(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist>> BrowseWorkArtistsAsync(System.Guid mbid, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> BrowseWorkCollections(System.Guid mbid, int? limit = default, int? offset = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection>> BrowseWorkCollectionsAsync(System.Guid mbid, int? limit = default, int? offset = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork> BrowseWorks(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? limit = default, int? offset = default, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork> BrowseWorks(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork>> BrowseWorksAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Browses.IBrowseResults<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork>> BrowseWorksAsync(MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, int? limit = default, int? offset = default, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public void Close();

  public void ConfigureClient(System.Action<System.Net.Http.HttpClient>? code);

  public void ConfigureClientCreation(System.Func<System.Net.Http.HttpClient>? code);

  public sealed override void Dispose();

  protected override void Finalize();

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IAnnotation>> FindAllAnnotations(string query, int? pageSize = default, int? offset = default, bool simple = false);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IArea>> FindAllAreas(string query, int? pageSize = default, int? offset = default, bool simple = false);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist>> FindAllArtists(string query, int? pageSize = default, int? offset = default, bool simple = false);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.ICdStub>> FindAllCdStubs(string query, int? pageSize = default, int? offset = default, bool simple = false);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent>> FindAllEvents(string query, int? pageSize = default, int? offset = default, bool simple = false);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument>> FindAllInstruments(string query, int? pageSize = default, int? offset = default, bool simple = false);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel>> FindAllLabels(string query, int? pageSize = default, int? offset = default, bool simple = false);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace>> FindAllPlaces(string query, int? pageSize = default, int? offset = default, bool simple = false);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording>> FindAllRecordings(string query, int? pageSize = default, int? offset = default, bool simple = false);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup>> FindAllReleaseGroups(string query, int? pageSize = default, int? offset = default, bool simple = false);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>> FindAllReleases(string query, int? pageSize = default, int? offset = default, bool simple = false);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries>> FindAllSeries(string query, int? pageSize = default, int? offset = default, bool simple = false);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.ITag>> FindAllTags(string query, int? pageSize = default, int? offset = default, bool simple = false);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IUrl>> FindAllUrls(string query, int? pageSize = default, int? offset = default, bool simple = false);

  public MetaBrainz.MusicBrainz.Interfaces.IStreamingQueryResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork>> FindAllWorks(string query, int? pageSize = default, int? offset = default, bool simple = false);

  public MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IAnnotation>> FindAnnotations(string query, int? limit = default, int? offset = default, bool simple = false);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IAnnotation>>> FindAnnotationsAsync(string query, int? limit = default, int? offset = default, bool simple = false, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IArea>> FindAreas(string query, int? limit = default, int? offset = default, bool simple = false);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IArea>>> FindAreasAsync(string query, int? limit = default, int? offset = default, bool simple = false, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist>> FindArtists(string query, int? limit = default, int? offset = default, bool simple = false);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist>>> FindArtistsAsync(string query, int? limit = default, int? offset = default, bool simple = false, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.ICdStub>> FindCdStubs(string query, int? limit = default, int? offset = default, bool simple = false);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.ICdStub>>> FindCdStubsAsync(string query, int? limit = default, int? offset = default, bool simple = false, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent>> FindEvents(string query, int? limit = default, int? offset = default, bool simple = false);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent>>> FindEventsAsync(string query, int? limit = default, int? offset = default, bool simple = false, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument>> FindInstruments(string query, int? limit = default, int? offset = default, bool simple = false);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument>>> FindInstrumentsAsync(string query, int? limit = default, int? offset = default, bool simple = false, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel>> FindLabels(string query, int? limit = default, int? offset = default, bool simple = false);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel>>> FindLabelsAsync(string query, int? limit = default, int? offset = default, bool simple = false, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace>> FindPlaces(string query, int? limit = default, int? offset = default, bool simple = false);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace>>> FindPlacesAsync(string query, int? limit = default, int? offset = default, bool simple = false, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording>> FindRecordings(string query, int? limit = default, int? offset = default, bool simple = false);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording>>> FindRecordingsAsync(string query, int? limit = default, int? offset = default, bool simple = false, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup>> FindReleaseGroups(string query, int? limit = default, int? offset = default, bool simple = false);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup>>> FindReleaseGroupsAsync(string query, int? limit = default, int? offset = default, bool simple = false, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>> FindReleases(string query, int? limit = default, int? offset = default, bool simple = false);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>>> FindReleasesAsync(string query, int? limit = default, int? offset = default, bool simple = false, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries>> FindSeries(string query, int? limit = default, int? offset = default, bool simple = false);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries>>> FindSeriesAsync(string query, int? limit = default, int? offset = default, bool simple = false, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.ITag>> FindTags(string query, int? limit = default, int? offset = default, bool simple = false);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.ITag>>> FindTagsAsync(string query, int? limit = default, int? offset = default, bool simple = false, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IUrl>> FindUrls(string query, int? limit = default, int? offset = default, bool simple = false);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IUrl>>> FindUrlsAsync(string query, int? limit = default, int? offset = default, bool simple = false, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork>> FindWorks(string query, int? limit = default, int? offset = default, bool simple = false);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResults<MetaBrainz.MusicBrainz.Interfaces.Searches.ISearchResult<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork>>> FindWorksAsync(string query, int? limit = default, int? offset = default, bool simple = false, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Entities.IArea LookupArea(System.Guid mbid, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Entities.IArea> LookupAreaAsync(System.Guid mbid, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist LookupArtist(System.Guid mbid, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> LookupArtistAsync(System.Guid mbid, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection LookupCollection(System.Guid mbid, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection> LookupCollectionAsync(System.Guid mbid, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.IDiscIdLookupResult LookupDiscId(string discid, int[]? toc = null, Include inc = Include.None, bool allMedia = false, bool noStubs = false);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.IDiscIdLookupResult> LookupDiscIdAsync(string discid, int[]? toc = null, Include inc = Include.None, bool allMediaFormats = false, bool noStubs = false, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent LookupEvent(System.Guid mbid, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> LookupEventAsync(System.Guid mbid, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Entities.IGenre LookupGenre(System.Guid mbid);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Entities.IGenre> LookupGenreAsync(System.Guid mbid, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument LookupInstrument(System.Guid mbid, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument> LookupInstrumentAsync(System.Guid mbid, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Entities.IIsrc LookupIsrc(string isrc, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Entities.IIsrc> LookupIsrcAsync(string isrc, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public System.Collections.Generic.IReadOnlyList<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork> LookupIswc(string iswc, Include inc = Include.None);

  public System.Threading.Tasks.Task<System.Collections.Generic.IReadOnlyList<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork>> LookupIswcAsync(string iswc, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel LookupLabel(System.Guid mbid, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> LookupLabelAsync(System.Guid mbid, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace LookupPlace(System.Guid mbid, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace> LookupPlaceAsync(System.Guid mbid, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording LookupRecording(System.Guid mbid, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> LookupRecordingAsync(System.Guid mbid, Include inc = Include.None, ReleaseType? type = default, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease LookupRelease(System.Guid mbid, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> LookupReleaseAsync(System.Guid mbid, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup LookupReleaseGroup(System.Guid mbid, Include inc = Include.None, ReleaseStatus? status = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> LookupReleaseGroupAsync(System.Guid mbid, Include inc = Include.None, ReleaseStatus? status = default, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries LookupSeries(System.Guid mbid, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries> LookupSeriesAsync(System.Guid mbid, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Entities.IUrl LookupUrl(System.Guid mbid, Include inc = Include.None);

  public MetaBrainz.MusicBrainz.Interfaces.Entities.IUrl LookupUrl(System.Uri resource, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Entities.IUrl> LookupUrlAsync(System.Guid mbid, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Entities.IUrl> LookupUrlAsync(System.Uri resource, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public MetaBrainz.MusicBrainz.Interfaces.Entities.IWork LookupWork(System.Guid mbid, Include inc = Include.None);

  public System.Threading.Tasks.Task<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork> LookupWorkAsync(System.Guid mbid, Include inc = Include.None, System.Threading.CancellationToken cancellationToken = default);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArea[] areas);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist[] artists);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent event);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent[] events);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument instrument);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument[] instruments);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel label);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel[] labels);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace place);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace[] places);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording recording);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording[] recordings);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup releaseGroup);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup[] releaseGroups);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease[] releases);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries series);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries[] series);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IWork work);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IWork[] works);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IArea> areas);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> artists);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> events);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument> instruments);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> labels);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace> places);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> recordings);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> releases);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> releaseGroups);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries> series);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork> works);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<System.Guid> items);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Guid item);

  public string RemoveFromCollection(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params System.Guid[] items);

  public string RemoveFromCollection(string client, System.Guid collection, EntityType entityType, System.Collections.Generic.IEnumerable<System.Guid> items);

  public string RemoveFromCollection(string client, System.Guid collection, EntityType entityType, System.Guid item);

  public string RemoveFromCollection(string client, System.Guid collection, EntityType entityType, params System.Guid[] items);

  public string RemoveFromCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area);

  public string RemoveFromCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArea[] areas);

  public string RemoveFromCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist);

  public string RemoveFromCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist[] artists);

  public string RemoveFromCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent event);

  public string RemoveFromCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent[] events);

  public string RemoveFromCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument instrument);

  public string RemoveFromCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument[] instruments);

  public string RemoveFromCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel label);

  public string RemoveFromCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel[] labels);

  public string RemoveFromCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace place);

  public string RemoveFromCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace[] places);

  public string RemoveFromCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording recording);

  public string RemoveFromCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording[] recordings);

  public string RemoveFromCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release);

  public string RemoveFromCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup releaseGroup);

  public string RemoveFromCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup[] releaseGroups);

  public string RemoveFromCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease[] releases);

  public string RemoveFromCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries series);

  public string RemoveFromCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries[] series);

  public string RemoveFromCollection(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IWork work);

  public string RemoveFromCollection(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IWork[] works);

  public string RemoveFromCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IArea> areas);

  public string RemoveFromCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> artists);

  public string RemoveFromCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> events);

  public string RemoveFromCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument> instruments);

  public string RemoveFromCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> labels);

  public string RemoveFromCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace> places);

  public string RemoveFromCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> recordings);

  public string RemoveFromCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> releases);

  public string RemoveFromCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> releaseGroups);

  public string RemoveFromCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries> series);

  public string RemoveFromCollection(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork> works);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArea[] areas);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist[] artists);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent event, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent[] events);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument instrument, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument[] instruments);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel label, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel[] labels);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace place, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace[] places);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording recording, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording[] recordings);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup releaseGroup, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup[] releaseGroups);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease[] releases);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries series, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries[] series);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IWork work, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IWork[] works);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IArea> areas, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> artists, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> events, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument> instruments, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> labels, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace> places, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> recordings, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> releases, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> releaseGroups, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries> series, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork> works, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Collections.Generic.IEnumerable<System.Guid> items, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Guid item, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, params System.Guid[] items);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArea[] areas);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist[] artists);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent[] events);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument[] instruments);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel[] labels);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace[] places);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording[] recordings);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup[] releaseGroups);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease[] releases);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries[] series);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IWork[] works);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, MetaBrainz.MusicBrainz.Interfaces.Entities.ICollection collection, System.Threading.CancellationToken cancellationToken, params System.Guid[] items);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, EntityType entityType, System.Collections.Generic.IEnumerable<System.Guid> items, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, EntityType entityType, System.Guid item, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, EntityType entityType, params System.Guid[] items);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, EntityType entityType, System.Threading.CancellationToken cancellationToken, params System.Guid[] items);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IArea area, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArea[] areas);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist artist, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist[] artists);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent event, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent[] events);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument instrument, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument[] instruments);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel label, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel[] labels);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace place, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace[] places);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording recording, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording[] recordings);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup releaseGroup, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup[] releaseGroups);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease[] releases);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries series, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries[] series);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, MetaBrainz.MusicBrainz.Interfaces.Entities.IWork work, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, params MetaBrainz.MusicBrainz.Interfaces.Entities.IWork[] works);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IArea> areas, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist> artists, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent> events, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument> instruments, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel> labels, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace> places, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording> recordings, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease> releases, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup> releaseGroups, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries> series, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Collections.Generic.IEnumerable<MetaBrainz.MusicBrainz.Interfaces.Entities.IWork> works, System.Threading.CancellationToken cancellationToken = default);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArea[] areas);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IArtist[] artists);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IEvent[] events);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IInstrument[] instruments);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.ILabel[] labels);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IPlace[] places);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording[] recordings);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IReleaseGroup[] releaseGroups);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease[] releases);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.ISeries[] series);

  public System.Threading.Tasks.Task<string> RemoveFromCollectionAsync(string client, System.Guid collection, System.Threading.CancellationToken cancellationToken, params MetaBrainz.MusicBrainz.Interfaces.Entities.IWork[] works);

  public MetaBrainz.MusicBrainz.Objects.Submissions.BarcodeSubmission SubmitBarcodes(string client);

  public MetaBrainz.MusicBrainz.Objects.Submissions.IsrcSubmission SubmitIsrcs(string client);

  public MetaBrainz.MusicBrainz.Objects.Submissions.RatingSubmission SubmitRatings(string client);

  public MetaBrainz.MusicBrainz.Objects.Submissions.TagSubmission SubmitTags(string client);

}
```

### Type: ReleaseStatus

```cs
[System.FlagsAttribute]
public enum ReleaseStatus : byte {

  Bootleg = (byte) 1,
  Official = (byte) 2,
  Promotion = (byte) 4,
  Promotional = (byte) 4,
  PseudoRelease = (byte) 8,

}
```

### Type: ReleaseType

```cs
[System.FlagsAttribute]
public enum ReleaseType {

  Album = 1,
  Audiobook = 1024,
  Broadcast = 2,
  Compilation = 2048,
  DJMix = 4096,
  EP = 4,
  Interview = 8192,
  Live = 16384,
  MixTape = 32768,
  Other = 8,
  Remix = 65536,
  Single = 16,
  Soundtrack = 131072,
  SpokenWord = 262144,
  StreetAlbum = 32768,

}
```

### Type: TagVote

```cs
public enum TagVote {

  Down = 1,
  Up = 0,
  Withdraw = 2,

}
```

## Namespace: MetaBrainz.MusicBrainz.Interfaces

### Type: IAuthorizationToken

```cs
public interface IAuthorizationToken : MetaBrainz.Common.Json.IJsonBasedObject {

  string AccessToken {
    public abstract get;
  }

  int Lifetime {
    public abstract get;
  }

  string RefreshToken {
    public abstract get;
  }

  string TokenType {
    public abstract get;
  }

}
```

### Type: IDiscIdLookupResult

```cs
public interface IDiscIdLookupResult : MetaBrainz.Common.Json.IJsonBasedObject {

  MetaBrainz.MusicBrainz.Interfaces.Entities.IDisc? Disc {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease>? Releases {
    public abstract get;
  }

  MetaBrainz.MusicBrainz.Interfaces.Entities.ICdStub? Stub {
    public abstract get;
  }

}
```

### Type: IPagedQueryResults\<TResults, out TItem>

```cs
public interface IPagedQueryResults<TResults, out TItem> : MetaBrainz.Common.Json.IJsonBasedObject
  where TResults : IPagedQueryResults<TResults, out TItem> {

  int? Limit {
    public abstract get;
    public abstract set;
  }

  int? NextOffset {
    public abstract get;
    public abstract set;
  }

  int Offset {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<out TItem> Results {
    public abstract get;
  }

  int TotalResults {
    public abstract get;
  }

  public abstract IStreamingQueryResults<out TItem> AsStream();

  public abstract TResults Next();

  public abstract System.Threading.Tasks.Task<TResults> NextAsync(System.Threading.CancellationToken cancellationToken = default);

  public abstract TResults Previous();

  public abstract System.Threading.Tasks.Task<TResults> PreviousAsync(System.Threading.CancellationToken cancellationToken = default);

}
```

### Type: IStreamingQueryResults\<out TItem>

```cs
public interface IStreamingQueryResults<out TItem> : System.Collections.Generic.IAsyncEnumerable<out TItem>, System.Collections.Generic.IEnumerable<out TItem>, System.Collections.IEnumerable {

}
```

### Type: IUserInfo

```cs
public interface IUserInfo : MetaBrainz.Common.Json.IJsonBasedObject {

  string? Email {
    public abstract get;
    public abstract init;
  }

  string? Gender {
    public abstract get;
  }

  string Name {
    public abstract get;
  }

  System.Uri Profile {
    public abstract get;
  }

  string? TimeZone {
    public abstract get;
  }

  int UserId {
    public abstract get;
  }

  bool VerifiedEmail {
    public abstract get;
  }

  System.Uri? Website {
    public abstract get;
  }

}
```

## Namespace: MetaBrainz.MusicBrainz.Interfaces.Browses

### Type: IBrowseResults\<T>

```cs
public interface IBrowseResults<T> : MetaBrainz.Common.Json.IJsonBasedObject, MetaBrainz.MusicBrainz.Interfaces.IPagedQueryResults<IBrowseResults<T>, T>
  where T : MetaBrainz.MusicBrainz.Interfaces.Entities.IEntity {

}
```

## Namespace: MetaBrainz.MusicBrainz.Interfaces.Entities

### Type: IAlias

```cs
public interface IAlias : MetaBrainz.Common.Json.IJsonBasedObject {

  MetaBrainz.MusicBrainz.PartialDate? Begin {
    public abstract get;
  }

  MetaBrainz.MusicBrainz.PartialDate? End {
    public abstract get;
  }

  bool Ended {
    public abstract get;
  }

  string? Locale {
    public abstract get;
  }

  string Name {
    public abstract get;
  }

  bool Primary {
    public abstract get;
  }

  string? SortName {
    public abstract get;
  }

  string? Type {
    public abstract get;
  }

  System.Guid? TypeId {
    public abstract get;
  }

}
```

### Type: IAliasedEntity

```cs
public interface IAliasedEntity : IEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  System.Collections.Generic.IReadOnlyList<IAlias>? Aliases {
    public abstract get;
  }

}
```

### Type: IAnnotatedEntity

```cs
public interface IAnnotatedEntity : IEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  string? Annotation {
    public abstract get;
  }

}
```

### Type: IAnnotation

```cs
public interface IAnnotation : MetaBrainz.Common.Json.IJsonBasedObject {

  System.Guid? Entity {
    public abstract get;
  }

  string? Name {
    public abstract get;
  }

  string? Text {
    public abstract get;
  }

  MetaBrainz.MusicBrainz.EntityType? Type {
    public abstract get;
  }

}
```

### Type: IArea

```cs
public interface IArea : IAliasedEntity, IAnnotatedEntity, IEntity, INamedEntity, IRelatableEntity, ITaggableEntity, ITypedEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  System.Collections.Generic.IReadOnlyList<string>? Iso31661Codes {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<string>? Iso31662Codes {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<string>? Iso31663Codes {
    public abstract get;
  }

  ILifeSpan? LifeSpan {
    public abstract get;
  }

  string? SortName {
    public abstract get;
  }

}
```

### Type: IArtist

```cs
public interface IArtist : IAliasedEntity, IAnnotatedEntity, IEntity, INamedEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITypedEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  IArea? Area {
    public abstract get;
  }

  IArea? BeginArea {
    public abstract get;
  }

  string? Country {
    public abstract get;
  }

  IArea? EndArea {
    public abstract get;
  }

  string? Gender {
    public abstract get;
  }

  System.Guid? GenderId {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<string>? Ipis {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<string>? Isnis {
    public abstract get;
  }

  ILifeSpan? LifeSpan {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<IRecording>? Recordings {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<IReleaseGroup>? ReleaseGroups {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<IRelease>? Releases {
    public abstract get;
  }

  string? SortName {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<IWork>? Works {
    public abstract get;
  }

}
```

### Type: ICdStub

```cs
public interface ICdStub : MetaBrainz.Common.Json.IJsonBasedObject {

  string? Artist {
    public abstract get;
  }

  string? Barcode {
    public abstract get;
  }

  string? Disambiguation {
    public abstract get;
  }

  string Id {
    public abstract get;
  }

  string Title {
    public abstract get;
  }

  int TrackCount {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<ISimpleTrack>? Tracks {
    public abstract get;
  }

}
```

### Type: ICollection

```cs
public interface ICollection : IEntity, ITypedEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  MetaBrainz.MusicBrainz.EntityType ContentType {
    public abstract get;
  }

  string? Editor {
    public abstract get;
  }

  int ItemCount {
    public abstract get;
  }

  string? Name {
    public abstract get;
  }

}
```

### Type: ICoordinates

```cs
public interface ICoordinates : MetaBrainz.Common.Json.IJsonBasedObject {

  double Latitude {
    public abstract get;
  }

  double Longitude {
    public abstract get;
  }

}
```

### Type: ICoverArtArchive

```cs
public interface ICoverArtArchive : MetaBrainz.Common.Json.IJsonBasedObject {

  bool Artwork {
    public abstract get;
  }

  bool Back {
    public abstract get;
  }

  int Count {
    public abstract get;
  }

  bool Darkened {
    public abstract get;
  }

  bool Front {
    public abstract get;
  }

}
```

### Type: IDisc

```cs
public interface IDisc : MetaBrainz.Common.Json.IJsonBasedObject {

  string Id {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<int> Offsets {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<IRelease>? Releases {
    public abstract get;
  }

  int Sectors {
    public abstract get;
  }

}
```

### Type: IEntity

```cs
public interface IEntity : MetaBrainz.Common.Json.IJsonBasedObject {

  MetaBrainz.MusicBrainz.EntityType EntityType {
    public abstract get;
  }

  System.Guid Id {
    public abstract get;
  }

}
```

### Type: IEvent

```cs
public interface IEvent : IAliasedEntity, IAnnotatedEntity, IEntity, INamedEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITypedEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  bool Cancelled {
    public abstract get;
  }

  ILifeSpan? LifeSpan {
    public abstract get;
  }

  string? Setlist {
    public abstract get;
  }

  string? Time {
    public abstract get;
  }

}
```

### Type: IGenre

```cs
public interface IGenre : IEntity, INamedEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  int? VoteCount {
    public abstract get;
  }

}
```

### Type: IInstrument

```cs
public interface IInstrument : IAliasedEntity, IAnnotatedEntity, IEntity, INamedEntity, IRelatableEntity, ITaggableEntity, ITypedEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  string? Description {
    public abstract get;
  }

}
```

### Type: IIsrc

```cs
public interface IIsrc : MetaBrainz.Common.Json.IJsonBasedObject {

  System.Collections.Generic.IReadOnlyList<IRecording> Recordings {
    public abstract get;
  }

  string Value {
    public abstract get;
  }

}
```

### Type: ILabel

```cs
public interface ILabel : IAliasedEntity, IAnnotatedEntity, IEntity, INamedEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITypedEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  IArea? Area {
    public abstract get;
  }

  string? Country {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<string>? Ipis {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<string>? Isnis {
    public abstract get;
  }

  int? LabelCode {
    public abstract get;
  }

  ILifeSpan? LifeSpan {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<IRelease>? Releases {
    public abstract get;
  }

  string? SortName {
    public abstract get;
  }

}
```

### Type: ILabelInfo

```cs
public interface ILabelInfo : MetaBrainz.Common.Json.IJsonBasedObject {

  string? CatalogNumber {
    public abstract get;
  }

  ILabel? Label {
    public abstract get;
  }

}
```

### Type: ILifeSpan

```cs
public interface ILifeSpan : MetaBrainz.Common.Json.IJsonBasedObject {

  MetaBrainz.MusicBrainz.PartialDate? Begin {
    public abstract get;
  }

  MetaBrainz.MusicBrainz.PartialDate? End {
    public abstract get;
  }

  bool Ended {
    public abstract get;
  }

}
```

### Type: IMedium

```cs
public interface IMedium : MetaBrainz.Common.Json.IJsonBasedObject {

  System.Collections.Generic.IReadOnlyList<ITrack>? DataTracks {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<IDisc>? Discs {
    public abstract get;
  }

  string? Format {
    public abstract get;
  }

  System.Guid? FormatId {
    public abstract get;
  }

  int Position {
    public abstract get;
  }

  ITrack? Pregap {
    public abstract get;
  }

  string? Title {
    public abstract get;
  }

  int TrackCount {
    public abstract get;
  }

  int? TrackOffset {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<ITrack>? Tracks {
    public abstract get;
  }

}
```

### Type: INameCredit

```cs
public interface INameCredit : MetaBrainz.Common.Json.IJsonBasedObject {

  IArtist? Artist {
    public abstract get;
  }

  string? JoinPhrase {
    public abstract get;
  }

  string? Name {
    public abstract get;
  }

}
```

### Type: INamedEntity

```cs
public interface INamedEntity : IEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  string? Disambiguation {
    public abstract get;
  }

  string? Name {
    public abstract get;
  }

}
```

### Type: IPlace

```cs
public interface IPlace : IAliasedEntity, IAnnotatedEntity, IEntity, INamedEntity, IRelatableEntity, ITaggableEntity, ITypedEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  string? Address {
    public abstract get;
  }

  IArea? Area {
    public abstract get;
  }

  ICoordinates? Coordinates {
    public abstract get;
  }

  ILifeSpan? LifeSpan {
    public abstract get;
  }

}
```

### Type: IRatableEntity

```cs
public interface IRatableEntity : IEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  IRating? Rating {
    public abstract get;
  }

  IRating? UserRating {
    public abstract get;
  }

}
```

### Type: IRating

```cs
public interface IRating : MetaBrainz.Common.Json.IJsonBasedObject {

  decimal? Value {
    public abstract get;
  }

  int? VoteCount {
    public abstract get;
  }

}
```

### Type: IRecording

```cs
public interface IRecording : IAliasedEntity, IAnnotatedEntity, IEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITitledEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  System.Collections.Generic.IReadOnlyList<INameCredit>? ArtistCredit {
    public abstract get;
  }

  MetaBrainz.MusicBrainz.PartialDate? FirstReleaseDate {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<string>? Isrcs {
    public abstract get;
  }

  System.TimeSpan? Length {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<IRelease>? Releases {
    public abstract get;
  }

  bool Video {
    public abstract get;
  }

}
```

### Type: IRelatableEntity

```cs
public interface IRelatableEntity : IEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  System.Collections.Generic.IReadOnlyList<IRelationship>? Relationships {
    public abstract get;
  }

}
```

### Type: IRelationship

```cs
public interface IRelationship : MetaBrainz.Common.Json.IJsonBasedObject {

  IArea? Area {
    public abstract get;
  }

  IArtist? Artist {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyDictionary<string, string>? AttributeCredits {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyDictionary<string, System.Guid>? AttributeIds {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<string>? Attributes {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyDictionary<string, string>? AttributeValues {
    public abstract get;
  }

  MetaBrainz.MusicBrainz.PartialDate? Begin {
    public abstract get;
  }

  string? Direction {
    public abstract get;
  }

  MetaBrainz.MusicBrainz.PartialDate? End {
    public abstract get;
  }

  bool Ended {
    public abstract get;
  }

  IEvent? Event {
    public abstract get;
  }

  IInstrument? Instrument {
    public abstract get;
  }

  ILabel? Label {
    public abstract get;
  }

  int? OrderingKey {
    public abstract get;
  }

  IPlace? Place {
    public abstract get;
  }

  IRecording? Recording {
    public abstract get;
  }

  IRelease? Release {
    public abstract get;
  }

  IReleaseGroup? ReleaseGroup {
    public abstract get;
  }

  ISeries? Series {
    public abstract get;
  }

  string? SourceCredit {
    public abstract get;
  }

  IRelatableEntity? Target {
    public abstract get;
  }

  string? TargetCredit {
    public abstract get;
  }

  System.Guid? TargetId {
    public abstract get;
  }

  MetaBrainz.MusicBrainz.EntityType? TargetType {
    public abstract get;
  }

  string? Type {
    public abstract get;
  }

  System.Guid? TypeId {
    public abstract get;
  }

  IUrl? Url {
    public abstract get;
  }

  IWork? Work {
    public abstract get;
  }

}
```

### Type: IRelease

```cs
public interface IRelease : IAliasedEntity, IAnnotatedEntity, IEntity, IRelatableEntity, ITaggableEntity, ITitledEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  System.Collections.Generic.IReadOnlyList<INameCredit>? ArtistCredit {
    public abstract get;
  }

  string? Asin {
    public abstract get;
  }

  string? Barcode {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<ICollection>? Collections {
    public abstract get;
  }

  string? Country {
    public abstract get;
  }

  ICoverArtArchive? CoverArtArchive {
    public abstract get;
  }

  MetaBrainz.MusicBrainz.PartialDate? Date {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<ILabelInfo>? LabelInfo {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<IMedium>? Media {
    public abstract get;
  }

  string? Packaging {
    public abstract get;
  }

  System.Guid? PackagingId {
    public abstract get;
  }

  string? Quality {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<IReleaseEvent>? ReleaseEvents {
    public abstract get;
  }

  IReleaseGroup? ReleaseGroup {
    public abstract get;
  }

  string? Status {
    public abstract get;
  }

  System.Guid? StatusId {
    public abstract get;
  }

  ITextRepresentation? TextRepresentation {
    public abstract get;
  }

}
```

### Type: IReleaseEvent

```cs
public interface IReleaseEvent : MetaBrainz.Common.Json.IJsonBasedObject {

  IArea? Area {
    public abstract get;
  }

  MetaBrainz.MusicBrainz.PartialDate? Date {
    public abstract get;
  }

}
```

### Type: IReleaseGroup

```cs
public interface IReleaseGroup : IAliasedEntity, IAnnotatedEntity, IEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITitledEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  System.Collections.Generic.IReadOnlyList<INameCredit>? ArtistCredit {
    public abstract get;
  }

  MetaBrainz.MusicBrainz.PartialDate? FirstReleaseDate {
    public abstract get;
  }

  string? PrimaryType {
    public abstract get;
  }

  System.Guid? PrimaryTypeId {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<IRelease>? Releases {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<System.Guid>? SecondaryTypeIds {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<string>? SecondaryTypes {
    public abstract get;
  }

}
```

### Type: ISeries

```cs
public interface ISeries : IAliasedEntity, IAnnotatedEntity, IEntity, INamedEntity, IRelatableEntity, ITaggableEntity, ITypedEntity, MetaBrainz.Common.Json.IJsonBasedObject {

}
```

### Type: ISimpleTrack

```cs
public interface ISimpleTrack : MetaBrainz.Common.Json.IJsonBasedObject {

  string? Artist {
    public abstract get;
  }

  System.TimeSpan Length {
    public abstract get;
  }

  string? Title {
    public abstract get;
  }

}
```

### Type: ITag

```cs
public interface ITag : MetaBrainz.Common.Json.IJsonBasedObject {

  string Name {
    public abstract get;
  }

  int? VoteCount {
    public abstract get;
  }

}
```

### Type: ITaggableEntity

```cs
public interface ITaggableEntity : IEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  System.Collections.Generic.IReadOnlyList<IGenre>? Genres {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<ITag>? Tags {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<IGenre>? UserGenres {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<ITag>? UserTags {
    public abstract get;
  }

}
```

### Type: ITextRepresentation

```cs
public interface ITextRepresentation : MetaBrainz.Common.Json.IJsonBasedObject {

  string? Language {
    public abstract get;
  }

  string? Script {
    public abstract get;
  }

}
```

### Type: ITitledEntity

```cs
public interface ITitledEntity : IEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  string? Disambiguation {
    public abstract get;
  }

  string? Title {
    public abstract get;
  }

}
```

### Type: ITrack

```cs
public interface ITrack : MetaBrainz.Common.Json.IJsonBasedObject {

  System.Collections.Generic.IReadOnlyList<INameCredit>? ArtistCredit {
    public abstract get;
  }

  System.Guid Id {
    public abstract get;
  }

  System.TimeSpan? Length {
    public abstract get;
  }

  string? Number {
    public abstract get;
  }

  int? Position {
    public abstract get;
  }

  IRecording? Recording {
    public abstract get;
  }

  string? Title {
    public abstract get;
  }

}
```

### Type: ITypedEntity

```cs
public interface ITypedEntity : IEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  string? Type {
    public abstract get;
  }

  System.Guid? TypeId {
    public abstract get;
  }

}
```

### Type: IUrl

```cs
public interface IUrl : IEntity, IRelatableEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  System.Uri? Resource {
    public abstract get;
  }

}
```

### Type: IWork

```cs
public interface IWork : IAliasedEntity, IAnnotatedEntity, IEntity, IRatableEntity, IRelatableEntity, ITaggableEntity, ITitledEntity, ITypedEntity, MetaBrainz.Common.Json.IJsonBasedObject {

  System.Collections.Generic.IReadOnlyList<IWorkAttribute>? Attributes {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<string>? Iswcs {
    public abstract get;
  }

  string? Language {
    public abstract get;
  }

  System.Collections.Generic.IReadOnlyList<string>? Languages {
    public abstract get;
  }

}
```

### Type: IWorkAttribute

```cs
public interface IWorkAttribute : MetaBrainz.Common.Json.IJsonBasedObject {

  string? Type {
    public abstract get;
  }

  System.Guid? TypeId {
    public abstract get;
  }

  string? Value {
    public abstract get;
  }

  System.Guid? ValueId {
    public abstract get;
  }

}
```

## Namespace: MetaBrainz.MusicBrainz.Interfaces.Searches

### Type: ISearchResult

```cs
public interface ISearchResult {

  byte Score {
    public abstract get;
  }

}
```

### Type: ISearchResult\<out T>

```cs
public interface ISearchResult<out T> : ISearchResult {

  T Item {
    public abstract get;
  }

}
```

### Type: ISearchResults\<T>

```cs
public interface ISearchResults<T> : MetaBrainz.Common.Json.IJsonBasedObject, MetaBrainz.MusicBrainz.Interfaces.IPagedQueryResults<ISearchResults<T>, T>
  where T : ISearchResult {

  System.DateTimeOffset? Created {
    public abstract get;
  }

}
```

## Namespace: MetaBrainz.MusicBrainz.Objects.Submissions

### Type: BarcodeSubmission

```cs
public sealed class BarcodeSubmission : Submission {

  public BarcodeSubmission Add(MetaBrainz.MusicBrainz.Interfaces.Entities.IRelease release, string barcode);

  public BarcodeSubmission Add(System.Guid mbid, string barcode);

}
```

### Type: IsrcSubmission

```cs
public sealed class IsrcSubmission : Submission {

  public IsrcSubmission Add(MetaBrainz.MusicBrainz.Interfaces.Entities.IRecording recording, params string[] isrcs);

  public IsrcSubmission Add(System.Guid mbid, params string[] isrcs);

}
```

### Type: RatingSubmission

```cs
public sealed class RatingSubmission : Submission {

  public RatingSubmission Add(byte rating, MetaBrainz.MusicBrainz.EntityType entityType, System.Guid mbid);

  public RatingSubmission Add(byte rating, MetaBrainz.MusicBrainz.EntityType entityType, params System.Guid[] mbids);

  public RatingSubmission Add(byte rating, MetaBrainz.MusicBrainz.Interfaces.Entities.IRatableEntity entity);

  public RatingSubmission Add(byte rating, params MetaBrainz.MusicBrainz.Interfaces.Entities.IRatableEntity[] entities);

}
```

### Type: Submission

```cs
public abstract class Submission : MetaBrainz.MusicBrainz.Interfaces.Submissions.ISubmission {

  public string Submit();

  public System.Threading.Tasks.Task<string> SubmitAsync(System.Threading.CancellationToken cancellationToken = default);

}
```

### Type: TagSubmission

```cs
public sealed class TagSubmission : Submission {

  public TagSubmission Add(MetaBrainz.MusicBrainz.EntityType entityType, System.Guid mbid, MetaBrainz.MusicBrainz.TagVote vote, params string[] tags);

  public TagSubmission Add(MetaBrainz.MusicBrainz.Interfaces.Entities.ITaggableEntity entity, MetaBrainz.MusicBrainz.TagVote vote, params string[] tags);

  public TagSubmission Add(string tag, MetaBrainz.MusicBrainz.TagVote vote, MetaBrainz.MusicBrainz.EntityType entityType, System.Guid mbid);

  public TagSubmission Add(string tag, MetaBrainz.MusicBrainz.TagVote vote, MetaBrainz.MusicBrainz.EntityType entityType, params System.Guid[] mbids);

  public TagSubmission Add(string tag, MetaBrainz.MusicBrainz.TagVote vote, MetaBrainz.MusicBrainz.Interfaces.Entities.ITaggableEntity entity);

  public TagSubmission Add(string tag, MetaBrainz.MusicBrainz.TagVote vote, params MetaBrainz.MusicBrainz.Interfaces.Entities.ITaggableEntity[] entities);

}
```
