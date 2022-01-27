using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces;
using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Browses;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Returns the recordings associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose recordings should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested recordings.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRecording> BrowseAllArtistRecordings(Guid mbid, int? pageSize = null, int? offset = null,
                                                                      Include inc = Include.None)
    => new BrowseRecordings(this, Query.BuildExtraText(inc, "artist", mbid), pageSize, offset).AsStream();

  /// <summary>Returns the recordings in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained recordings should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested recordings.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRecording> BrowseAllCollectionRecordings(Guid mbid, int? pageSize = null, int? offset = null,
                                                                          Include inc = Include.None)
    => new BrowseRecordings(this, Query.BuildExtraText(inc, "collection", mbid), pageSize, offset).AsStream();

  /// <summary>Returns the recordings associated with the given artist.</summary>
  /// <param name="artist">The artist whose recordings should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested recordings.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRecording> BrowseAllRecordings(IArtist artist, int? pageSize = null, int? offset = null,
                                                                Include inc = Include.None)
    => new BrowseRecordings(this, Query.BuildExtraText(inc, "artist", artist.Id), pageSize, offset).AsStream();

  /// <summary>Returns the recordings in the given collection.</summary>
  /// <param name="collection">The collection whose contained recordings should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested recordings.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRecording> BrowseAllRecordings(ICollection collection, int? pageSize = null, int? offset = null,
                                                                Include inc = Include.None)
    => new BrowseRecordings(this, Query.BuildExtraText(inc, "collection", collection.Id), pageSize, offset).AsStream();

  /// <summary>Returns the recordings associated with the given release.</summary>
  /// <param name="release">The release whose recordings should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested recordings.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRecording> BrowseAllRecordings(IRelease release, int? pageSize = null, int? offset = null,
                                                                Include inc = Include.None)
    => new BrowseRecordings(this, Query.BuildExtraText(inc, "release", release.Id), pageSize, offset).AsStream();

  /// <summary>Returns the recordings associated with the given release.</summary>
  /// <param name="mbid">The MBID for the release whose recordings should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested recordings.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IRecording> BrowseAllReleaseRecordings(Guid mbid, int? pageSize = null, int? offset = null,
                                                                       Include inc = Include.None)
    => new BrowseRecordings(this, Query.BuildExtraText(inc, "release", mbid), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the recordings associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose recordings should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRecording> BrowseArtistRecordings(Guid mbid, int? limit = null, int? offset = null,
                                                           Include inc = Include.None)
    => Utils.ResultOf(this.BrowseArtistRecordingsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the recordings associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose recordings should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRecording>> BrowseArtistRecordingsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                      Include inc = Include.None,
                                                                      CancellationToken cancellationToken = default)
    => new BrowseRecordings(this, Query.BuildExtraText(inc, "artist", mbid), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the recordings in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained recordings should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRecording> BrowseCollectionRecordings(Guid mbid, int? limit = null, int? offset = null,
                                                               Include inc = Include.None)
    => Utils.ResultOf(this.BrowseCollectionRecordingsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the recordings in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained recordings should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRecording>> BrowseCollectionRecordingsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                          Include inc = Include.None,
                                                                          CancellationToken cancellationToken = default)
    => new BrowseRecordings(this, Query.BuildExtraText(inc, "collection", mbid), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the recordings associated with the given artist.</summary>
  /// <param name="artist">The artist whose recordings should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRecording> BrowseRecordings(IArtist artist, int? limit = null, int? offset = null,
                                                     Include inc = Include.None)
    => Utils.ResultOf(this.BrowseRecordingsAsync(artist, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the recordings in the given collection.</summary>
  /// <param name="collection">The collection whose contained recordings should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRecording> BrowseRecordings(ICollection collection, int? limit = null, int? offset = null,
                                                     Include inc = Include.None)
    => Utils.ResultOf(this.BrowseRecordingsAsync(collection, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the recordings associated with the given release.</summary>
  /// <param name="release">The release whose recordings should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRecording> BrowseRecordings(IRelease release, int? limit = null, int? offset = null,
                                                     Include inc = Include.None)
    => Utils.ResultOf(this.BrowseRecordingsAsync(release, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the recordings associated with the given artist.</summary>
  /// <param name="artist">The artist whose recordings should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRecording>> BrowseRecordingsAsync(IArtist artist, int? limit = null, int? offset = null,
                                                                Include inc = Include.None,
                                                                CancellationToken cancellationToken = default)
    => new BrowseRecordings(this, Query.BuildExtraText(inc, "artist", artist.Id), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the recordings in the given collection.</summary>
  /// <param name="collection">The collection whose contained recordings should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRecording>> BrowseRecordingsAsync(ICollection collection, int? limit = null, int? offset = null,
                                                                Include inc = Include.None,
                                                                CancellationToken cancellationToken = default) {
    var browse = new BrowseRecordings(this, Query.BuildExtraText(inc, "collection", collection.Id), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the recordings associated with the given release.</summary>
  /// <param name="release">The release whose recordings should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRecording>> BrowseRecordingsAsync(IRelease release, int? limit = null, int? offset = null,
                                                                Include inc = Include.None,
                                                                CancellationToken cancellationToken = default)
    => new BrowseRecordings(this, Query.BuildExtraText(inc, "release", release.Id), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the recordings associated with the given release.</summary>
  /// <param name="mbid">The MBID for the release whose recordings should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IRecording> BrowseReleaseRecordings(Guid mbid, int? limit = null, int? offset = null,
                                                            Include inc = Include.None)
    => Utils.ResultOf(this.BrowseReleaseRecordingsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the recordings associated with the given release.</summary>
  /// <param name="mbid">The MBID for the release whose recordings should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRecording>> BrowseReleaseRecordingsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                       Include inc = Include.None,
                                                                       CancellationToken cancellationToken = default)
    => new BrowseRecordings(this, Query.BuildExtraText(inc, "release", mbid), limit, offset).NextAsync(cancellationToken);

}
