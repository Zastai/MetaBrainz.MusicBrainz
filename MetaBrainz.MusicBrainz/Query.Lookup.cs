using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using MetaBrainz.Common;
using MetaBrainz.MusicBrainz.Interfaces;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects;
using MetaBrainz.MusicBrainz.Objects.Browses;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz;

public sealed partial class Query {

  /// <summary>Looks up the specified area.</summary>
  /// <param name="mbid">The MBID for the area to look up.</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The requested area.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IArea> LookupAreaAsync(Guid mbid, Include inc = Include.None, CancellationToken cancellationToken = default)
    => this.PerformRequestAsync<IArea, Area>("area", mbid, Query.CreateOptions(inc), cancellationToken);

  /// <summary>Looks up the specified artist.</summary>
  /// <param name="mbid">The MBID for the artist to look up.</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">
  /// The release type to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.ReleaseGroups"/> and/or
  /// <see cref="Include.Releases"/>.
  /// </param>
  /// <param name="status">
  /// The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.
  /// </param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The requested artist.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IArtist> LookupArtistAsync(Guid mbid, Include inc = Include.None, ReleaseType? type = null,
                                         ReleaseStatus? status = null, CancellationToken cancellationToken = default)
    => this.PerformRequestAsync<IArtist, Artist>("artist", mbid, Query.CreateOptions(inc, status, type), cancellationToken);

  /// <summary>Looks up the specified collection.</summary>
  /// <param name="mbid">The MBID for the collection to look up.</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The requested collection.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<ICollection> LookupCollectionAsync(Guid mbid, Include inc = Include.None,
                                                 CancellationToken cancellationToken = default)
    => this.PerformRequestAsync<ICollection, Collection>("collection", mbid, Query.CreateOptions(inc), cancellationToken);

  /// <summary>Looks up the specified disc ID.</summary>
  /// <param name="discid">
  /// The disc ID to look up.
  /// When <paramref name="toc"/> is specified, this can be <c>"-"</c> to indicate that only a fuzzy TOC lookup should be done.
  /// </param>
  /// <param name="toc">
  /// The TOC (table of contents) to use for a fuzzy lookup if <paramref name="discid"/> has no exact matches.
  /// The array should contain the first track number, last track number and the address of the disc's lead-out (in sectors),
  /// followed by the start address of each track (in sectors).
  /// </param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="allMediaFormats">
  /// If <see langword="true"/>, all media formats are considered for a fuzzy lookup; otherwise, only CDs are considered.
  /// </param>
  /// <param name="noStubs">If <see langword="true"/>, CD stubs are not returned.</param>
  /// <returns>The result of the disc ID lookup. This can be a single disc or CD stub, or a list
  /// of matching releases.
  /// </returns>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IDiscIdLookupResult> LookupDiscIdAsync(string discid, int[]? toc = null, Include inc = Include.None,
                                                     bool allMediaFormats = false, bool noStubs = false,
                                                     CancellationToken cancellationToken = default) {
    var extra = Query.CreateOptions(toc, inc, allMediaFormats, noStubs);
    return this.PerformRequestAsync<IDiscIdLookupResult, DiscIdLookupResult>("discid", discid, extra, cancellationToken);
  }

  /// <summary>Looks up the specified event.</summary>
  /// <param name="mbid">The MBID for the event to look up.</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The requested event.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IEvent> LookupEventAsync(Guid mbid, Include inc = Include.None, CancellationToken cancellationToken = default)
    => this.PerformRequestAsync<IEvent, Event>("event", mbid, Query.CreateOptions(inc), cancellationToken);

  /// <summary>Looks up the specified genre.</summary>
  /// <param name="mbid">The MBID for the genre to look up.</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The requested genre.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IGenre> LookupGenreAsync(Guid mbid, Include inc = Include.None, CancellationToken cancellationToken = default)
    => this.PerformRequestAsync<IGenre, Genre>("genre", mbid, Query.CreateOptions(inc), cancellationToken);

  /// <summary>Looks up the specified instrument.</summary>
  /// <param name="mbid">The MBID for the instrument to look up.</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The requested instrument.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IInstrument> LookupInstrumentAsync(Guid mbid, Include inc = Include.None,
                                                 CancellationToken cancellationToken = default)
    => this.PerformRequestAsync<IInstrument, Instrument>("instrument", mbid, Query.CreateOptions(inc), cancellationToken);

  /// <summary>Looks up the recordings associated with the specified ISRC value.</summary>
  /// <param name="isrc">The ISRC to look up.</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The recordings associated with the requested ISRC.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IIsrc> LookupIsrcAsync(string isrc, Include inc = Include.None, CancellationToken cancellationToken = default)
    => this.PerformRequestAsync<IIsrc, Isrc>("isrc", isrc, Query.CreateOptions(inc), cancellationToken);

  /// <summary>Looks up the works associated with the specified ISWC.</summary>
  /// <param name="iswc">The ISWC to look up.</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The works associated with the requested ISWC.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public async Task<IReadOnlyList<IWork>> LookupIswcAsync(string iswc, Include inc = Include.None,
                                                          CancellationToken cancellationToken = default) {
    // This "lookup" behaves like a browse, except that it does not support offset/limit.
    var lookup = new IswcLookup(this, iswc, Query.CreateOptions(inc));
    var results = await lookup.NextAsync(cancellationToken).ConfigureAwait(false);
    return results.Results;
  }

  /// <summary>Looks up the specified label.</summary>
  /// <param name="mbid">The MBID for the label to look up.</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">
  /// The release type to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.
  /// </param>
  /// <param name="status">
  /// The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.
  /// </param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The requested label.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<ILabel> LookupLabelAsync(Guid mbid, Include inc = Include.None, ReleaseType? type = null,
                                       ReleaseStatus? status = null, CancellationToken cancellationToken = default)
    => this.PerformRequestAsync<ILabel, Label>("label", mbid, Query.CreateOptions(inc, status, type), cancellationToken);

  /// <summary>Looks up the specified place.</summary>
  /// <param name="mbid">The MBID for the place to look up.</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The requested place.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IPlace> LookupPlaceAsync(Guid mbid, Include inc = Include.None, CancellationToken cancellationToken = default)
    => this.PerformRequestAsync<IPlace, Place>("place", mbid, Query.CreateOptions(inc), cancellationToken);

  /// <summary>Looks up the specified recording.</summary>
  /// <param name="mbid">The MBID for the recording to look up.</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="type">
  /// The release type to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.
  /// </param>
  /// <param name="status">
  /// The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.
  /// </param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The requested recording.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IRecording> LookupRecordingAsync(Guid mbid, Include inc = Include.None, ReleaseType? type = null,
                                               ReleaseStatus? status = null, CancellationToken cancellationToken = default) {
    var options = Query.CreateOptions(inc, status, type);
    return this.PerformRequestAsync<IRecording, Recording>("recording", mbid, options, cancellationToken);
  }

  /// <summary>Looks up the specified release.</summary>
  /// <param name="mbid">The MBID for the release to look up.</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The requested release.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IRelease> LookupReleaseAsync(Guid mbid, Include inc = Include.None, CancellationToken cancellationToken = default)
    => this.PerformRequestAsync<IRelease, Release>("release", mbid, Query.CreateOptions(inc), cancellationToken);

  /// <summary>Looks up the specified release group.</summary>
  /// <param name="mbid">The MBID for the release group to look up.</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="status">
  /// The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.
  /// </param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The requested release group.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IReleaseGroup> LookupReleaseGroupAsync(Guid mbid, Include inc = Include.None, ReleaseStatus? status = null,
                                                     CancellationToken cancellationToken = default) {
    var options = Query.CreateOptions(inc, status);
    return this.PerformRequestAsync<IReleaseGroup, ReleaseGroup>("release-group", mbid, options, cancellationToken);
  }

  /// <summary>Looks up the specified series.</summary>
  /// <param name="mbid">The MBID for the series to look up.</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The requested series.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<ISeries> LookupSeriesAsync(Guid mbid, Include inc = Include.None, CancellationToken cancellationToken = default)
    => this.PerformRequestAsync<ISeries, Series>("series", mbid, Query.CreateOptions(inc), cancellationToken);

  /// <summary>Looks up the specified URL.</summary>
  /// <param name="mbid">The MBID for the URL to look up.</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The requested URL.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IUrl> LookupUrlAsync(Guid mbid, Include inc = Include.None, CancellationToken cancellationToken = default)
    => this.PerformRequestAsync<IUrl, Url>("url", mbid, Query.CreateOptions(inc), cancellationToken);

  /// <summary>Looks up the specified URL.</summary>
  /// <param name="resource">The resource to look up.</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The requested URL.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IUrl> LookupUrlAsync(Uri resource, Include inc = Include.None, CancellationToken cancellationToken = default)
    => this.PerformRequestAsync<IUrl, Url>("url", null, Query.CreateOptions(inc, resource), cancellationToken);

  /// <summary>Looks up the specified work.</summary>
  /// <param name="mbid">The MBID for the work to look up.</param>
  /// <param name="inc">Additional information to include in the result.</param>
  /// <param name="cancellationToken">The cancellation token to cancel the operation.</param>
  /// <returns>The requested work.</returns>
  /// <exception cref="HttpError">When the web service reports an error.</exception>
  /// <exception cref="HttpRequestException">When something goes wrong with the request.</exception>
  public Task<IWork> LookupWorkAsync(Guid mbid, Include inc = Include.None, CancellationToken cancellationToken = default)
    => this.PerformRequestAsync<IWork, Work>("work", mbid, Query.CreateOptions(inc), cancellationToken);

}
