using System;
using System.Net;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces;
using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Browses;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Returns the release groups associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose release groups should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>
  /// The requested release groups.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IReleaseGroup> BrowseAllArtistReleaseGroups(Guid mbid, int? pageSize = null, int? offset = null,
                                                                            Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.BuildExtraText(inc, "artist", mbid, type), pageSize, offset).AsStream();

  /// <summary>Returns the release groups in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained release groups should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>
  /// The requested release groups.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IReleaseGroup> BrowseAllCollectionReleaseGroups(Guid mbid, int? pageSize = null, int? offset = null,
                                                                                Include inc = Include.None,
                                                                                ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.BuildExtraText(inc, "collection", mbid, type), pageSize, offset).AsStream();

  /// <summary>Returns the release groups associated with the given release.</summary>
  /// <param name="mbid">The MBID for the release whose release groups should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>
  /// The requested release groups.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  /// <remarks>
  /// Currently a release can only be part of a single release group, so assuming <paramref name="mbid"/> is valid, this should
  /// always return exactly one result.
  /// </remarks>
  public IStreamingQueryResults<IReleaseGroup> BrowseAllReleaseReleaseGroups(Guid mbid, int? pageSize = null, int? offset = null,
                                                                             Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.BuildExtraText(inc, "release", mbid, type), pageSize, offset).AsStream();

  /// <summary>Returns the release groups associated with the given artist.</summary>
  /// <param name="artist">The artist whose release groups should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>
  /// The requested release groups.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IReleaseGroup> BrowseAllReleaseGroups(IArtist artist, int? pageSize = null, int? offset = null,
                                                                      Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.BuildExtraText(inc, "artist", artist.Id, type), pageSize, offset).AsStream();

  /// <summary>Returns the release groups in the given collection.</summary>
  /// <param name="collection">The collection whose contained release groups should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>
  /// The requested release groups.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IReleaseGroup> BrowseAllReleaseGroups(ICollection collection, int? pageSize = null,
                                                                      int? offset = null, Include inc = Include.None,
                                                                      ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.BuildExtraText(inc, "collection", collection.Id, type), pageSize, offset).AsStream();

  /// <summary>Returns the release groups associated with the given release.</summary>
  /// <param name="release">The release whose release groups should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>
  /// The requested release groups.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  /// <remarks>
  /// Currently a release can only be part of a single release group, so this should always return exactly one result.
  /// </remarks>
  public IStreamingQueryResults<IReleaseGroup> BrowseAllReleaseGroups(IRelease release, int? pageSize = null, int? offset = null,
                                                                      Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.BuildExtraText(inc, "release", release.Id, type), pageSize, offset).AsStream();

  /// <inheritdoc cref="BrowseArtistReleaseGroupsAsync"/>
  public IBrowseResults<IReleaseGroup> BrowseArtistReleaseGroups(Guid mbid, int? limit = null, int? offset = null,
                                                                 Include inc = Include.None, ReleaseType? type = null)
    => Utils.ResultOf(this.BrowseArtistReleaseGroupsAsync(mbid, limit, offset, inc, type));

  /// <summary>Returns (the specified subset of) the release groups associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IReleaseGroup>> BrowseArtistReleaseGroupsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                            Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.BuildExtraText(inc, "artist", mbid, type), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseCollectionReleaseGroupsAsync"/>
  public IBrowseResults<IReleaseGroup> BrowseCollectionReleaseGroups(Guid mbid, int? limit = null, int? offset = null,
                                                                     Include inc = Include.None, ReleaseType? type = null)
    => Utils.ResultOf(this.BrowseCollectionReleaseGroupsAsync(mbid, limit, offset, inc, type));

  /// <summary>Returns (the specified subset of) the release groups in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IReleaseGroup>> BrowseCollectionReleaseGroupsAsync(
    Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.BuildExtraText(inc, "collection", mbid, type), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseReleaseReleaseGroupsAsync"/>
  public IBrowseResults<IReleaseGroup> BrowseReleaseReleaseGroups(Guid mbid, int? limit = null, int? offset = null,
                                                                  Include inc = Include.None, ReleaseType? type = null)
    => Utils.ResultOf(this.BrowseReleaseReleaseGroupsAsync(mbid, limit, offset, inc, type));

  /// <summary>Returns (the specified subset of) the release groups associated with the given release.</summary>
  /// <param name="mbid">The MBID for the release whose release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  /// <remarks>
  /// Currently a release can only be part of a single release group, so assuming <paramref name="mbid"/> is valid, this should
  /// always return exactly one result.
  /// </remarks>
  public Task<IBrowseResults<IReleaseGroup>> BrowseReleaseReleaseGroupsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                             Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.BuildExtraText(inc, "release", mbid, type), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseReleaseGroupsAsync(IArtist,int?,int?,Include,ReleaseType?)"/>
  public IBrowseResults<IReleaseGroup> BrowseReleaseGroups(IArtist artist, int? limit = null, int? offset = null,
                                                           Include inc = Include.None, ReleaseType? type = null)
    => Utils.ResultOf(this.BrowseReleaseGroupsAsync(artist, limit, offset, inc, type));

  /// <inheritdoc cref="BrowseReleaseGroupsAsync(ICollection,int?,int?,Include,ReleaseType?)"/>
  public IBrowseResults<IReleaseGroup> BrowseReleaseGroups(ICollection collection, int? limit = null, int? offset = null,
                                                           Include inc = Include.None, ReleaseType? type = null)
    => Utils.ResultOf(this.BrowseReleaseGroupsAsync(collection, limit, offset, inc, type));

  /// <inheritdoc cref="BrowseReleaseGroupsAsync(IRelease,int?,int?,Include,ReleaseType?)"/>
  public IBrowseResults<IReleaseGroup> BrowseReleaseGroups(IRelease release, int? limit = null, int? offset = null,
                                                           Include inc = Include.None, ReleaseType? type = null)
    => Utils.ResultOf(this.BrowseReleaseGroupsAsync(release, limit, offset, inc, type));

  /// <summary>Returns (the specified subset of) the release groups associated with the given artist.</summary>
  /// <param name="artist">The artist whose release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IReleaseGroup>> BrowseReleaseGroupsAsync(IArtist artist, int? limit = null, int? offset = null,
                                                                      Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.BuildExtraText(inc, "artist", artist.Id, type), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the release groups in the given collection.</summary>
  /// <param name="collection">The collection whose contained release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IReleaseGroup>> BrowseReleaseGroupsAsync(ICollection collection, int? limit = null, int? offset = null,
                                                                      Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.BuildExtraText(inc, "collection", collection.Id, type), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the release groups associated with the given release.</summary>
  /// <param name="release">The release whose release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  /// <remarks>
  /// Currently a release can only be part of a single release group, so this should always return exactly one result.
  /// </remarks>
  public Task<IBrowseResults<IReleaseGroup>> BrowseReleaseGroupsAsync(IRelease release, int? limit = null, int? offset = null,
                                                                      Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.BuildExtraText(inc, "release", release.Id, type), limit, offset).NextAsync();

}
