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

  /// <summary>Returns the events associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose events should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested events.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IEvent> BrowseAllAreaEvents(Guid mbid, int? pageSize = null, int? offset = null,
                                                            Include inc = Include.None)
    => new BrowseEvents(this, Query.CreateOptions("area", mbid, inc), pageSize, offset).AsStream();

  /// <summary>Returns the events associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose events should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested events.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IEvent> BrowseAllArtistEvents(Guid mbid, int? pageSize = null, int? offset = null,
                                                              Include inc = Include.None)
    => new BrowseEvents(this, Query.CreateOptions("artist", mbid, inc), pageSize, offset).AsStream();

  /// <summary>Returns the events in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose events should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested events.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IEvent> BrowseAllCollectionEvents(Guid mbid, int? pageSize = null, int? offset = null,
                                                                  Include inc = Include.None)
    => new BrowseEvents(this, Query.CreateOptions("collection", mbid, inc), pageSize, offset).AsStream();

  /// <summary>Returns the events associated with the given area.</summary>
  /// <param name="area">The area whose events should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested events.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IEvent> BrowseAllEvents(IArea area, int? pageSize = null, int? offset = null,
                                                        Include inc = Include.None)
    => new BrowseEvents(this, Query.CreateOptions("area", area.Id, inc), pageSize, offset).AsStream();

  /// <summary>Returns the events associated with the given artist.</summary>
  /// <param name="artist">The artist whose events should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested events.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IEvent> BrowseAllEvents(IArtist artist, int? pageSize = null, int? offset = null,
                                                        Include inc = Include.None)
    => new BrowseEvents(this, Query.CreateOptions("artist", artist.Id, inc), pageSize, offset).AsStream();

  /// <summary>Returns the events in the given collection.</summary>
  /// <param name="collection">The collection whose contained events should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested events.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IEvent> BrowseAllEvents(ICollection collection, int? pageSize = null, int? offset = null,
                                                        Include inc = Include.None)
    => new BrowseEvents(this, Query.CreateOptions("collection", collection.Id, inc), pageSize, offset).AsStream();

  /// <summary>Returns the events associated with the given place.</summary>
  /// <param name="place">The place whose events should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested events.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IEvent> BrowseAllEvents(IPlace place, int? pageSize = null, int? offset = null,
                                                        Include inc = Include.None)
    => new BrowseEvents(this, Query.CreateOptions("place", place.Id, inc), pageSize, offset).AsStream();

  /// <summary>Returns the events associated with the given place.</summary>
  /// <param name="mbid">The MBID for the place whose events should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested events.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<IEvent> BrowseAllPlaceEvents(Guid mbid, int? pageSize = null, int? offset = null,
                                                             Include inc = Include.None)
    => new BrowseEvents(this, Query.CreateOptions("place", mbid, inc), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the events associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IEvent>> BrowseAreaEventsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                            Include inc = Include.None,
                                                            CancellationToken cancellationToken = default)
    => new BrowseEvents(this, Query.CreateOptions("area", mbid, inc), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the events associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IEvent>> BrowseArtistEventsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                              Include inc = Include.None,
                                                              CancellationToken cancellationToken = default)
    => new BrowseEvents(this, Query.CreateOptions("artist", mbid, inc), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the events in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IEvent>> BrowseCollectionEventsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                  Include inc = Include.None,
                                                                  CancellationToken cancellationToken = default)
    => new BrowseEvents(this, Query.CreateOptions("collection", mbid, inc), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the events associated with the given area.</summary>
  /// <param name="area">The area whose events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IEvent>> BrowseEventsAsync(IArea area, int? limit = null, int? offset = null,
                                                        Include inc = Include.None, CancellationToken cancellationToken = default)
    => new BrowseEvents(this, Query.CreateOptions("area", area.Id, inc), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the events associated with the given artist.</summary>
  /// <param name="artist">The artist whose events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IEvent>> BrowseEventsAsync(IArtist artist, int? limit = null, int? offset = null,
                                                        Include inc = Include.None, CancellationToken cancellationToken = default)
    => new BrowseEvents(this, Query.CreateOptions("artist", artist.Id, inc), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the events in the given collection.</summary>
  /// <param name="collection">The collection whose contained events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IEvent>> BrowseEventsAsync(ICollection collection, int? limit = null, int? offset = null,
                                                        Include inc = Include.None, CancellationToken cancellationToken = default)
    => new BrowseEvents(this, Query.CreateOptions("collection", collection.Id, inc), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the events associated with the given place.</summary>
  /// <param name="place">The place whose events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IEvent>> BrowseEventsAsync(IPlace place, int? limit = null, int? offset = null,
                                                        Include inc = Include.None, CancellationToken cancellationToken = default)
    => new BrowseEvents(this, Query.CreateOptions("place", place.Id, inc), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the events associated with the given place.</summary>
  /// <param name="mbid">The MBID for the place whose events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<IEvent>> BrowsePlaceEventsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                             Include inc = Include.None,
                                                             CancellationToken cancellationToken = default)
    => new BrowseEvents(this, Query.CreateOptions("place", mbid, inc), limit, offset).NextAsync(cancellationToken);

}
