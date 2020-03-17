using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using MetaBrainz.MusicBrainz.Interfaces;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects;
using MetaBrainz.MusicBrainz.Objects.Browses;
using MetaBrainz.MusicBrainz.Objects.Entities;

namespace MetaBrainz.MusicBrainz {

  public sealed partial class Query {

    /// <summary>Looks up the specified area.</summary>
    /// <param name="mbid">The MBID for the area to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested area.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IArea LookupArea(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("area", mbid.ToString("D"), BuildExtraText(inc));
      return Query.Deserialize<Area>(json);
    }

    /// <summary>Looks up the specified area.</summary>
    /// <param name="mbid">The MBID for the area to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>An asynchronous operation returning the requested area.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public async Task<IArea> LookupAreaAsync(Guid mbid, Include inc = Include.None) {
      var json = await this.PerformRequestAsync("area", mbid.ToString("D"), BuildExtraText(inc)).ConfigureAwait(false);
      return Query.Deserialize<Area>(json);
    }

    /// <summary>Looks up the specified artist.</summary>
    /// <param name="mbid">The MBID for the artist to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="type">
    /// The release type to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.ReleaseGroups"/> and/or <see cref="Include.Releases"/>.
    /// </param>
    /// <param name="status">The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <returns>The requested artist.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IArtist LookupArtist(Guid mbid, Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null) {
      var json = this.PerformRequest("artist", mbid.ToString("D"), BuildExtraText(inc, status, type));
      return Query.Deserialize<Artist>(json);
    }

    /// <summary>Looks up the specified artist.</summary>
    /// <param name="mbid">The MBID for the artist to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="type">
    /// The release type to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.ReleaseGroups"/> and/or <see cref="Include.Releases"/>.
    /// </param>
    /// <param name="status">The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <returns>An asynchronous operation returning the requested artist.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public async Task<IArtist> LookupArtistAsync(Guid mbid, Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null) {
      var json = await this.PerformRequestAsync("artist", mbid.ToString("D"), BuildExtraText(inc, status, type)).ConfigureAwait(false);
      return Query.Deserialize<Artist>(json);
    }

    /// <summary>Looks up the specified collection.</summary>
    /// <param name="mbid">The MBID for the collection to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested collection.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public ICollection LookupCollection(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("collection", mbid.ToString("D"), BuildExtraText(inc));
      return Query.Deserialize<Collection>(json);
    }

    /// <summary>Looks up the specified collection.</summary>
    /// <param name="mbid">The MBID for the collection to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>An asynchronous operation returning the requested collection.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public async Task<ICollection> LookupCollectionAsync(Guid mbid, Include inc = Include.None) {
      var json = await this.PerformRequestAsync("collection", mbid.ToString("D"), BuildExtraText(inc)).ConfigureAwait(false);
      return Query.Deserialize<Collection>(json);
    }

    /// <summary>Looks up the specified disc ID.</summary>
    /// <param name="discid">The disc ID to look up.</param>
    /// <param name="toc">
    ///   The TOC (table of contents) to use for a fuzzy lookup if <paramref name="discid"/> has no exact matches.
    ///   The array should contain the first track number, last track number and the address of the disc's lead-out (in sectors),
    ///   followed by the start address of each track (in sectors).
    /// </param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="allMedia">If true, all media types are considered for a fuzzy lookup; otherwise, only CDs are considered.</param>
    /// <param name="noStubs">If true, CD stubs are not returned.</param>
    /// <returns>The result of the disc ID lookup. This can be a single disc or CD stub, or a list of matching releases.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IDiscIdLookupResult LookupDiscId(string discid, int[]? toc = null, Include inc = Include.None, bool allMedia = false, bool noStubs = false) {
      var json = this.PerformRequest("discid", discid, BuildExtraText(inc, toc, allMedia, noStubs));
      return new DiscIdLookupResult(discid, json);
    }

    /// <summary>Looks up the specified disc ID.</summary>
    /// <param name="discid">The disc ID to look up.</param>
    /// <param name="toc">
    ///   The TOC (table of contents) to use for a fuzzy lookup if <paramref name="discid"/> has no exact matches.
    ///   The array should contain the first track number, last track number and the address of the disc's lead-out (in sectors),
    ///   followed by the start address of each track (in sectors).
    /// </param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="allMediaFormats">If true, all media formats are considered for a fuzzy lookup; otherwise, only CDs are considered.</param>
    /// <param name="noStubs">If true, CD stubs are not returned.</param>
    /// <returns>An asynchronous operation returning the result of the disc ID lookup. This can be a single disc or CD stub, or a list of matching releases.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public async Task<IDiscIdLookupResult> LookupDiscIdAsync(string discid, int[]? toc = null, Include inc = Include.None, bool allMediaFormats = false, bool noStubs = false) {
      var json = await this.PerformRequestAsync("discid", discid, BuildExtraText(inc, toc, allMediaFormats, noStubs)).ConfigureAwait(false);
      return new DiscIdLookupResult(discid, json);
    }

    /// <summary>Looks up the specified event.</summary>
    /// <param name="mbid">The MBID for the event to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested event.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IEvent LookupEvent(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("event", mbid.ToString("D"), BuildExtraText(inc));
      return Query.Deserialize<Event>(json);
    }

    /// <summary>Looks up the specified event.</summary>
    /// <param name="mbid">The MBID for the event to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>An asynchronous operation returning the requested event.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public async Task<IEvent> LookupEventAsync(Guid mbid, Include inc = Include.None) {
      var json = await this.PerformRequestAsync("event", mbid.ToString("D"), BuildExtraText(inc)).ConfigureAwait(false);
      return Query.Deserialize<Event>(json);
    }

    /// <summary>Looks up the specified instrument.</summary>
    /// <param name="mbid">The MBID for the instrument to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested instrument.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IInstrument LookupInstrument(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("instrument", mbid.ToString("D"), BuildExtraText(inc));
      return Query.Deserialize<Instrument>(json);
    }

    /// <summary>Looks up the specified instrument.</summary>
    /// <param name="mbid">The MBID for the instrument to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>An asynchronous operation returning the requested instrument.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public async Task<IInstrument> LookupInstrumentAsync(Guid mbid, Include inc = Include.None) {
      var json = await this.PerformRequestAsync("instrument", mbid.ToString("D"), BuildExtraText(inc)).ConfigureAwait(false);
      return Query.Deserialize<Instrument>(json);
    }

    /// <summary>Looks up the recordings associated with the specified ISRC value.</summary>
    /// <param name="isrc">The ISRC to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The recordings associated with the requested ISRC.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IIsrc LookupIsrc(string isrc, Include inc = Include.None) {
      var json = this.PerformRequest("isrc", isrc, BuildExtraText(inc));
      return Query.Deserialize<Isrc>(json);
    }

    /// <summary>Looks up the recordings associated with the specified ISRC value.</summary>
    /// <param name="isrc">The ISRC to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>An asynchronous operation returning the recordings associated with the requested ISRC.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public async Task<IIsrc> LookupIsrcAsync(string isrc, Include inc = Include.None) {
      var json = await this.PerformRequestAsync("isrc", isrc, BuildExtraText(inc)).ConfigureAwait(false);
      return Query.Deserialize<Isrc>(json);
    }

    /// <summary>Looks up the works associated with the specified ISWC.</summary>
    /// <param name="iswc">The ISWC to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The works associated with the requested ISWC.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IReadOnlyList<IWork> LookupIswc(string iswc, Include inc = Include.None) {
      // This "lookup" behaves like a browse, except that it does not support offset/limit.
      return new IswcLookup(this, iswc, BuildExtraText(inc)).Next().Results;
    }

    /// <summary>Looks up the works associated with the specified ISWC.</summary>
    /// <param name="iswc">The ISWC to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>An asynchronous operation returning the works associated with the requested ISWC.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public async Task<IReadOnlyList<IWork>> LookupIswcAsync(string iswc, Include inc = Include.None) {
      var il = await new IswcLookup(this, iswc, BuildExtraText(inc)).NextAsync().ConfigureAwait(false);
      return il.Results;
    }

    /// <summary>Looks up the specified label.</summary>
    /// <param name="mbid">The MBID for the label to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="type">The release type to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <param name="status">The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <returns>The requested label.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public ILabel LookupLabel(Guid mbid, Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null) {
      var json = this.PerformRequest("label", mbid.ToString("D"), BuildExtraText(inc, status, type));
      return Query.Deserialize<Label>(json);
    }

    /// <summary>Looks up the specified label.</summary>
    /// <param name="mbid">The MBID for the label to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="type">The release type to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <param name="status">The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <returns>An asynchronous operation returning the requested label.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public async Task<ILabel> LookupLabelAsync(Guid mbid, Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null) {
      var json = await this.PerformRequestAsync("label", mbid.ToString("D"), BuildExtraText(inc, status, type)).ConfigureAwait(false);
      return Query.Deserialize<Label>(json);
    }

    /// <summary>Looks up the specified place.</summary>
    /// <param name="mbid">The MBID for the place to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested place.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IPlace LookupPlace(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("place", mbid.ToString("D"), BuildExtraText(inc));
      return Query.Deserialize<Place>(json);
    }

    /// <summary>Looks up the specified place.</summary>
    /// <param name="mbid">The MBID for the place to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>An asynchronous operation returning the requested place.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public async Task<IPlace> LookupPlaceAsync(Guid mbid, Include inc = Include.None) {
      var json = await this.PerformRequestAsync("place", mbid.ToString("D"), BuildExtraText(inc)).ConfigureAwait(false);
      return Query.Deserialize<Place>(json);
    }

    /// <summary>Looks up the specified recording.</summary>
    /// <param name="mbid">The MBID for the recording to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="type">The release type to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <param name="status">The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <returns>The requested recording.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IRecording LookupRecording(Guid mbid, Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null) {
      var json = this.PerformRequest("recording", mbid.ToString("D"), BuildExtraText(inc, status, type));
      return Query.Deserialize<Recording>(json);
    }

    /// <summary>Looks up the specified recording.</summary>
    /// <param name="mbid">The MBID for the recording to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="type">The release type to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <param name="status">The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <returns>An asynchronous operation returning the requested recording.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public async Task<IRecording> LookupRecordingAsync(Guid mbid, Include inc = Include.None, ReleaseType? type = null, ReleaseStatus? status = null) {
      var json = await this.PerformRequestAsync("recording", mbid.ToString("D"), BuildExtraText(inc, status, type)).ConfigureAwait(false);
      return Query.Deserialize<Recording>(json);
    }

    /// <summary>Looks up the specified release.</summary>
    /// <param name="mbid">The MBID for the release to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested release.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IRelease LookupRelease(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("release", mbid.ToString("D"), BuildExtraText(inc));
      return Query.Deserialize<Release>(json);
    }

    /// <summary>Looks up the specified release.</summary>
    /// <param name="mbid">The MBID for the release to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>An asynchronous operation returning the requested release.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public async Task<IRelease> LookupReleaseAsync(Guid mbid, Include inc = Include.None) {
      var json = await this.PerformRequestAsync("release", mbid.ToString("D"), BuildExtraText(inc)).ConfigureAwait(false);
      return Query.Deserialize<Release>(json);
    }

    /// <summary>Looks up the specified release group.</summary>
    /// <param name="mbid">The MBID for the release group to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="status">The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <returns>The requested release group.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IReleaseGroup LookupReleaseGroup(Guid mbid, Include inc = Include.None, ReleaseStatus? status = null) {
      var json = this.PerformRequest("release-group", mbid.ToString("D"), BuildExtraText(inc, status));
      return Query.Deserialize<ReleaseGroup>(json);
    }

    /// <summary>Looks up the specified release group.</summary>
    /// <param name="mbid">The MBID for the release group to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="status">The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <returns>An asynchronous operation returning the requested release group.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public async Task<IReleaseGroup> LookupReleaseGroupAsync(Guid mbid, Include inc = Include.None, ReleaseStatus? status = null) {
      var json = await this.PerformRequestAsync("release-group", mbid.ToString("D"), BuildExtraText(inc, status)).ConfigureAwait(false);
      return Query.Deserialize<ReleaseGroup>(json);
    }

    /// <summary>Looks up the specified series.</summary>
    /// <param name="mbid">The MBID for the series to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested series.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public ISeries LookupSeries(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("series", mbid.ToString("D"), BuildExtraText(inc));
      return Query.Deserialize<Series>(json);
    }

    /// <summary>Looks up the specified series.</summary>
    /// <param name="mbid">The MBID for the series to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>An asynchronous operation returning the requested series.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public async Task<ISeries> LookupSeriesAsync(Guid mbid, Include inc = Include.None) {
      var json = await this.PerformRequestAsync("series", mbid.ToString("D"), BuildExtraText(inc)).ConfigureAwait(false);
      return Query.Deserialize<Series>(json);
    }

    /// <summary>Looks up the specified URL.</summary>
    /// <param name="mbid">The MBID for the URL to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested URL.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IUrl LookupUrl(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("url", mbid.ToString("D"), BuildExtraText(inc));
      return Query.Deserialize<Url>(json);
    }

    /// <summary>Looks up the specified URL.</summary>
    /// <param name="resource">The resource to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested URL.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IUrl LookupUrl(Uri resource, Include inc = Include.None) {
      var json = this.PerformRequest("url", null, BuildExtraText(inc, resource));
      return Query.Deserialize<Url>(json);
    }

    /// <summary>Looks up the specified URL.</summary>
    /// <param name="mbid">The MBID for the URL to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>An asynchronous operation returning the requested URL.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public async Task<IUrl> LookupUrlAsync(Guid mbid, Include inc = Include.None) {
      var json = await this.PerformRequestAsync("url", mbid.ToString("D"), BuildExtraText(inc)).ConfigureAwait(false);
      return Query.Deserialize<Url>(json);
    }

    /// <summary>Looks up the specified URL.</summary>
    /// <param name="resource">The resource to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>An asynchronous operation returning the requested URL.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public async Task<IUrl> LookupUrlAsync(Uri resource, Include inc = Include.None) {
      var json = await this.PerformRequestAsync("url", null, BuildExtraText(inc, resource)).ConfigureAwait(false);
      return Query.Deserialize<Url>(json);
    }

    /// <summary>Looks up the specified work.</summary>
    /// <param name="mbid">The MBID for the work to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested work.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IWork LookupWork(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("work", mbid.ToString("D"), BuildExtraText(inc));
      return Query.Deserialize<Work>(json);
    }

    /// <summary>Looks up the specified work.</summary>
    /// <param name="mbid">The MBID for the work to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>An asynchronous operation returning the requested work.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public async Task<IWork> LookupWorkAsync(Guid mbid, Include inc = Include.None) {
      var json = await this.PerformRequestAsync("work", mbid.ToString("D"), BuildExtraText(inc)).ConfigureAwait(false);
      return Query.Deserialize<Work>(json);
    }

  }

}
