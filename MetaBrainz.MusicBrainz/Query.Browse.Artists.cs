using System;
using System.Net;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces;
using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Browses;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Returns the artists associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose artists should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested artists.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IArtist> BrowseAllAreaArtists(Guid mbid, int? pageSize = null, int? offset = null,
                                                              Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "area", mbid), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the releases associated with the given area.</summary>
  /// <param name="area">The area whose artists should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested artists.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IArtist> BrowseAllArtists(IArea area, int? pageSize = null, int? offset = null,
                                                          Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "area", area.Id), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the artists in the given collection.</summary>
  /// <param name="collection">The collection whose contained artists should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested artists.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IArtist> BrowseAllArtists(ICollection collection, int? pageSize = null, int? offset = null,
                                                          Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "collection", collection.Id), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the releases associated with the given recording.</summary>
  /// <param name="recording">The recording whose artists should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested artists.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IArtist> BrowseAllArtists(IRecording recording, int? pageSize = null, int? offset = null,
                                                          Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "recording", recording.Id), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the releases associated with the given release.</summary>
  /// <param name="release">The release whose artists should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested artists.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IArtist> BrowseAllArtists(IRelease release, int? pageSize = null, int? offset = null,
                                                          Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "release", release.Id), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the releases associated with the given release group.</summary>
  /// <param name="releaseGroup">The release group whose artists should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested artists.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IArtist> BrowseAllArtists(IReleaseGroup releaseGroup, int? pageSize = null, int? offset = null,
                                                          Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "release-group", releaseGroup.Id), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the releases associated with the given work.</summary>
  /// <param name="work">The work whose artists should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested artists.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IArtist> BrowseAllArtists(IWork work, int? pageSize = null, int? offset = null,
                                                          Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "work", work.Id), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the artists in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained artists should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested artists.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IArtist> BrowseAllCollectionArtists(Guid mbid, int? pageSize = null, int? offset = null,
                                                                    Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "collection", mbid), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the artists associated with the given recording.</summary>
  /// <param name="mbid">The MBID for the recording whose artists should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested artists.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IArtist> BrowseAllRecordingArtists(Guid mbid, int? pageSize = null, int? offset = null,
                                                                   Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "recording", mbid), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the artists associated with the given release.</summary>
  /// <param name="mbid">The MBID for the release whose artists should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested artists.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IArtist> BrowseAllReleaseArtists(Guid mbid, int? pageSize = null, int? offset = null,
                                                                 Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "release", mbid), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the artists associated with the given release group.</summary>
  /// <param name="mbid">The MBID for the release group whose artists should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested artists.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IArtist> BrowseAllReleaseGroupArtists(Guid mbid, int? pageSize = null, int? offset = null,
                                                                      Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "release-group", mbid), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the artists associated with the given work.</summary>
  /// <param name="mbid">The MBID for the work whose artists should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested artists.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IArtist> BrowseAllWorkArtists(Guid mbid, int? pageSize = null, int? offset = null,
                                                              Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "work", mbid), pageSize, offset).AsStream();

  /// <inheritdoc cref="BrowseAreaArtistsAsync"/>
  public IBrowseResults<IArtist> BrowseAreaArtists(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseAreaArtistsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the artists associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseAreaArtistsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                              Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "area", mbid), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseArtistsAsync(IArea,int?,int?,Include)"/>
  public IBrowseResults<IArtist> BrowseArtists(IArea area, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseArtistsAsync(area, limit, offset, inc));

  /// <inheritdoc cref="BrowseArtistsAsync(ICollection,int?,int?,Include)"/>
  public IBrowseResults<IArtist> BrowseArtists(ICollection collection, int? limit = null, int? offset = null,
                                               Include inc = Include.None)
    => Utils.ResultOf(this.BrowseArtistsAsync(collection, limit, offset, inc));

  /// <inheritdoc cref="BrowseArtistsAsync(IRecording,int?,int?,Include)"/>
  public IBrowseResults<IArtist> BrowseArtists(IRecording recording, int? limit = null, int? offset = null,
                                               Include inc = Include.None)
    => Utils.ResultOf(this.BrowseArtistsAsync(recording, limit, offset, inc));

  /// <inheritdoc cref="BrowseArtistsAsync(IRelease,int?,int?,Include)"/>
  public IBrowseResults<IArtist> BrowseArtists(IRelease release, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseArtistsAsync(release, limit, offset, inc));

  /// <inheritdoc cref="BrowseArtistsAsync(IReleaseGroup,int?,int?,Include)"/>
  public IBrowseResults<IArtist> BrowseArtists(IReleaseGroup releaseGroup, int? limit = null, int? offset = null,
                                               Include inc = Include.None)
    => Utils.ResultOf(this.BrowseArtistsAsync(releaseGroup, limit, offset, inc));

  /// <inheritdoc cref="BrowseArtistsAsync(IWork,int?,int?,Include)"/>
  public IBrowseResults<IArtist> BrowseArtists(IWork work, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseArtistsAsync(work, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the releases associated with the given area.</summary>
  /// <param name="area">The area whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseArtistsAsync(IArea area, int? limit = null, int? offset = null,
                                                          Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "area", area.Id), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the artists in the given collection.</summary>
  /// <param name="collection">The collection whose contained artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseArtistsAsync(ICollection collection, int? limit = null, int? offset = null,
                                                          Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "collection", collection.Id), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the releases associated with the given recording.</summary>
  /// <param name="recording">The recording whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseArtistsAsync(IRecording recording, int? limit = null, int? offset = null,
                                                          Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "recording", recording.Id), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the releases associated with the given release.</summary>
  /// <param name="release">The release whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseArtistsAsync(IRelease release, int? limit = null, int? offset = null,
                                                          Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "release", release.Id), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the releases associated with the given release group.</summary>
  /// <param name="releaseGroup">The release group whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseArtistsAsync(IReleaseGroup releaseGroup, int? limit = null, int? offset = null,
                                                          Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "release-group", releaseGroup.Id), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the releases associated with the given work.</summary>
  /// <param name="work">The work whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseArtistsAsync(IWork work, int? limit = null, int? offset = null,
                                                          Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "work", work.Id), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseCollectionArtistsAsync"/>
  public IBrowseResults<IArtist> BrowseCollectionArtists(Guid mbid, int? limit = null, int? offset = null,
                                                         Include inc = Include.None)
    => Utils.ResultOf(this.BrowseCollectionArtistsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the artists in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseCollectionArtistsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                    Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "collection", mbid), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseRecordingArtistsAsync"/>
  public IBrowseResults<IArtist> BrowseRecordingArtists(Guid mbid, int? limit = null, int? offset = null,
                                                        Include inc = Include.None)
    => Utils.ResultOf(this.BrowseRecordingArtistsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the artists associated with the given recording.</summary>
  /// <param name="mbid">The MBID for the recording whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseRecordingArtistsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                   Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "recording", mbid), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseReleaseArtistsAsync"/>
  public IBrowseResults<IArtist> BrowseReleaseArtists(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseReleaseArtistsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the artists associated with the given release.</summary>
  /// <param name="mbid">The MBID for the release whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseReleaseArtistsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                 Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "release", mbid), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseReleaseGroupArtistsAsync"/>
  public IBrowseResults<IArtist> BrowseReleaseGroupArtists(Guid mbid, int? limit = null, int? offset = null,
                                                           Include inc = Include.None)
    => Utils.ResultOf(this.BrowseReleaseGroupArtistsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the artists associated with the given release group.</summary>
  /// <param name="mbid">The MBID for the release group whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseReleaseGroupArtistsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                      Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "release-group", mbid), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseWorkArtistsAsync"/>
  public IBrowseResults<IArtist> BrowseWorkArtists(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseWorkArtistsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the artists associated with the given work.</summary>
  /// <param name="mbid">The MBID for the work whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseWorkArtistsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                              Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, "work", mbid), limit, offset).NextAsync();

}