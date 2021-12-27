using System;
using System.Net;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Browses;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Returns (the specified subset of) the events associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IEvent> BrowseAreaEvents(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseAreaEventsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the events associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IEvent>> BrowseAreaEventsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                            Include inc = Include.None)
    => new BrowseEvents(this, Query.BuildExtraText(inc, $"area={mbid:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the events associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IEvent> BrowseArtistEvents(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseArtistEventsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the events associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IEvent>> BrowseArtistEventsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                              Include inc = Include.None)
    => new BrowseEvents(this, Query.BuildExtraText(inc, $"artist={mbid:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the events in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IEvent> BrowseCollectionEvents(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseCollectionEventsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the events in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IEvent>> BrowseCollectionEventsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                  Include inc = Include.None)
    => new BrowseEvents(this, Query.BuildExtraText(inc, $"collection={mbid:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the events associated with the given area.</summary>
  /// <param name="area">The area whose events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IEvent> BrowseEvents(IArea area, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseEventsAsync(area, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the events associated with the given artist.</summary>
  /// <param name="artist">The artist whose events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IEvent> BrowseEvents(IArtist artist, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseEventsAsync(artist, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the events in the given collection.</summary>
  /// <param name="collection">The collection whose contained events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IEvent> BrowseEvents(ICollection collection, int? limit = null, int? offset = null,
                                             Include inc = Include.None)
    => Utils.ResultOf(this.BrowseEventsAsync(collection, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the events associated with the given place.</summary>
  /// <param name="place">The place whose events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IEvent> BrowseEvents(IPlace place, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseEventsAsync(place, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the events associated with the given area.</summary>
  /// <param name="area">The area whose events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IEvent>> BrowseEventsAsync(IArea area, int? limit = null, int? offset = null,
                                                        Include inc = Include.None)
    => new BrowseEvents(this, Query.BuildExtraText(inc, $"area={area.Id:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the events associated with the given artist.</summary>
  /// <param name="artist">The artist whose events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IEvent>> BrowseEventsAsync(IArtist artist, int? limit = null, int? offset = null,
                                                        Include inc = Include.None)
    => new BrowseEvents(this, Query.BuildExtraText(inc, $"artist={artist.Id:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the events in the given collection.</summary>
  /// <param name="collection">The collection whose contained events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IEvent>> BrowseEventsAsync(ICollection collection, int? limit = null, int? offset = null,
                                                        Include inc = Include.None)
    => new BrowseEvents(this, Query.BuildExtraText(inc, $"collection={collection.Id:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the events associated with the given place.</summary>
  /// <param name="place">The place whose events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IEvent>> BrowseEventsAsync(IPlace place, int? limit = null, int? offset = null,
                                                        Include inc = Include.None)
    => new BrowseEvents(this, Query.BuildExtraText(inc, $"place={place.Id:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the events associated with the given place.</summary>
  /// <param name="mbid">The MBID for the place whose events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IEvent> BrowsePlaceEvents(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowsePlaceEventsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the events associated with the given place.</summary>
  /// <param name="mbid">The MBID for the place whose events should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IEvent>> BrowsePlaceEventsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                             Include inc = Include.None)
    => new BrowseEvents(this, Query.BuildExtraText(inc, $"place={mbid:D}"), limit, offset).NextAsync();

}
