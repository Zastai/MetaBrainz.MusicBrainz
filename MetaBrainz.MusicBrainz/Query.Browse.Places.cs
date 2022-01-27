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

  /// <summary>Returns the places associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose places should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested places.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IPlace> BrowseAllAreaPlaces(Guid mbid, int? pageSize = null, int? offset = null,
                                                            Include inc = Include.None)
    => new BrowsePlaces(this, Query.BuildExtraText(inc, "area", mbid), pageSize, offset).AsStream();

  /// <summary>Returns the places in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained places should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested places.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IPlace> BrowseAllCollectionPlaces(Guid mbid, int? pageSize = null, int? offset = null,
                                                                  Include inc = Include.None)
    => new BrowsePlaces(this, Query.BuildExtraText(inc, "collection", mbid), pageSize, offset).AsStream();

  /// <summary>Returns the places associated with the given area.</summary>
  /// <param name="area">The area whose places should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested places.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IPlace> BrowseAllPlaces(IArea area, int? pageSize = null, int? offset = null,
                                                        Include inc = Include.None)
    => new BrowsePlaces(this, Query.BuildExtraText(inc, "area", area.Id), pageSize, offset).AsStream();

  /// <summary>Returns the places in the given collection.</summary>
  /// <param name="collection">The collection whose contained places should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested places.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IStreamingQueryResults<IPlace> BrowseAllPlaces(ICollection collection, int? pageSize = null, int? offset = null,
                                                        Include inc = Include.None)
    => new BrowsePlaces(this, Query.BuildExtraText(inc, "collection", collection.Id), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the places associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose places should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IPlace> BrowseAreaPlaces(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseAreaPlacesAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the places associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose places should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IPlace>> BrowseAreaPlacesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                            Include inc = Include.None,
                                                            CancellationToken cancellationToken = default)
    => new BrowsePlaces(this, Query.BuildExtraText(inc, "area", mbid), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the places in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained places should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IPlace> BrowseCollectionPlaces(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseCollectionPlacesAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the places in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained places should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IPlace>> BrowseCollectionPlacesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                  Include inc = Include.None,
                                                                  CancellationToken cancellationToken = default)
    => new BrowsePlaces(this, Query.BuildExtraText(inc, "collection", mbid), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the places associated with the given area.</summary>
  /// <param name="area">The area whose places should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IPlace> BrowsePlaces(IArea area, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowsePlacesAsync(area, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the places in the given collection.</summary>
  /// <param name="collection">The collection whose contained places should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IPlace> BrowsePlaces(ICollection collection, int? limit = null, int? offset = null,
                                             Include inc = Include.None)
    => Utils.ResultOf(this.BrowsePlacesAsync(collection, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the places associated with the given area.</summary>
  /// <param name="area">The area whose places should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IPlace>> BrowsePlacesAsync(IArea area, int? limit = null, int? offset = null,
                                                        Include inc = Include.None, CancellationToken cancellationToken = default)
    => new BrowsePlaces(this, Query.BuildExtraText(inc, "area", area.Id), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the places in the given collection.</summary>
  /// <param name="collection">The collection whose contained places should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IPlace>> BrowsePlacesAsync(ICollection collection, int? limit = null, int? offset = null,
                                                        Include inc = Include.None, CancellationToken cancellationToken = default)
    => new BrowsePlaces(this, Query.BuildExtraText(inc, "collection", collection.Id), limit, offset).NextAsync(cancellationToken);

}
