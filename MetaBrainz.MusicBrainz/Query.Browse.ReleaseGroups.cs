using System;
using System.Net;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Browses;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Returns (the specified subset of) the release groups associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IReleaseGroup> BrowseArtistReleaseGroups(Guid mbid, int? limit = null, int? offset = null,
                                                                 Include inc = Include.None, ReleaseType? type = null)
    => Utils.ResultOf(this.BrowseArtistReleaseGroupsAsync(mbid, limit, offset, inc, type));

  /// <summary>Returns (the specified subset of) the release groups associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IReleaseGroup>> BrowseArtistReleaseGroupsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                            Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.BuildExtraText(inc, $"artist={mbid:D}", type), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the release groups in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IReleaseGroup> BrowseCollectionReleaseGroups(Guid mbid, int? limit = null, int? offset = null,
                                                                     Include inc = Include.None, ReleaseType? type = null)
    => Utils.ResultOf(this.BrowseCollectionReleaseGroupsAsync(mbid, limit, offset, inc, type));

  /// <summary>Returns (the specified subset of) the release groups in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IReleaseGroup>> BrowseCollectionReleaseGroupsAsync(
    Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.BuildExtraText(inc, $"collection={mbid:D}", type), limit, offset).NextAsync();

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
  public IBrowseResults<IReleaseGroup> BrowseReleaseReleaseGroups(Guid mbid, int? limit = null, int? offset = null,
                                                                  Include inc = Include.None, ReleaseType? type = null)
    => Utils.ResultOf(this.BrowseReleaseReleaseGroupsAsync(mbid, limit, offset, inc, type));

  /// <summary>Returns (the specified subset of) the release groups associated with the given release.</summary>
  /// <param name="mbid">The MBID for the release whose release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  /// <remarks>
  /// Currently a release can only be part of a single release group, so assuming <paramref name="mbid"/> is valid, this should
  /// always return exactly one result.
  /// </remarks>
  public Task<IBrowseResults<IReleaseGroup>> BrowseReleaseReleaseGroupsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                             Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.BuildExtraText(inc, $"release={mbid:D}", type), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the release groups associated with the given artist.</summary>
  /// <param name="artist">The artist whose release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IReleaseGroup> BrowseReleaseGroups(IArtist artist, int? limit = null, int? offset = null,
                                                           Include inc = Include.None, ReleaseType? type = null)
    => Utils.ResultOf(this.BrowseReleaseGroupsAsync(artist, limit, offset, inc, type));

  /// <summary>Returns (the specified subset of) the release groups in the given collection.</summary>
  /// <param name="collection">The collection whose contained release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IReleaseGroup> BrowseReleaseGroups(ICollection collection, int? limit = null, int? offset = null,
                                                           Include inc = Include.None, ReleaseType? type = null)
    => Utils.ResultOf(this.BrowseReleaseGroupsAsync(collection, limit, offset, inc, type));

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
  public IBrowseResults<IReleaseGroup> BrowseReleaseGroups(IRelease release, int? limit = null, int? offset = null,
                                                           Include inc = Include.None, ReleaseType? type = null)
    => Utils.ResultOf(this.BrowseReleaseGroupsAsync(release, limit, offset, inc, type));

  /// <summary>Returns (the specified subset of) the release groups associated with the given artist.</summary>
  /// <param name="artist">The artist whose release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IReleaseGroup>> BrowseReleaseGroupsAsync(IArtist artist, int? limit = null, int? offset = null,
                                                                      Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.BuildExtraText(inc, $"artist={artist.Id:D}", type), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the release groups in the given collection.</summary>
  /// <param name="collection">The collection whose contained release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IReleaseGroup>> BrowseReleaseGroupsAsync(ICollection collection, int? limit = null, int? offset = null,
                                                                      Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.BuildExtraText(inc, $"collection={collection.Id:D}", type), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the release groups associated with the given release.</summary>
  /// <param name="release">The release whose release groups should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  /// <remarks>
  /// Currently a release can only be part of a single release group, so this should always return exactly one result.
  /// </remarks>
  public Task<IBrowseResults<IReleaseGroup>> BrowseReleaseGroupsAsync(IRelease release, int? limit = null, int? offset = null,
                                                                      Include inc = Include.None, ReleaseType? type = null)
    => new BrowseReleaseGroups(this, Query.BuildExtraText(inc, $"release={release.Id:D}", type), limit, offset).NextAsync();

}
