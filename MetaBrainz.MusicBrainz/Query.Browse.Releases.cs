using System;
using System.Net;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Browses;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <inheritdoc cref="BrowseAreaReleasesAsync"/>
  public IBrowseResults<IRelease> BrowseAreaReleases(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None,
                                                     ReleaseType? type = null, ReleaseStatus? status = null)
    => Utils.ResultOf(this.BrowseAreaReleasesAsync(mbid, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases associated with the given area.</summary>
  /// <param name="mbid">The MBID for the area whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseAreaReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                Include inc = Include.None, ReleaseType? type = null,
                                                                ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, $"area={mbid:D}", type, status), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseArtistReleasesAsync"/>
  public IBrowseResults<IRelease> BrowseArtistReleases(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None,
                                                       ReleaseType? type = null, ReleaseStatus? status = null)
    => Utils.ResultOf(this.BrowseArtistReleasesAsync(mbid, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases associated with the given artist.</summary>
  /// <param name="mbid">The MBID for the artist whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseArtistReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                  Include inc = Include.None, ReleaseType? type = null,
                                                                  ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, $"artist={mbid:D}", type, status), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseCollectionReleasesAsync"/>
  public IBrowseResults<IRelease> BrowseCollectionReleases(Guid mbid, int? limit = null, int? offset = null,
                                                           Include inc = Include.None, ReleaseType? type = null,
                                                           ReleaseStatus? status = null)
    => Utils.ResultOf(this.BrowseCollectionReleasesAsync(mbid, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases in the given collection.</summary>
  /// <param name="mbid">The MBID for the collection whose contained releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseCollectionReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                      Include inc = Include.None, ReleaseType? type = null,
                                                                      ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, $"collection={mbid:D}", type, status), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseLabelReleasesAsync"/>
  public IBrowseResults<IRelease> BrowseLabelReleases(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None,
                                                      ReleaseType? type = null, ReleaseStatus? status = null)
    => Utils.ResultOf(this.BrowseLabelReleasesAsync(mbid, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases associated with the given label.</summary>
  /// <param name="mbid">The MBID for the label whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseLabelReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                 Include inc = Include.None, ReleaseType? type = null,
                                                                 ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, $"label={mbid:D}", type, status), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseRecordingReleasesAsync"/>
  public IBrowseResults<IRelease> BrowseRecordingReleases(Guid mbid, int? limit = null, int? offset = null,
                                                          Include inc = Include.None, ReleaseType? type = null,
                                                          ReleaseStatus? status = null)
    => Utils.ResultOf(this.BrowseRecordingReleasesAsync(mbid, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases associated with the given recording.</summary>
  /// <param name="mbid">The MBID for the recording whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseRecordingReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                     Include inc = Include.None, ReleaseType? type = null,
                                                                     ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, $"recording={mbid:D}", type, status), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseReleaseGroupReleasesAsync"/>
  public IBrowseResults<IRelease> BrowseReleaseGroupReleases(Guid mbid, int? limit = null, int? offset = null,
                                                             Include inc = Include.None, ReleaseType? type = null,
                                                             ReleaseStatus? status = null)
    => Utils.ResultOf(this.BrowseReleaseGroupReleasesAsync(mbid, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases associated with the given release group.</summary>
  /// <param name="mbid">The MBID for the release group whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleaseGroupReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                        Include inc = Include.None, ReleaseType? type = null,
                                                                        ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, $"release-group={mbid:D}", type, status), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseReleasesAsync(IArea,int?,int?,Include,ReleaseType?,ReleaseStatus?)"/>
  public IBrowseResults<IRelease> BrowseReleases(IArea area, int? limit = null, int? offset = null, Include inc = Include.None,
                                                 ReleaseType? type = null, ReleaseStatus? status = null)
    => Utils.ResultOf(this.BrowseReleasesAsync(area, limit, offset, inc, type, status));

  /// <inheritdoc cref="BrowseReleasesAsync(IArtist,int?,int?,Include,ReleaseType?,ReleaseStatus?)"/>
  public IBrowseResults<IRelease> BrowseReleases(IArtist artist, int? limit = null, int? offset = null, Include inc = Include.None,
                                                 ReleaseType? type = null, ReleaseStatus? status = null)
    => Utils.ResultOf(this.BrowseReleasesAsync(artist, limit, offset, inc, type, status));

  /// <inheritdoc cref="BrowseReleasesAsync(ICollection,int?,int?,Include,ReleaseType?,ReleaseStatus?)"/>
  public IBrowseResults<IRelease> BrowseReleases(ICollection collection, int? limit = null, int? offset = null,
                                                 Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null)
    => Utils.ResultOf(this.BrowseReleasesAsync(collection, limit, offset, inc, type, status));

  /// <inheritdoc cref="BrowseReleasesAsync(ILabel,int?,int?,Include,ReleaseType?,ReleaseStatus?)"/>
  public IBrowseResults<IRelease> BrowseReleases(ILabel label, int? limit = null, int? offset = null, Include inc = Include.None,
                                                 ReleaseType? type = null, ReleaseStatus? status = null)
    => Utils.ResultOf(this.BrowseReleasesAsync(label, limit, offset, inc, type, status));

  /// <inheritdoc cref="BrowseReleasesAsync(IRecording,int?,int?,Include,ReleaseType?,ReleaseStatus?)"/>
  public IBrowseResults<IRelease> BrowseReleases(IRecording recording, int? limit = null, int? offset = null,
                                                 Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null)
    => Utils.ResultOf(this.BrowseReleasesAsync(recording, limit, offset, inc, type, status));

  /// <inheritdoc cref="BrowseReleasesAsync(IReleaseGroup,int?,int?,Include,ReleaseType?,ReleaseStatus?)"/>
  public IBrowseResults<IRelease> BrowseReleases(IReleaseGroup releaseGroup, int? limit = null, int? offset = null,
                                                 Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, $"release-group={releaseGroup.Id:D}", type, status), limit, offset)
      .Next();

  /// <inheritdoc cref="BrowseReleasesAsync(ITrack,int?,int?,Include,ReleaseType?,ReleaseStatus?)"/>
  public IBrowseResults<IRelease> BrowseReleases(ITrack track, int? limit = null, int? offset = null, Include inc = Include.None,
                                                 ReleaseType? type = null, ReleaseStatus? status = null)
    => Utils.ResultOf(this.BrowseReleasesAsync(track, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases associated with the given area.</summary>
  /// <param name="area">The area whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(IArea area, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, $"area={area.Id:D}", type, status), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the releases associated with the given artist.</summary>
  /// <param name="artist">The artist whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(IArtist artist, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, $"artist={artist.Id:D}", type, status), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the releases in the given collection.</summary>
  /// <param name="collection">The collection whose contained releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(ICollection collection, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, $"collection={collection.Id:D}", type, status), limit, offset)
      .NextAsync();

  /// <summary>Returns (the specified subset of) the releases associated with the given label.</summary>
  /// <param name="label">The label whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(ILabel label, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, $"label={label.Id:D}", type, status), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the releases associated with the given recording.</summary>
  /// <param name="recording">The recording whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(IRecording recording, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, $"recording={recording.Id:D}", type, status), limit, offset).NextAsync();

  /// <summary>Returns (the specified subset of) the releases associated with the given release group.</summary>
  /// <param name="releaseGroup">The release group whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(IReleaseGroup releaseGroup, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, $"release-group={releaseGroup.Id:D}", type, status), limit, offset)
      .NextAsync();

  /// <summary>Returns (the specified subset of) the releases associated with the given track.</summary>
  /// <param name="track">The track whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseReleasesAsync(ITrack track, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, $"track={track.Id:D}", type, status), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseTrackArtistReleasesAsync(Guid,int?,int?,Include,ReleaseType?,ReleaseStatus?)"/>
  public IBrowseResults<IRelease> BrowseTrackArtistReleases(Guid mbid, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => Utils.ResultOf(this.BrowseTrackArtistReleasesAsync(mbid, limit, offset, inc, type, status));

  /// <inheritdoc cref="BrowseTrackArtistReleasesAsync(IArtist,int?,int?,Include,ReleaseType?,ReleaseStatus?)"/>
  public IBrowseResults<IRelease> BrowseTrackArtistReleases(IArtist artist, int? limit = null, int? offset = null,
                                                            Include inc = Include.None, ReleaseType? type = null,
                                                            ReleaseStatus? status = null)
    => Utils.ResultOf(this.BrowseTrackArtistReleasesAsync(artist, limit, offset, inc, type, status));

  /// <summary>
  /// Returns (the specified subset of) the releases that include the given artist in a track-level artist credit only.
  /// </summary>
  /// <param name="mbid">The MBID for the artist whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseTrackArtistReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                       Include inc = Include.None, ReleaseType? type = null,
                                                                       ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, $"track_artist={mbid:D}", type, status), limit, offset).NextAsync();

  /// <summary>
  /// Returns (the specified subset of) the releases that include the given artist in a track-level artist credit only.
  /// </summary>
  /// <param name="artist">The artist whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseTrackArtistReleasesAsync(IArtist artist, int? limit = null, int? offset = null,
                                                                       Include inc = Include.None, ReleaseType? type = null,
                                                                       ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, $"track_artist={artist.Id:D}", type, status), limit, offset).NextAsync();

  /// <inheritdoc cref="BrowseTrackReleasesAsync"/>
  public IBrowseResults<IRelease> BrowseTrackReleases(Guid mbid, int? limit = null, int? offset = null, Include inc = Include.None,
                                                      ReleaseType? type = null, ReleaseStatus? status = null)
    => Utils.ResultOf(this.BrowseTrackReleasesAsync(mbid, limit, offset, inc, type, status));

  /// <summary>Returns (the specified subset of) the releases associated with the given track.</summary>
  /// <param name="mbid">The MBID for the track whose releases should be retrieved.</param>
  /// <param name="limit">The maximum number of results to return (1-100; default is 25).</param>
  /// <param name="offset">The offset at which to start (i.e. the number of results to skip).</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">The release type to filter on (if any).</param>
  /// <param name="status">The release status to filter on (if any).</param>
  /// <returns>The browse request, including the initial results.</returns>
  /// <exception cref="QueryException">When the web service reports an error.</exception>
  /// <exception cref="WebException">When something goes wrong with the web request.</exception>
  public Task<IBrowseResults<IRelease>> BrowseTrackReleasesAsync(Guid mbid, int? limit = null, int? offset = null,
                                                                 Include inc = Include.None, ReleaseType? type = null,
                                                                 ReleaseStatus? status = null)
    => new BrowseReleases(this, Query.BuildExtraText(inc, $"track={mbid:D}", type, status), limit, offset).NextAsync();

}
