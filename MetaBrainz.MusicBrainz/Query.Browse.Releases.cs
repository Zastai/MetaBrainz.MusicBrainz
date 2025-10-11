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

  /// <summary>Returns the releases associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose releases should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>
  /// The requested releases.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllAreaReleases(Guid mbid, int? pageSize = null, int? offset = null,
                                                                Include inc = Include.None, ReleaseType? type = null,
                                                                ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.CreateOptions("area", mbid, inc, type, status), pageSize, offset).AsStream();

  /// <summary>Returns the releases associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose releases should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>
  /// The requested releases.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllArtistReleases(Guid mbid, int? pageSize = null, int? offset = null,
                                                                  Include inc = Include.None, ReleaseType? type = null,
                                                                  ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.CreateOptions("artist", mbid, inc, type, status), pageSize, offset).AsStream();

  /// <summary>Returns the releases in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained releases should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>
  /// The requested releases.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllCollectionReleases(Guid mbid, int? pageSize = null, int? offset = null,
                                                                      Include inc = Include.None, ReleaseType? type = null,
                                                                      ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.CreateOptions("collection", mbid, inc, type, status), pageSize, offset).AsStream();

  /// <summary>Returns the releases associated with the given label.</summary>
  /// <param name="mbid">The MBID for the label whose releases should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>
  /// The requested releases.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllLabelReleases(Guid mbid, int? pageSize = null, int? offset = null,
                                                                 Include inc = Include.None, ReleaseType? type = null,
                                                                 ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.CreateOptions("label", mbid, inc, type, status), pageSize, offset).AsStream();

  /// <summary>Returns the releases associated with the given recording.</summary>
  /// <param name="mbid">The MBID for the recording whose releases should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>
  /// The requested releases.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllRecordingReleases(Guid mbid, int? pageSize = null, int? offset = null,
                                                                     Include inc = Include.None, ReleaseType? type = null,
                                                                     ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.CreateOptions("recording", mbid, inc, type, status), pageSize, offset).AsStream();

  /// <summary>Returns the releases associated with the given release group.</summary>
  /// <param name="mbid">The MBID for the release group whose releases should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>
  /// The requested releases.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllReleaseGroupReleases(Guid mbid, int? pageSize = null, int? offset = null,
                                                                        Include inc = Include.None, ReleaseType? type = null,
                                                                        ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.CreateOptions("release-group", mbid, inc, type, status), pageSize, offset).AsStream();

  /// <summary>Returns the releases associated with the given area.</summary>
  /// <param name="area">The area whose releases should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>
  /// The requested releases.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllReleases(IArea area, int? pageSize = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.CreateOptions("area", area.Id, inc, type, status), pageSize, offset).AsStream();

  /// <summary>Returns the releases associated with the given artist.</summary>
  /// <param name="artist">The artist whose releases should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>
  /// The requested releases.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllReleases(IArtist artist, int? pageSize = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.CreateOptions("artist", artist.Id, inc, type, status), pageSize, offset).AsStream();

  /// <summary>Returns the releases in the given collection.</summary>
  /// <param name="collection">The collection whose contained releases should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>
  /// The requested releases.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllReleases(ICollection collection, int? pageSize = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.CreateOptions("collection", collection.Id, inc, type, status), pageSize, offset)
      .AsStream();

  /// <summary>Returns the releases associated with the given label.</summary>
  /// <param name="label">The label whose releases should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>
  /// The requested releases.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllReleases(ILabel label, int? pageSize = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.CreateOptions("label", label.Id, inc, type, status), pageSize, offset).AsStream();

  /// <summary>Returns the releases associated with the given recording.</summary>
  /// <param name="recording">The recording whose releases should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>
  /// The requested releases.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllReleases(IRecording recording, int? pageSize = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.CreateOptions("recording", recording.Id, inc, type, status), pageSize, offset).AsStream();

  /// <summary>Returns the releases associated with the given release group.</summary>
  /// <param name="releaseGroup">The release group whose releases should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>
  /// The requested releases.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllReleases(IReleaseGroup releaseGroup, int? pageSize = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.CreateOptions("release-group", releaseGroup.Id, inc, type, status), pageSize, offset)
      .AsStream();

  /// <summary>Returns the releases associated with the given track.</summary>
  /// <param name="track">The track whose releases should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>
  /// The requested releases.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllReleases(ITrack track, int? pageSize = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.CreateOptions("track", track.Id, inc, type, status), pageSize, offset).AsStream();

  /// <summary>
  /// Returns the releases that include the given artist in a track-level artist credit only.
  /// </summary>
  /// <param name="mbid">The MBID for the artist whose releases should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>
  /// The requested releases.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllTrackArtistReleases(Guid mbid, int? pageSize = null, int? offset = null,
                                                                       Include inc = Include.None, ReleaseType? type = null,
                                                                       ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.CreateOptions("track_artist", mbid, inc, type, status), pageSize, offset).AsStream();

  /// <summary>
  /// Returns the releases that include the given artist in a track-level artist credit only.
  /// </summary>
  /// <param name="artist">The artist whose releases should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>
  /// The requested releases.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllTrackArtistReleases(IArtist artist, int? pageSize = null, int? offset = null,
                                                                       Include inc = Include.None, ReleaseType? type = null,
                                                                       ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.CreateOptions("track_artist", artist.Id, inc, type, status), pageSize, offset).AsStream();

  /// <summary>Returns the releases associated with the given track.</summary>
  /// <param name="mbid">The MBID for the track whose releases should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>
  /// The requested releases.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllTrackReleases(Guid mbid, int? pageSize = null, int? offset = null,
                                                                 Include inc = Include.None, ReleaseType? type = null,
                                                                 ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.CreateOptions("track", mbid, inc, type, status), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the releases associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseAreaReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                Include inc = Include.None, ReleaseType? type = null,
                                                                ReleaseStatus? status = null,
                                                                CancellationToken cancellationToken = default)
    => new BrowseReleases(this, Query.CreateOptions("area", mbid, inc, type, status), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the releases associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseArtistReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                  Include inc = Include.None, ReleaseType? type = null,
                                                                  ReleaseStatus? status = null,
                                                                  CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.CreateOptions("artist", mbid, inc, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the releases in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseCollectionReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                      Include inc = Include.None, ReleaseType? type = null,
                                                                      ReleaseStatus? status = null,
                                                                      CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.CreateOptions("collection", mbid, inc, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the releases associated with the given label.</summary>
  /// <param name="mbid">The MBID for the label whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseLabelReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                 Include inc = Include.None, ReleaseType? type = null,
                                                                 ReleaseStatus? status = null,
                                                                 CancellationToken cancellationToken = default)
    => new BrowseReleases(this, Query.CreateOptions("label", mbid, inc, type, status), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the releases associated with the given recording.</summary>
  /// <param name="mbid">The MBID for the recording whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseRecordingReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                     Include inc = Include.None, ReleaseType? type = null,
                                                                     ReleaseStatus? status = null,
                                                                     CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.CreateOptions("recording", mbid, inc, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the releases associated with the given release group.</summary>
  /// <param name="mbid">The MBID for the release group whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleaseGroupReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                        Include inc = Include.None, ReleaseType? type = null,
                                                                        ReleaseStatus? status = null,
                                                                        CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.CreateOptions("release-group", mbid, inc, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the releases associated with the given area.</summary>
  /// <param name="area">The area whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(IArea area, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null,
                                                            CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.CreateOptions("area", area.Id, inc, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the releases associated with the given artist.</summary>
  /// <param name="artist">The artist whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(IArtist artist, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null,
                                                            CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.CreateOptions("artist", artist.Id, inc, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the releases in the given collection.</summary>
  /// <param name="collection">The collection whose contained releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(ICollection collection, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null,
                                                            CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.CreateOptions("collection", collection.Id, inc, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the releases associated with the given label.</summary>
  /// <param name="label">The label whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(ILabel label, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null,
                                                            CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.CreateOptions("label", label.Id, inc, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the releases associated with the given recording.</summary>
  /// <param name="recording">The recording whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(IRecording recording, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null,
                                                            CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.CreateOptions("recording", recording.Id, inc, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the releases associated with the given release group.</summary>
  /// <param name="releaseGroup">The release group whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(IReleaseGroup releaseGroup, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null,
                                                            CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.CreateOptions("release-group", releaseGroup.Id, inc, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the releases associated with the given track.</summary>
  /// <param name="track">The track whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(ITrack track, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null,
                                                            CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.CreateOptions("track", track.Id, inc, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>
  /// Returns (the specified subset of) the releases that include the given artist in a track-level artist credit only.
  /// </summary>
  /// <param name="mbid">The MBID for the artist whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseTrackArtistReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                       Include inc = Include.None, ReleaseType? type = null,
                                                                       ReleaseStatus? status = null,
                                                                       CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.CreateOptions("track_artist", mbid, inc, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>
  /// Returns (the specified subset of) the releases that include the given artist in a track-level artist credit only.
  /// </summary>
  /// <param name="artist">The artist whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseTrackArtistReleasesAsync(IArtist artist, int? limit = null, int? offset = null,
                                                                       Include inc = Include.None, ReleaseType? type = null,
                                                                       ReleaseStatus? status = null,
                                                                       CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.CreateOptions("track_artist", artist.Id, inc, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the releases associated with the given track.</summary>
  /// <param name="mbid">The MBID for the track whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseTrackReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                 Include inc = Include.None, ReleaseType? type = null,
                                                                 ReleaseStatus? status = null,
                                                                 CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.CreateOptions("track", mbid, inc, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

}
