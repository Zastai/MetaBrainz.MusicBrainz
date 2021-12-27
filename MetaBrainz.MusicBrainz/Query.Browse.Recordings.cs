using System;
using System.Net;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Browses;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <inheritdoc cref="BrowseArtistRecordingsAsync"/>
  public IBrowseResults<IRecording> BrowseArtistRecordings(Guid mbid, int? limit = null, int? offset = null,
                                                           Include inc = Include.None)
    => Utils.ResultOf(this.BrowseArtistRecordingsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the recordings associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose recordings should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRecording>> BrowseArtistRecordingsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                      Include inc = Include.None)
    => new BrowseRecordings(this, Query.BuildExtraText(inc, $"artist={mbid:D}"), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseCollectionRecordingsAsync"/>
  public IBrowseResults<IRecording> BrowseCollectionRecordings(Guid mbid, int? limit = null, int? offset = null,
                                                               Include inc = Include.None)
    => Utils.ResultOf(this.BrowseCollectionRecordingsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the recordings in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained recordings should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRecording>> BrowseCollectionRecordingsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                          Include inc = Include.None)
    => new BrowseRecordings(this, Query.BuildExtraText(inc, $"collection={mbid:D}"), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseRecordingsAsync(IArtist,int?,int?,Include)"/>
  public IBrowseResults<IRecording> BrowseRecordings(IArtist artist, int? limit = null, int? offset = null,
                                                     Include inc = Include.None)
    => Utils.ResultOf(this.BrowseRecordingsAsync(artist, limit, offset, inc));

  /// <inheritdoc cref="BrowseRecordingsAsync(ICollection,int?,int?,Include)"/>
  public IBrowseResults<IRecording> BrowseRecordings(ICollection collection, int? limit = null, int? offset = null,
                                                     Include inc = Include.None)
    => Utils.ResultOf(this.BrowseRecordingsAsync(collection, limit, offset, inc));

  /// <inheritdoc cref="BrowseRecordingsAsync(IRelease,int?,int?,Include)"/>
  public IBrowseResults<IRecording> BrowseRecordings(IRelease release, int? limit = null, int? offset = null,
                                                     Include inc = Include.None)
    => Utils.ResultOf(this.BrowseRecordingsAsync(release, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the recordings associated with the given artist.</summary>
  /// <param name="artist">The artist whose recordings should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRecording>> BrowseRecordingsAsync(IArtist artist, int? limit = null, int? offset = null,
                                                                Include inc = Include.None)
    => new BrowseRecordings(this, Query.BuildExtraText(inc, $"artist={artist.Id:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the recordings in the given collection.</summary>
  /// <param name="collection">The collection whose contained recordings should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRecording>> BrowseRecordingsAsync(ICollection collection, int? limit = null, int? offset = null,
                                                                Include inc = Include.None)
    => new BrowseRecordings(this, Query.BuildExtraText(inc, $"collection={collection.Id:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the recordings associated with the given release.</summary>
  /// <param name="release">The release whose recordings should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRecording>> BrowseRecordingsAsync(IRelease release, int? limit = null, int? offset = null,
                                                                Include inc = Include.None)
    => new BrowseRecordings(this, Query.BuildExtraText(inc, $"release={release.Id:D}"), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseReleaseRecordingsAsync"/>
  public IBrowseResults<IRecording> BrowseReleaseRecordings(Guid mbid, int? limit = null, int? offset = null,
                                                            Include inc = Include.None)
    => Utils.ResultOf(this.BrowseReleaseRecordingsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the recordings associated with the given release.</summary>
  /// <param name="mbid">The MBID for the release whose recordings should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRecording>> BrowseReleaseRecordingsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                       Include inc = Include.None)
    => new BrowseRecordings(this, Query.BuildExtraText(inc, $"release={mbid:D}"), limit, offset).NextAsync();

}
