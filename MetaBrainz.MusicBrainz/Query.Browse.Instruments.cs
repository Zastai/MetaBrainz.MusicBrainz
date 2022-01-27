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

  /// <summary>Returns the instruments in the given collection.</summary>
  /// <param name="collection">The collection whose contained instruments should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested instruments.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IInstrument> BrowseAllInstruments(ICollection collection, int? pageSize = null, int? offset = null,
                                                                  Include inc = Include.None)
    => new BrowseInstruments(this, Query.BuildExtraText(inc, "collection", collection.Id), pageSize, offset).AsStream();

  /// <summary>Returns the instruments in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained instruments should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested instruments.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IInstrument> BrowseAllCollectionInstruments(Guid mbid, int? pageSize = null, int? offset = null,
                                                                            Include inc = Include.None)
    => new BrowseInstruments(this, Query.BuildExtraText(inc, "collection", mbid), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the instruments in the given collection.</summary>
  /// <param name="collection">The collection whose contained instruments should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IInstrument> BrowseInstruments(ICollection collection, int? limit = null, int? offset = null,
                                                       Include inc = Include.None)
    => AsyncUtils.ResultOf(this.BrowseInstrumentsAsync(collection, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the instruments in the given collection.</summary>
  /// <param name="collection">The collection whose contained instruments should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IInstrument>> BrowseInstrumentsAsync(ICollection collection, int? limit = null, int? offset = null,
                                                                  Include inc = Include.None,
                                                                  CancellationToken cancellationToken = default) {
    var browse = new BrowseInstruments(this, Query.BuildExtraText(inc, "collection", collection.Id), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the instruments in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained instruments should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IInstrument> BrowseCollectionInstruments(Guid mbid, int? limit = null, int? offset = null,
                                                                 Include inc = Include.None)
    => AsyncUtils.ResultOf(this.BrowseCollectionInstrumentsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the instruments in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained instruments should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IInstrument>> BrowseCollectionInstrumentsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                            Include inc = Include.None,
                                                                            CancellationToken cancellationToken = default)
    => new BrowseInstruments(this, Query.BuildExtraText(inc, "collection", mbid), limit, offset).NextAsync(cancellationToken);

}
