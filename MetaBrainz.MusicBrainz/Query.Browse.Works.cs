using System;
using System.Net;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Browses;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Returns (the specified subset of) the works associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose works should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IWork> BrowseArtistWorks(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseArtistWorksAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the works associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose works should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IWork>> BrowseArtistWorksAsync(Guid mbid, int? limit = null, int? offset = null,
                                                            Include inc = Include.None)
    => new BrowseWorks(this, Query.BuildExtraText(inc, $"artist={mbid:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the works in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained works should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IWork> BrowseCollectionWorks(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseCollectionWorksAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the works in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained works should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IWork>> BrowseCollectionWorksAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                Include inc = Include.None)
    => new BrowseWorks(this, Query.BuildExtraText(inc, $"collection={mbid:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the works associated with the given artist.</summary>
  /// <param name="artist">The artist whose works should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IWork> BrowseWorks(IArtist artist, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseWorksAsync(artist, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the works in the given collection.</summary>
  /// <param name="collection">The collection whose contained works should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IWork> BrowseWorks(ICollection collection, int? limit = null, int? offset = null,
                                           Include inc = Include.None)
    => Utils.ResultOf(this.BrowseWorksAsync(collection, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the works associated with the given artist.</summary>
  /// <param name="artist">The artist whose works should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IWork>> BrowseWorksAsync(IArtist artist, int? limit = null, int? offset = null,
                                                      Include inc = Include.None)
    => new BrowseWorks(this, Query.BuildExtraText(inc, $"artist={artist.Id:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the works in the given collection.</summary>
  /// <param name="collection">The collection whose contained works should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IWork>> BrowseWorksAsync(ICollection collection, int? limit = null, int? offset = null,
                                                      Include inc = Include.None)
    => new BrowseWorks(this, Query.BuildExtraText(inc, $"collection={collection.Id:D}"), limit, offset).NextAsync();

}
