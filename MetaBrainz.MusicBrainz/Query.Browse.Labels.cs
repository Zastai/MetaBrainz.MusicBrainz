using System;
using System.Net;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Browses;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Returns (the specified subset of) the labels associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ILabel> BrowseAreaLabels(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseAreaLabelsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the labels associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ILabel>> BrowseAreaLabelsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                            Include inc = Include.None)
    => new BrowseLabels(this, Query.BuildExtraText(inc, $"area={mbid:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the labels in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ILabel> BrowseCollectionLabels(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseCollectionLabelsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the labels in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ILabel>> BrowseCollectionLabelsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                  Include inc = Include.None)
    => new BrowseLabels(this, Query.BuildExtraText(inc, $"collection={mbid:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the labels associated with the given area.</summary>
  /// <param name="area">The area whose labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ILabel> BrowseLabels(IArea area, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseLabelsAsync(area, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the labels in the given collection.</summary>
  /// <param name="collection">The collection whose contained labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ILabel> BrowseLabels(ICollection collection, int? limit = null, int? offset = null,
                                             Include inc = Include.None)
    => Utils.ResultOf(this.BrowseLabelsAsync(collection, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the labels associated with the given release.</summary>
  /// <param name="release">The release whose labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ILabel> BrowseLabels(IRelease release, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseLabelsAsync(release, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the labels associated with the given area.</summary>
  /// <param name="area">The area whose labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ILabel>> BrowseLabelsAsync(IArea area, int? limit = null, int? offset = null,
                                                        Include inc = Include.None)
    => new BrowseLabels(this, Query.BuildExtraText(inc, $"area={area.Id:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the labels in the given collection.</summary>
  /// <param name="collection">The collection whose contained labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ILabel>> BrowseLabelsAsync(ICollection collection, int? limit = null, int? offset = null,
                                                        Include inc = Include.None)
    => new BrowseLabels(this, Query.BuildExtraText(inc, $"collection={collection.Id:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the labels associated with the given release.</summary>
  /// <param name="release">The release whose labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ILabel>> BrowseLabelsAsync(IRelease release, int? limit = null, int? offset = null,
                                                        Include inc = Include.None)
    => new BrowseLabels(this, Query.BuildExtraText(inc, $"release={release.Id:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the labels associated with the given release.</summary>
  /// <param name="mbid">The MBID for the release whose labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ILabel> BrowseReleaseLabels(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseReleaseLabelsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the labels associated with the given release.</summary>
  /// <param name="mbid">The MBID for the release whose labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ILabel>> BrowseReleaseLabelsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                               Include inc = Include.None)
    => new BrowseLabels(this, Query.BuildExtraText(inc, $"release={mbid:D}"), limit, offset).NextAsync();

}
