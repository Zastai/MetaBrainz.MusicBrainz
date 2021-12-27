using System;
using System.Net;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Browses;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Returns (the specified subset of) the artists associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IArtist> BrowseAreaArtists(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseAreaArtistsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the artists associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseAreaArtistsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                              Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, $"area={mbid:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the releases associated with the given area.</summary>
  /// <param name="area">The area whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IArtist> BrowseArtists(IArea area, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseArtistsAsync(area, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the artists in the given collection.</summary>
  /// <param name="collection">The collection whose contained artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IArtist> BrowseArtists(ICollection collection, int? limit = null, int? offset = null,
                                               Include inc = Include.None)
    => Utils.ResultOf(this.BrowseArtistsAsync(collection, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the releases associated with the given recording.</summary>
  /// <param name="recording">The recording whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IArtist> BrowseArtists(IRecording recording, int? limit = null, int? offset = null,
                                               Include inc = Include.None)
    => Utils.ResultOf(this.BrowseArtistsAsync(recording, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the releases associated with the given release.</summary>
  /// <param name="release">The release whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IArtist> BrowseArtists(IRelease release, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseArtistsAsync(release, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the releases associated with the given release group.</summary>
  /// <param name="releaseGroup">The release group whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IArtist> BrowseArtists(IReleaseGroup releaseGroup, int? limit = null, int? offset = null,
                                               Include inc = Include.None)
    => Utils.ResultOf(this.BrowseArtistsAsync(releaseGroup, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the releases associated with the given work.</summary>
  /// <param name="work">The work whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IArtist> BrowseArtists(IWork work, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseArtistsAsync(work, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the releases associated with the given area.</summary>
  /// <param name="area">The area whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseArtistsAsync(IArea area, int? limit = null, int? offset = null,
                                                          Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, $"area={area.Id:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the artists in the given collection.</summary>
  /// <param name="collection">The collection whose contained artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseArtistsAsync(ICollection collection, int? limit = null, int? offset = null,
                                                          Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, $"collection={collection.Id:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the releases associated with the given recording.</summary>
  /// <param name="recording">The recording whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseArtistsAsync(IRecording recording, int? limit = null, int? offset = null,
                                                          Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, $"recording={recording.Id:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the releases associated with the given release.</summary>
  /// <param name="release">The release whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseArtistsAsync(IRelease release, int? limit = null, int? offset = null,
                                                          Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, $"release={release.Id:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the releases associated with the given release group.</summary>
  /// <param name="releaseGroup">The release group whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseArtistsAsync(IReleaseGroup releaseGroup, int? limit = null, int? offset = null,
                                                          Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, $"release-group={releaseGroup.Id:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the releases associated with the given work.</summary>
  /// <param name="work">The work whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseArtistsAsync(IWork work, int? limit = null, int? offset = null,
                                                          Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, $"work={work.Id:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the artists in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IArtist> BrowseCollectionArtists(Guid mbid, int? limit = null, int? offset = null,
                                                         Include inc = Include.None)
    => Utils.ResultOf(this.BrowseCollectionArtistsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the artists in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseCollectionArtistsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                    Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, $"collection={mbid:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the artists associated with the given recording.</summary>
  /// <param name="mbid">The MBID for the recording whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IArtist> BrowseRecordingArtists(Guid mbid, int? limit = null, int? offset = null,
                                                        Include inc = Include.None)
    => Utils.ResultOf(this.BrowseRecordingArtistsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the artists associated with the given recording.</summary>
  /// <param name="mbid">The MBID for the recording whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseRecordingArtistsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                   Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, $"recording={mbid:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the artists associated with the given release.</summary>
  /// <param name="mbid">The MBID for the release whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IArtist> BrowseReleaseArtists(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseReleaseArtistsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the artists associated with the given release.</summary>
  /// <param name="mbid">The MBID for the release whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseReleaseArtistsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                 Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, $"release={mbid:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the artists associated with the given release group.</summary>
  /// <param name="mbid">The MBID for the release group whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IArtist> BrowseReleaseGroupArtists(Guid mbid, int? limit = null, int? offset = null,
                                                           Include inc = Include.None)
    => Utils.ResultOf(this.BrowseReleaseGroupArtistsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the artists associated with the given release group.</summary>
  /// <param name="mbid">The MBID for the release group whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseReleaseGroupArtistsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                      Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, $"release-group={mbid:D}"), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the artists associated with the given work.</summary>
  /// <param name="mbid">The MBID for the work whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public IBrowseResults<IArtist> BrowseWorkArtists(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None)
    => Utils.ResultOf(this.BrowseWorkArtistsAsync(mbid, limit, offset, inc));

  /// <summary>Returns (the specified subset of) the artists associated with the given work.</summary>
  /// <param name="mbid">The MBID for the work whose artists should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IArtist>> BrowseWorkArtistsAsync(Guid mbid, int? limit = null, int? offset = null,
                                                              Include inc = Include.None)
    => new BrowseArtists(this, Query.BuildExtraText(inc, $"work={mbid:D}"), limit, offset).NextAsync();

}
