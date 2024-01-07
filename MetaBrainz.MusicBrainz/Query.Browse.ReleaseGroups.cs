using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.Common;
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
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IReleaseGroup> BrowseAllArtistReleaseGroups(Guid mbid, int? pageSize = null, int? offset = null,
                                                                            Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.CreateOptions("artist", mbid, inc, type), pageSize, offset).AsStream();

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
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IReleaseGroup> BrowseAllCollectionReleaseGroups(Guid mbid, int? pageSize = null, int? offset = null,
                                                                                Include inc = Include.None,
                                                                                ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.CreateOptions("collection", mbid, inc, type), pageSize, offset).AsStream();

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
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  /// <remarks>
  /// Currently a release can only be part of a single release group, so assuming <paramref name="mbid"/> is valid, this should
  /// always return exactly one result.
  /// </remarks>
  public IStreamingQueryResults<IReleaseGroup> BrowseAllReleaseReleaseGroups(Guid mbid, int? pageSize = null, int? offset = null,
                                                                             Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.CreateOptions("release", mbid, inc, type), pageSize, offset).AsStream();

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
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IReleaseGroup> BrowseAllReleaseGroups(IArtist artist, int? pageSize = null, int? offset = null,
                                                                      Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.CreateOptions("artist", artist.Id, inc, type), pageSize, offset).AsStream();

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
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IReleaseGroup> BrowseAllReleaseGroups(ICollection collection, int? pageSize = null,
                                                                      int? offset = null, Include inc = Include.None,
                                                                      ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.CreateOptions("collection", collection.Id, inc, type), pageSize, offset).AsStream();

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
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  /// <remarks>
  /// Currently a release can only be part of a single release group, so this should always return exactly one result.
  /// </remarks>
  public IStreamingQueryResults<IReleaseGroup> BrowseAllReleaseGroups(IRelease release, int? pageSize = null, int? offset = null,
                                                                      Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.CreateOptions("release", release.Id, inc, type), pageSize, offset).AsStream();

  /// <inheritdoc cref="BrowseArtistReleaseGroupsAsync"/>
  public IBrowseResults<IReleaseGroup> BrowseArtistReleaseGroups(Guid mbid, int? limit = null, int? offset = null,
                                                                 Include inc = Include.None, ReleaseType? type = null)
    => AsyncUtils.ResultOf(this.BrowseArtistReleaseGroupsAsync(mbid, limit, offset, inc, type));

  /// <summary>Returns (the specified subset of) the release groups associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IReleaseGroup>> BrowseArtistReleaseGroupsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                            Include inc = Include.None, ReleaseType? type = null,
                                                                            CancellationToken cancellationToken = default)
    => new BrowseReleaseGroups(this, Query.CreateOptions("artist", mbid, inc, type), limit, offset).NextAsync(cancellationToken);

  /// <inheritdoc cref="BrowseCollectionReleaseGroupsAsync"/>
  public IBrowseResults<IReleaseGroup> BrowseCollectionReleaseGroups(Guid mbid, int? limit = null, int? offset = null,
                                                                     Include inc = Include.None, ReleaseType? type = null)
    => AsyncUtils.ResultOf(this.BrowseCollectionReleaseGroupsAsync(mbid, limit, offset, inc, type));

  /// <summary>Returns (the specified subset of) the release groups in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IReleaseGroup>> BrowseCollectionReleaseGroupsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                                Include inc = Include.None,
                                                                                ReleaseType? type = null,
                                                                                CancellationToken cancellationToken = default) {
    var browse = new BrowseReleaseGroups(this, Query.CreateOptions("collection", mbid, inc, type), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <inheritdoc cref="BrowseReleaseReleaseGroupsAsync"/>
  public IBrowseResults<IReleaseGroup> BrowseReleaseReleaseGroups(Guid mbid, int? limit = null, int? offset = null,
                                                                  Include inc = Include.None, ReleaseType? type = null)
    => AsyncUtils.ResultOf(this.BrowseReleaseReleaseGroupsAsync(mbid, limit, offset, inc, type));

  /// <summary>Returns (the specified subset of) the release groups associated with the given release.</summary>
  /// <param name="mbid">The MBID for the release whose release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  /// <remarks>
  /// Currently a release can only be part of a single release group, so assuming <paramref name="mbid"/> is valid, this should
  /// always return exactly one result.
  /// </remarks>
  public Task<IBrowseResults<IReleaseGroup>> BrowseReleaseReleaseGroupsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                             Include inc = Include.None, ReleaseType? type = null,
                                                                             CancellationToken cancellationToken = default)
    => new BrowseReleaseGroups(this, Query.CreateOptions("release", mbid, inc, type), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the release groups associated with the given artist.</summary>
  /// <param name="artist">The artist whose release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<IReleaseGroup> BrowseReleaseGroups(IArtist artist, int? limit = null, int? offset = null,
                                                           Include inc = Include.None, ReleaseType? type = null)
    => AsyncUtils.ResultOf(this.BrowseReleaseGroupsAsync(artist, limit, offset, inc, type));

  /// <summary>Returns (the specified subset of) the release groups in the given collection.</summary>
  /// <param name="collection">The collection whose contained release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<IReleaseGroup> BrowseReleaseGroups(ICollection collection, int? limit = null, int? offset = null,
                                                           Include inc = Include.None, ReleaseType? type = null)
    => AsyncUtils.ResultOf(this.BrowseReleaseGroupsAsync(collection, limit, offset, inc, type));

  /// <summary>Returns (the specified subset of) the release groups associated with the given release.</summary>
  /// <param name="release">The release whose release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  /// <remarks>
  /// Currently a release can only be part of a single release group, so this should always return exactly one result.
  /// </remarks>
  public IBrowseResults<IReleaseGroup> BrowseReleaseGroups(IRelease release, int? limit = null, int? offset = null,
                                                           Include inc = Include.None, ReleaseType? type = null)
    => AsyncUtils.ResultOf(this.BrowseReleaseGroupsAsync(release, limit, offset, inc, type));

  /// <summary>Returns (the specified subset of) the release groups associated with the given artist.</summary>
  /// <param name="artist">The artist whose release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IReleaseGroup>> BrowseReleaseGroupsAsync(IArtist artist, int? limit = null, int? offset = null,
                                                                      Include inc = Include.None, ReleaseType? type = null,
                                                                      CancellationToken cancellationToken = default) {
    var browse = new BrowseReleaseGroups(this, Query.CreateOptions("artist", artist.Id, inc, type), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the release groups in the given collection.</summary>
  /// <param name="collection">The collection whose contained release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IReleaseGroup>> BrowseReleaseGroupsAsync(ICollection collection, int? limit = null, int? offset = null,
                                                                      Include inc = Include.None, ReleaseType? type = null,
                                                                      CancellationToken cancellationToken = default) {
    var browse = new BrowseReleaseGroups(this, Query.CreateOptions("collection", collection.Id, inc, type), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the release groups associated with the given release.</summary>
  /// <param name="release">The release whose release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  /// <remarks>
  /// Currently a release can only be part of a single release group, so this should always return exactly one result.
  /// </remarks>
  public Task<IBrowseResults<IReleaseGroup>> BrowseReleaseGroupsAsync(IRelease release, int? limit = null, int? offset = null,
                                                                      Include inc = Include.None, ReleaseType? type = null,
                                                                      CancellationToken cancellationToken = default) {
    var browse = new BrowseReleaseGroups(this, Query.CreateOptions("release", release.Id, inc, type), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

}
