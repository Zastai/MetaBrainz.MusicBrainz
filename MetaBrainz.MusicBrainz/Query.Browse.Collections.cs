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

  /// <summary>Returns the collections that include the given area.</summary>
  /// <param name="mbid">The MBID for the area whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllAreaCollections(Guid mbid, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("area", mbid), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllArtistCollections(Guid mbid, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("artist", mbid), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given area.</summary>
  /// <param name="area">The area whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllCollections(IArea area, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("area", area.Id), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given artist.</summary>
  /// <param name="artist">The artist whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllCollections(IArtist artist, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("artist", artist.Id), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given event.</summary>
  /// <param name="event">The event whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllCollections(IEvent @event, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("event", @event.Id), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given instrument.</summary>
  /// <param name="instrument">The instrument whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllCollections(IInstrument instrument, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("instrument", instrument.Id), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given label.</summary>
  /// <param name="label">The label whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllCollections(ILabel label, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("label", label.Id), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given place.</summary>
  /// <param name="place">The place whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllCollections(IPlace place, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("place", place.Id), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given recording.</summary>
  /// <param name="recording">The recording whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllCollections(IRecording recording, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("recording", recording.Id), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given release.</summary>
  /// <param name="release">The release whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllCollections(IRelease release, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("release", release.Id), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given release group.</summary>
  /// <param name="releaseGroup">The release group whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllCollections(IReleaseGroup releaseGroup, int? pageSize = null,
                                                                  int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("release-group", releaseGroup.Id), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given series.</summary>
  /// <param name="series">The series whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllCollections(ISeries series, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("series", series.Id), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given work.</summary>
  /// <param name="work">The work whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllCollections(IWork work, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("work", work.Id), pageSize, offset).AsStream();

  /// <summary>Returns the collections of the given editor.</summary>
  /// <param name="editor">The editor whose collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllEditorCollections(string editor, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("editor", editor), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given event.</summary>
  /// <param name="mbid">The MBID for the event whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllEventCollections(Guid mbid, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("event", mbid), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given instrument.</summary>
  /// <param name="mbid">The MBID for the instrument whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllInstrumentCollections(Guid mbid, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("instrument", mbid), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given label.</summary>
  /// <param name="mbid">The MBID for the label whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllLabelCollections(Guid mbid, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("label", mbid), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given place.</summary>
  /// <param name="mbid">The MBID for the place whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllPlaceCollections(Guid mbid, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("place", mbid), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given recording.</summary>
  /// <param name="mbid">The MBID for the recording whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllRecordingCollections(Guid mbid, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("recording", mbid), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given release.</summary>
  /// <param name="mbid">The MBID for the release whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllReleaseCollections(Guid mbid, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("release", mbid), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given release group.</summary>
  /// <param name="mbid">The MBID for the release group whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllReleaseGroupCollections(Guid mbid, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("release-group", mbid), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given series.</summary>
  /// <param name="mbid">The MBID for the series whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllSeriesCollections(Guid mbid, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("series", mbid), pageSize, offset).AsStream();

  /// <summary>Returns the collections that include the given work.</summary>
  /// <param name="mbid">The MBID for the work whose containing collections should be retrieved.</param>
  /// <param name="pageSize">The maximum number of results to get in one request (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>
  /// The requested collections.<br/>
  /// Note that this may use multiple "paged" requests to the web service. As such, an item can potentially be returned more than
  /// once: once at the end of a page, then again in the next page, if a new entry was inserted earlier in the sequence. Similarly,
  /// a result may be skipped if an item that was already returned is deleted (but deletions are far less likely).
  /// </returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IStreamingQueryResults<ICollection> BrowseAllWorkCollections(Guid mbid, int? pageSize = null, int? offset = null)
    => new BrowseCollections(this, Query.CreateOptions("work", mbid), pageSize, offset).AsStream();

  /// <summary>Returns (the specified subset of) the collections that include the given area.</summary>
  /// <param name="mbid">The MBID for the area whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseAreaCollections(Guid mbid, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseAreaCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given area.</summary>
  /// <param name="mbid">The MBID for the area whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseAreaCollectionsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                      CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("area", mbid), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseArtistCollections(Guid mbid, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseArtistCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseArtistCollectionsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                        CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("artist", mbid), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given area.</summary>
  /// <param name="area">The area whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(IArea area, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseCollectionsAsync(area, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given artist.</summary>
  /// <param name="artist">The artist whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(IArtist artist, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseCollectionsAsync(artist, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given event.</summary>
  /// <param name="event">The event whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(IEvent @event, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseCollectionsAsync(@event, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given instrument.</summary>
  /// <param name="instrument">The instrument whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(IInstrument instrument, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseCollectionsAsync(instrument, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given label.</summary>
  /// <param name="label">The label whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(ILabel label, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseCollectionsAsync(label, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given place.</summary>
  /// <param name="place">The place whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(IPlace place, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseCollectionsAsync(place, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given recording.</summary>
  /// <param name="recording">The recording whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(IRecording recording, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseCollectionsAsync(recording, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given release.</summary>
  /// <param name="release">The release whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(IRelease release, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseCollectionsAsync(release, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given release group.</summary>
  /// <param name="releaseGroup">The release group whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(IReleaseGroup releaseGroup, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseCollectionsAsync(releaseGroup, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given series.</summary>
  /// <param name="series">The series whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(ISeries series, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseCollectionsAsync(series, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given work.</summary>
  /// <param name="work">The work whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(IWork work, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseCollectionsAsync(work, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given area.</summary>
  /// <param name="area">The area whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(IArea area, int? limit = null, int? offset = null,
                                                                  CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("area", area.Id), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given artist.</summary>
  /// <param name="artist">The artist whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(IArtist artist, int? limit = null, int? offset = null,
                                                                  CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("artist", artist.Id), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given event.</summary>
  /// <param name="event">The event whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(IEvent @event, int? limit = null, int? offset = null,
                                                                  CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("event", @event.Id), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given instrument.</summary>
  /// <param name="instrument">The instrument whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(IInstrument instrument, int? limit = null, int? offset = null,
                                                                  CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("instrument", instrument.Id), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given label.</summary>
  /// <param name="label">The label whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(ILabel label, int? limit = null, int? offset = null,
                                                                  CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("label", label.Id), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given place.</summary>
  /// <param name="place">The place whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(IPlace place, int? limit = null, int? offset = null,
                                                                  CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("place", place.Id), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given recording.</summary>
  /// <param name="recording">The recording whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(IRecording recording, int? limit = null, int? offset = null,
                                                                  CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("recording", recording.Id), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given release.</summary>
  /// <param name="release">The release whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(IRelease release, int? limit = null, int? offset = null,
                                                                  CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("release", release.Id), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given release group.</summary>
  /// <param name="releaseGroup">The release group whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(IReleaseGroup releaseGroup, int? limit = null, int? offset = null,
                                                                  CancellationToken cancellationToken = default) {
    var browse = new BrowseCollections(this, Query.CreateOptions("release-group", releaseGroup.Id), limit, offset);
    return browse.NextAsync(cancellationToken);
  }

  /// <summary>Returns (the specified subset of) the collections that include the given series.</summary>
  /// <param name="series">The series whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(ISeries series, int? limit = null, int? offset = null,
                                                                  CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("series", series.Id), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given work.</summary>
  /// <param name="work">The work whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(IWork work, int? limit = null, int? offset = null,
                                                                  CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("work", work.Id), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections of the given editor.</summary>
  /// <param name="editor">The editor whose collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseEditorCollections(string editor, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseEditorCollectionsAsync(editor, limit, offset));

  /// <summary>Returns (the specified subset of) the collections of the given editor.</summary>
  /// <param name="editor">The editor whose collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseEditorCollectionsAsync(string editor, int? limit = null, int? offset = null,
                                                                        CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("editor", editor), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given event.</summary>
  /// <param name="mbid">The MBID for the event whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseEventCollections(Guid mbid, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseEventCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given event.</summary>
  /// <param name="mbid">The MBID for the event whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseEventCollectionsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                       CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("event", mbid), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given instrument.</summary>
  /// <param name="mbid">The MBID for the instrument whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseInstrumentCollections(Guid mbid, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseInstrumentCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given instrument.</summary>
  /// <param name="mbid">The MBID for the instrument whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseInstrumentCollectionsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                            CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("instrument", mbid), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given label.</summary>
  /// <param name="mbid">The MBID for the label whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseLabelCollections(Guid mbid, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseLabelCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given label.</summary>
  /// <param name="mbid">The MBID for the label whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseLabelCollectionsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                       CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("label", mbid), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given place.</summary>
  /// <param name="mbid">The MBID for the place whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowsePlaceCollections(Guid mbid, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowsePlaceCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given place.</summary>
  /// <param name="mbid">The MBID for the place whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowsePlaceCollectionsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                       CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("place", mbid), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given recording.</summary>
  /// <param name="mbid">The MBID for the recording whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseRecordingCollections(Guid mbid, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseRecordingCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given recording.</summary>
  /// <param name="mbid">The MBID for the recording whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseRecordingCollectionsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                           CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("recording", mbid), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given release.</summary>
  /// <param name="mbid">The MBID for the release whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseReleaseCollections(Guid mbid, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseReleaseCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given release.</summary>
  /// <param name="mbid">The MBID for the release whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseReleaseCollectionsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                         CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("release", mbid), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given release group.</summary>
  /// <param name="mbid">The MBID for the release group whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseReleaseGroupCollections(Guid mbid, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseReleaseGroupCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given release group.</summary>
  /// <param name="mbid">The MBID for the release group whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseReleaseGroupCollectionsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                              CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("release-group", mbid), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given series.</summary>
  /// <param name="mbid">The MBID for the series whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseSeriesCollections(Guid mbid, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseSeriesCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given series.</summary>
  /// <param name="mbid">The MBID for the series whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseSeriesCollectionsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                        CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("series", mbid), limit, offset).NextAsync(cancellationToken);

  /// <summary>Returns (the specified subset of) the collections that include the given work.</summary>
  /// <param name="mbid">The MBID for the work whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public IBrowseResults<ICollection> BrowseWorkCollections(Guid mbid, int? limit = null, int? offset = null)
    => AsyncUtils.ResultOf(this.BrowseWorkCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given work.</summary>
  /// <param name="mbid">The MBID for the work whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseWorkCollectionsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                      CancellationToken cancellationToken = default)
    => new BrowseCollections(this, Query.CreateOptions("work", mbid), limit, offset).NextAsync(cancellationToken);

}
