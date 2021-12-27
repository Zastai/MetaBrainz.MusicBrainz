using System;
using System.Net;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Browses;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Returns (the specified subset of) the collections that include the given area.</summary>
  /// <param name="mbid">The MBID for the area whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseAreaCollections(Guid mbid, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseAreaCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given area.</summary>
  /// <param name="mbid">The MBID for the area whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseAreaCollectionsAsync(Guid mbid, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?area={mbid:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseArtistCollections(Guid mbid, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseArtistCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseArtistCollectionsAsync(Guid mbid, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?artist={mbid:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given area.</summary>
  /// <param name="area">The area whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(IArea area, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseCollectionsAsync(area, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given artist.</summary>
  /// <param name="artist">The artist whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(IArtist artist, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseCollectionsAsync(artist, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given event.</summary>
  /// <param name="event">The event whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(IEvent @event, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseCollectionsAsync(@event, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given instrument.</summary>
  /// <param name="instrument">The instrument whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(IInstrument instrument, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseCollectionsAsync(instrument, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given label.</summary>
  /// <param name="label">The label whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(ILabel label, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseCollectionsAsync(label, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given place.</summary>
  /// <param name="place">The place whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(IPlace place, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseCollectionsAsync(place, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given recording.</summary>
  /// <param name="recording">The recording whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(IRecording recording, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseCollectionsAsync(recording, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given release.</summary>
  /// <param name="release">The release whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(IRelease release, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseCollectionsAsync(release, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given release group.</summary>
  /// <param name="releaseGroup">The release group whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(IReleaseGroup releaseGroup, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseCollectionsAsync(releaseGroup, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given series.</summary>
  /// <param name="series">The series whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(ISeries series, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseCollectionsAsync(series, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given work.</summary>
  /// <param name="work">The work whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseCollections(IWork work, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseCollectionsAsync(work, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given area.</summary>
  /// <param name="area">The area whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(IArea area, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?area={area.Id:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given artist.</summary>
  /// <param name="artist">The artist whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(IArtist artist, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?artist={artist.Id:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given event.</summary>
  /// <param name="event">The event whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(IEvent @event, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?event={@event.Id:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given instrument.</summary>
  /// <param name="instrument">The instrument whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(IInstrument instrument, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?instrument={instrument.Id:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given label.</summary>
  /// <param name="label">The label whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(ILabel label, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?label={label.Id:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given place.</summary>
  /// <param name="place">The place whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(IPlace place, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?place={place.Id:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given recording.</summary>
  /// <param name="recording">The recording whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(IRecording recording, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?recording={recording.Id:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given release.</summary>
  /// <param name="release">The release whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(IRelease release, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?release={release.Id:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given release group.</summary>
  /// <param name="releaseGroup">The release group whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(IReleaseGroup releaseGroup, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?release-group={releaseGroup.Id:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given series.</summary>
  /// <param name="series">The series whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(ISeries series, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?series={series.Id:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given work.</summary>
  /// <param name="work">The work whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseCollectionsAsync(IWork work, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?work={work.Id:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections of the given editor.</summary>
  /// <param name="editor">The editor whose collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseEditorCollections(string editor, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseEditorCollectionsAsync(editor, limit, offset));

  /// <summary>Returns (the specified subset of) the collections of the given editor.</summary>
  /// <param name="editor">The editor whose collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseEditorCollectionsAsync(string editor, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?editor={editor}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given event.</summary>
  /// <param name="mbid">The MBID for the event whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseEventCollections(Guid mbid, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseEventCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given event.</summary>
  /// <param name="mbid">The MBID for the event whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseEventCollectionsAsync(Guid mbid, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?event={mbid:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given instrument.</summary>
  /// <param name="mbid">The MBID for the instrument whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseInstrumentCollections(Guid mbid, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseInstrumentCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given instrument.</summary>
  /// <param name="mbid">The MBID for the instrument whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseInstrumentCollectionsAsync(Guid mbid, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?instrument={mbid:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given label.</summary>
  /// <param name="mbid">The MBID for the label whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseLabelCollections(Guid mbid, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseLabelCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given label.</summary>
  /// <param name="mbid">The MBID for the label whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseLabelCollectionsAsync(Guid mbid, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?label={mbid:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given place.</summary>
  /// <param name="mbid">The MBID for the place whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowsePlaceCollections(Guid mbid, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowsePlaceCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given place.</summary>
  /// <param name="mbid">The MBID for the place whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowsePlaceCollectionsAsync(Guid mbid, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?place={mbid:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given recording.</summary>
  /// <param name="mbid">The MBID for the recording whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseRecordingCollections(Guid mbid, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseRecordingCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given recording.</summary>
  /// <param name="mbid">The MBID for the recording whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseRecordingCollectionsAsync(Guid mbid, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?recording={mbid:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given release.</summary>
  /// <param name="mbid">The MBID for the release whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseReleaseCollections(Guid mbid, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseReleaseCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given release.</summary>
  /// <param name="mbid">The MBID for the release whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseReleaseCollectionsAsync(Guid mbid, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?release={mbid:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given release group.</summary>
  /// <param name="mbid">The MBID for the release group whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseReleaseGroupCollections(Guid mbid, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseReleaseGroupCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given release group.</summary>
  /// <param name="mbid">The MBID for the release group whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseReleaseGroupCollectionsAsync(Guid mbid, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?release-group={mbid:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given series.</summary>
  /// <param name="mbid">The MBID for the series whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseSeriesCollections(Guid mbid, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseSeriesCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given series.</summary>
  /// <param name="mbid">The MBID for the series whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseSeriesCollectionsAsync(Guid mbid, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?series={mbid:D}", limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the collections that include the given work.</summary>
  /// <param name="mbid">The MBID for the work whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<ICollection> BrowseWorkCollections(Guid mbid, int? limit = null, int? offset = null)
    => Utils.ResultOf(this.BrowseWorkCollectionsAsync(mbid, limit, offset));

  /// <summary>Returns (the specified subset of) the collections that include the given work.</summary>
  /// <param name="mbid">The MBID for the work whose containing collections should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <returns>An asynchronous operation returning the browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<ICollection>> BrowseWorkCollectionsAsync(Guid mbid, int? limit = null, int? offset = null)
    => new BrowseCollections(this, $"?work={mbid:D}", limit, offset).NextAsync();

}
