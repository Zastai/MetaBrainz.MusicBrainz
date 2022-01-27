using System;
using System.Net;
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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllAreaReleases(Guid mbid, int? pageSize = null, int? offset = null,
                                                                Include inc = Include.None, ReleaseType? type = null,
                                                                ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "area", mbid, type, status), pageSize, offset).AsStream();

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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllArtistReleases(Guid mbid, int? pageSize = null, int? offset = null,
                                                                  Include inc = Include.None, ReleaseType? type = null,
                                                                  ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "artist", mbid, type, status), pageSize, offset).AsStream();

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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllCollectionReleases(Guid mbid, int? pageSize = null, int? offset = null,
                                                                      Include inc = Include.None, ReleaseType? type = null,
                                                                      ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "collection", mbid, type, status), pageSize, offset).AsStream();

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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllLabelReleases(Guid mbid, int? pageSize = null, int? offset = null,
                                                                 Include inc = Include.None, ReleaseType? type = null,
                                                                 ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "label", mbid, type, status), pageSize, offset).AsStream();

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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllRecordingReleases(Guid mbid, int? pageSize = null, int? offset = null,
                                                                     Include inc = Include.None, ReleaseType? type = null,
                                                                     ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "recording", mbid, type, status), pageSize, offset).AsStream();

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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllReleaseGroupReleases(Guid mbid, int? pageSize = null, int? offset = null,
                                                                        Include inc = Include.None, ReleaseType? type = null,
                                                                        ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "release-group", mbid, type, status), pageSize, offset).AsStream();

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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllReleases(IArea area, int? pageSize = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "area", area.Id, type, status), pageSize, offset).AsStream();

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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllReleases(IArtist artist, int? pageSize = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "artist", artist.Id, type, status), pageSize, offset).AsStream();

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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllReleases(ICollection collection, int? pageSize = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "collection", collection.Id, type, status), pageSize, offset)
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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllReleases(ILabel label, int? pageSize = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "label", label.Id, type, status), pageSize, offset).AsStream();

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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllReleases(IRecording recording, int? pageSize = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "recording", recording.Id, type, status), pageSize, offset).AsStream();

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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllReleases(IReleaseGroup releaseGroup, int? pageSize = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "release-group", releaseGroup.Id, type, status), pageSize, offset)
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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllReleases(ITrack track, int? pageSize = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "track", track.Id, type, status), pageSize, offset).AsStream();

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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllTrackArtistReleases(Guid mbid, int? pageSize = null, int? offset = null,
                                                                       Include inc = Include.None, ReleaseType? type = null,
                                                                       ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "track_artist", mbid, type, status), pageSize, offset).AsStream();

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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllTrackArtistReleases(IArtist artist, int? pageSize = null, int? offset = null,
                                                                       Include inc = Include.None, ReleaseType? type = null,
                                                                       ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "track_artist", artist.Id, type, status), pageSize, offset).AsStream();

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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRelease> BrowseAllTrackReleases(Guid mbid, int? pageSize = null, int? offset = null,
                                                                 Include inc = Include.None, ReleaseType? type = null,
                                                                 ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "track", mbid, type, status), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the releases associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRelease> BrowseAreaReleases(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None,
                                                     ReleaseType? type = null, ReleaseStatus? status = null)
    => AsyncUtils.ResultOf(this.BrowseAreaReleasesAsync(mbid, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseAreaReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                Include inc = Include.None, ReleaseType? type = null,
                                                                ReleaseStatus? status = null,
                                                                CancellationToken cancellationToken = default)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "area", mbid, type, status), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the releases associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRelease> BrowseArtistReleases(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None,
                                                       ReleaseType? type = null, ReleaseStatus? status = null)
    => AsyncUtils.ResultOf(this.BrowseArtistReleasesAsync(mbid, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseArtistReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                  Include inc = Include.None, ReleaseType? type = null,
                                                                  ReleaseStatus? status = null,
                                                                  CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.BuildExtraText(inc, "artist", mbid, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the releases in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRelease> BrowseCollectionReleases(Guid mbid, int? limit = null, int? offset = null,
                                                           Include inc = Include.None, ReleaseType? type = null,
                                                           ReleaseStatus? status = null)
    => AsyncUtils.ResultOf(this.BrowseCollectionReleasesAsync(mbid, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseCollectionReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                      Include inc = Include.None, ReleaseType? type = null,
                                                                      ReleaseStatus? status = null,
                                                                      CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.BuildExtraText(inc, "collection", mbid, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the releases associated with the given label.</summary>
  /// <param name="mbid">The MBID for the label whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRelease> BrowseLabelReleases(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None,
                                                      ReleaseType? type = null, ReleaseStatus? status = null)
    => AsyncUtils.ResultOf(this.BrowseLabelReleasesAsync(mbid, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases associated with the given label.</summary>
  /// <param name="mbid">The MBID for the label whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseLabelReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                 Include inc = Include.None, ReleaseType? type = null,
                                                                 ReleaseStatus? status = null,
                                                                 CancellationToken cancellationToken = default)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "label", mbid, type, status), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the releases associated with the given recording.</summary>
  /// <param name="mbid">The MBID for the recording whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRelease> BrowseRecordingReleases(Guid mbid, int? limit = null, int? offset = null,
                                                          Include inc = Include.None, ReleaseType? type = null,
                                                          ReleaseStatus? status = null)
    => AsyncUtils.ResultOf(this.BrowseRecordingReleasesAsync(mbid, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases associated with the given recording.</summary>
  /// <param name="mbid">The MBID for the recording whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseRecordingReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                     Include inc = Include.None, ReleaseType? type = null,
                                                                     ReleaseStatus? status = null,
                                                                     CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.BuildExtraText(inc, "recording", mbid, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the releases associated with the given release group.</summary>
  /// <param name="mbid">The MBID for the release group whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRelease> BrowseReleaseGroupReleases(Guid mbid, int? limit = null, int? offset = null,
                                                             Include inc = Include.None, ReleaseType? type = null,
                                                             ReleaseStatus? status = null)
    => AsyncUtils.ResultOf(this.BrowseReleaseGroupReleasesAsync(mbid, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases associated with the given release group.</summary>
  /// <param name="mbid">The MBID for the release group whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleaseGroupReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                        Include inc = Include.None, ReleaseType? type = null,
                                                                        ReleaseStatus? status = null,
                                                                        CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.BuildExtraText(inc, "release-group", mbid, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the releases associated with the given area.</summary>
  /// <param name="area">The area whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRelease> BrowseReleases(IArea area, int? limit = null, int? offset = null, Include inc = Include.None,
                                                 ReleaseType? type = null, ReleaseStatus? status = null)
    => AsyncUtils.ResultOf(this.BrowseReleasesAsync(area, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases associated with the given artist.</summary>
  /// <param name="artist">The artist whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRelease> BrowseReleases(IArtist artist, int? limit = null, int? offset = null, Include inc = Include.None,
                                                 ReleaseType? type = null, ReleaseStatus? status = null)
    => AsyncUtils.ResultOf(this.BrowseReleasesAsync(artist, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases in the given collection.</summary>
  /// <param name="collection">The collection whose contained releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRelease> BrowseReleases(ICollection collection, int? limit = null, int? offset = null,
                                                 Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null)
    => AsyncUtils.ResultOf(this.BrowseReleasesAsync(collection, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases associated with the given label.</summary>
  /// <param name="label">The label whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRelease> BrowseReleases(ILabel label, int? limit = null, int? offset = null, Include inc = Include.None,
                                                 ReleaseType? type = null, ReleaseStatus? status = null)
    => AsyncUtils.ResultOf(this.BrowseReleasesAsync(label, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases associated with the given recording.</summary>
  /// <param name="recording">The recording whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRelease> BrowseReleases(IRecording recording, int? limit = null, int? offset = null,
                                                 Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null)
    => AsyncUtils.ResultOf(this.BrowseReleasesAsync(recording, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases associated with the given release group.</summary>
  /// <param name="releaseGroup">The release group whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRelease> BrowseReleases(IReleaseGroup releaseGroup, int? limit = null, int? offset = null,
                                                 Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, "release-group", releaseGroup.Id, type, status), limit, offset)
      .Next();

  /// <summary>Returns (the specified subset of) the releases associated with the given track.</summary>
  /// <param name="track">The track whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRelease> BrowseReleases(ITrack track, int? limit = null, int? offset = null, Include inc = Include.None,
                                                 ReleaseType? type = null, ReleaseStatus? status = null)
    => AsyncUtils.ResultOf(this.BrowseReleasesAsync(track, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases associated with the given area.</summary>
  /// <param name="area">The area whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(IArea area, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null,
                                                            CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.BuildExtraText(inc, "area", area.Id, type, status), limit, offset);
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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(IArtist artist, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null,
                                                            CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.BuildExtraText(inc, "artist", artist.Id, type, status), limit, offset);
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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(ICollection collection, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null,
                                                            CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.BuildExtraText(inc, "collection", collection.Id, type, status), limit, offset);
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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(ILabel label, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null,
                                                            CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.BuildExtraText(inc, "label", label.Id, type, status), limit, offset);
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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(IRecording recording, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null,
                                                            CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.BuildExtraText(inc, "recording", recording.Id, type, status), limit, offset);
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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(IReleaseGroup releaseGroup, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null,
                                                            CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.BuildExtraText(inc, "release-group", releaseGroup.Id, type, status), limit, offset);
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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(ITrack track, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null,
                                                            CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.BuildExtraText(inc, "track", track.Id, type, status), limit, offset);
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
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRelease> BrowseTrackArtistReleases(Guid mbid, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => AsyncUtils.ResultOf(this.BrowseTrackArtistReleasesAsync(mbid, limit, offset, inc, type, status));

  /// <summary>
  /// Returns (the specified subset of) the releases that include the given artist in a track-level artist credit only.
  /// </summary>
  /// <param name="artist">The artist whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRelease> BrowseTrackArtistReleases(IArtist artist, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => AsyncUtils.ResultOf(this.BrowseTrackArtistReleasesAsync(artist, limit, offset, inc, type, status));

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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseTrackArtistReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                       Include inc = Include.None, ReleaseType? type = null,
                                                                       ReleaseStatus? status = null,
                                                                       CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.BuildExtraText(inc, "track_artist", mbid, type, status), limit, offset);
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
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseTrackArtistReleasesAsync(IArtist artist, int? limit = null, int? offset = null,
                                                                       Include inc = Include.None, ReleaseType? type = null,
                                                                       ReleaseStatus? status = null,
                                                                       CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.BuildExtraText(inc, "track_artist", artist.Id, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the releases associated with the given track.</summary>
  /// <param name="mbid">The MBID for the track whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRelease> BrowseTrackReleases(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None,
                                                      ReleaseType? type = null, ReleaseStatus? status = null)
    => AsyncUtils.ResultOf(this.BrowseTrackReleasesAsync(mbid, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases associated with the given track.</summary>
  /// <param name="mbid">The MBID for the track whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseTrackReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                 Include inc = Include.None, ReleaseType? type = null,
                                                                 ReleaseStatus? status = null,
                                                                 CancellationToken cancellationToken = default) {
    var browse = new BrowseReleases(this, Query.BuildExtraText(inc, "track", mbid, type, status), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

}
