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

  /// <summary>Returns the labels associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose labels should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested labels.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ILabel> BrowseAllAreaLabels(Guid mbid, int? pageSize = null, int? offset = null,
                                                            Include inc = Include.None)
    => new BrowseLabels(this, Query.BuildExtraText(inc, "area", mbid), pageSize, offset).AsStream();

  /// <summary>Returns the labels in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained labels should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested labels.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ILabel> BrowseAllCollectionLabels(Guid mbid, int? pageSize = null, int? offset = null,
                                                                  Include inc = Include.None)
    => new BrowseLabels(this, Query.BuildExtraText(inc, "collection", mbid), pageSize, offset).AsStream();

  /// <summary>Returns the labels associated with the given area.</summary>
  /// <param name="area">The area whose labels should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested labels.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ILabel> BrowseAllLabels(IArea area, int? pageSize = null, int? offset = null,
                                                        Include inc = Include.None)
    => new BrowseLabels(this, Query.BuildExtraText(inc, "area", area.Id), pageSize, offset).AsStream();

  /// <summary>Returns the labels in the given collection.</summary>
  /// <param name="collection">The collection whose contained labels should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested labels.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ILabel> BrowseAllLabels(ICollection collection, int? pageSize = null, int? offset = null,
                                                        Include inc = Include.None)
    => new BrowseLabels(this, Query.BuildExtraText(inc, "collection", collection.Id), pageSize, offset).AsStream();

  /// <summary>Returns the labels associated with the given release.</summary>
  /// <param name="release">The release whose labels should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested labels.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ILabel> BrowseAllLabels(IRelease release, int? pageSize = null, int? offset = null,
                                                        Include inc = Include.None)
    => new BrowseLabels(this, Query.BuildExtraText(inc, "release", release.Id), pageSize, offset).AsStream();

  /// <summary>Returns the labels associated with the given release.</summary>
  /// <param name="mbid">The MBID for the release whose labels should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>
  /// The requested labels.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ILabel> BrowseAllReleaseLabels(Guid mbid, int? pageSize = null, int? offset = null,
                                                               Include inc = Include.None)
    => new BrowseLabels(this, Query.BuildExtraText(inc, "release", mbid), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the labels associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ILabel> BrowseAreaLabels(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => AsyncUtils.ResultOf(this.BrowseAreaLabelsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the labels associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ILabel>> BrowseAreaLabelsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                            Include inc = Include.None,
                                                            CancellationToken cancellationToken = default)
    => new BrowseLabels(this, Query.BuildExtraText(inc, "area", mbid), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the labels in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ILabel> BrowseCollectionLabels(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => AsyncUtils.ResultOf(this.BrowseCollectionLabelsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the labels in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ILabel>> BrowseCollectionLabelsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                  Include inc = Include.None,
                                                                  CancellationToken cancellationToken = default)
    => new BrowseLabels(this, Query.BuildExtraText(inc, "collection", mbid), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the labels associated with the given area.</summary>
  /// <param name="area">The area whose labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ILabel> BrowseLabels(IArea area, int? limit = null, int? offset = null, Include inc = Include.None)
    => AsyncUtils.ResultOf(this.BrowseLabelsAsync(area, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the labels in the given collection.</summary>
  /// <param name="collection">The collection whose contained labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ILabel> BrowseLabels(ICollection collection, int? limit = null, int? offset = null,
                                             Include inc = Include.None)
    => AsyncUtils.ResultOf(this.BrowseLabelsAsync(collection, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the labels associated with the given release.</summary>
  /// <param name="release">The release whose labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ILabel> BrowseLabels(IRelease release, int? limit = null, int? offset = null, Include inc = Include.None)
    => AsyncUtils.ResultOf(this.BrowseLabelsAsync(release, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the labels associated with the given area.</summary>
  /// <param name="area">The area whose labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ILabel>> BrowseLabelsAsync(IArea area, int? limit = null, int? offset = null,
                                                        Include inc = Include.None, CancellationToken cancellationToken = default)
    => new BrowseLabels(this, Query.BuildExtraText(inc, "area", area.Id), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the labels in the given collection.</summary>
  /// <param name="collection">The collection whose contained labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ILabel>> BrowseLabelsAsync(ICollection collection, int? limit = null, int? offset = null,
                                                        Include inc = Include.None, CancellationToken cancellationToken = default)
    => new BrowseLabels(this, Query.BuildExtraText(inc, "collection", collection.Id), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the labels associated with the given release.</summary>
  /// <param name="release">The release whose labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ILabel>> BrowseLabelsAsync(IRelease release, int? limit = null, int? offset = null,
                                                        Include inc = Include.None, CancellationToken cancellationToken = default)
    => new BrowseLabels(this, Query.BuildExtraText(inc, "release", release.Id), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the labels associated with the given release.</summary>
  /// <param name="mbid">The MBID for the release whose labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ILabel> BrowseReleaseLabels(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => AsyncUtils.ResultOf(this.BrowseReleaseLabelsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the labels associated with the given release.</summary>
  /// <param name="mbid">The MBID for the release whose labels should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ILabel>> BrowseReleaseLabelsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                               Include inc = Include.None,
                                                               CancellationToken cancellationToken = default)
    => new BrowseLabels(this, Query.BuildExtraText(inc, "release", mbid), limit, offset).NextAsync(cancellationToken);

}
