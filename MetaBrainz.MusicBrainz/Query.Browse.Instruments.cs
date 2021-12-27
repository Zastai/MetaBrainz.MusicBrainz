using System;
using System.Net;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Browses;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

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
    => Utils.ResultOf(this.BrowseInstrumentsAsync(collection, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the instruments in the given collection.</summary>
  /// <param name="collection">The collection whose contained instruments should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IInstrument>> BrowseInstrumentsAsync(ICollection collection, int? limit = null, int? offset = null,
                                                                  Include inc = Include.None)
    => new BrowseInstruments(this, Query.BuildExtraText(inc, $"collection={collection.Id:D}"), limit, offset).NextAsync();

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
    => Utils.ResultOf(this.BrowseCollectionInstrumentsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the instruments in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained instruments should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IInstrument>> BrowseCollectionInstrumentsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                            Include inc = Include.None)
    => new BrowseInstruments(this, Query.BuildExtraText(inc, $"collection={mbid:D}"), limit, offset).NextAsync();

}
