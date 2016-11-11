using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;

using MetaBrainz.MusicBrainz.Entities;
using MetaBrainz.MusicBrainz.Entities.Objects;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz {

  #if NETFX_LT_4_5
  using WorkList = IEnumerable<IWork>;
  #else
  using WorkList = IReadOnlyList<IWork>;
  #endif

  [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  public sealed partial class Query {

    /// <summary>Looks up the specified area.</summary>
    /// <param name="mbid">The MBID for the area to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested area.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IArea LookupArea(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("area", mbid.ToString("D"), Query.BuildExtraText(inc));
      return JsonConvert.DeserializeObject<Area>(json, Query.SerializerSettings);
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
      var json = this.PerformRequest("artist", mbid.ToString("D"), Query.BuildExtraText(inc, type: type, status: status));
      return JsonConvert.DeserializeObject<Artist>(json, Query.SerializerSettings);
    }

    /// <summary>Looks up the specified collection.</summary>
    /// <param name="mbid">The MBID for the collection to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested collection.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public ICollection LookupCollection(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("collection", mbid.ToString("D"), Query.BuildExtraText(inc));
      return JsonConvert.DeserializeObject<Collection>(json, Query.SerializerSettings);
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
    public DiscIdLookupResult LookupDiscId(string discid, int[] toc = null, Include inc = Include.None, bool allMedia = false, bool noStubs = false) {
      return new DiscIdLookupResult(discid, this.PerformRequest("discid", discid, Query.BuildExtraText(inc, allMedia: allMedia, noStubs: noStubs, toc: toc)), Query.SerializerSettings);
    }

    /// <summary>Looks up the specified event.</summary>
    /// <param name="mbid">The MBID for the event to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested event.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IEvent LookupEvent(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("event", mbid.ToString("D"), Query.BuildExtraText(inc));
      return JsonConvert.DeserializeObject<Event>(json, Query.SerializerSettings);
    }

    /// <summary>Looks up the specified instrument.</summary>
    /// <param name="mbid">The MBID for the instrument to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested instrument.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IInstrument LookupInstrument(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("instrument", mbid.ToString("D"), Query.BuildExtraText(inc));
      return JsonConvert.DeserializeObject<Instrument>(json, Query.SerializerSettings);
    }

    /// <summary>Looks up the recordings associated with the specified ISRC value.</summary>
    /// <param name="isrc">The ISRC to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The recordings associated with the requested ISRC.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IIsrc LookupIsrc(string isrc, Include inc = Include.None) {
      var json = this.PerformRequest("isrc", isrc, Query.BuildExtraText(inc));
      return JsonConvert.DeserializeObject<Isrc>(json, Query.SerializerSettings);
    }

    /// <summary>Looks up the works associated with the specified ISWC.</summary>
    /// <param name="iswc">The ISWC to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The works associated with the requested ISWC.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public WorkList LookupIswc(string iswc, Include inc = Include.None) {
      var json = this.PerformRequest("iswc", iswc, Query.BuildExtraText(inc));
      // While this lookup is returned as if it was a browse request for works, the offset/limit options don't work, so just return the results directly.
      return JsonConvert.DeserializeObject<BrowseWorksResult>(json)?.works;
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
      var json = this.PerformRequest("label", mbid.ToString("D"), Query.BuildExtraText(inc, type: type, status: status));
      return JsonConvert.DeserializeObject<Label>(json, Query.SerializerSettings);
    }

    /// <summary>Looks up the specified place.</summary>
    /// <param name="mbid">The MBID for the place to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested place.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IPlace LookupPlace(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("place", mbid.ToString("D"), Query.BuildExtraText(inc));
      return JsonConvert.DeserializeObject<Place>(json, Query.SerializerSettings);
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
      var json = this.PerformRequest("recording", mbid.ToString("D"), Query.BuildExtraText(inc, type: type, status: status));
      return JsonConvert.DeserializeObject<Recording>(json, Query.SerializerSettings);
    }

    /// <summary>Looks up the specified release.</summary>
    /// <param name="mbid">The MBID for the release to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested release.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IRelease LookupRelease(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("release", mbid.ToString("D"), Query.BuildExtraText(inc));
      return JsonConvert.DeserializeObject<Release>(json, Query.SerializerSettings);
    }

    /// <summary>Looks up the specified release group.</summary>
    /// <param name="mbid">The MBID for the release group to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <param name="status">The release status to filter on; applies only when <paramref name="inc"/> includes <see cref="Include.Releases"/>.</param>
    /// <returns>The requested release group.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IReleaseGroup LookupReleaseGroup(Guid mbid, Include inc = Include.None, ReleaseStatus? status = null) {
      var json = this.PerformRequest("release-group", mbid.ToString("D"), Query.BuildExtraText(inc, status: status));
      return JsonConvert.DeserializeObject<ReleaseGroup>(json, Query.SerializerSettings);
    }

    /// <summary>Looks up the specified series.</summary>
    /// <param name="mbid">The MBID for the series to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested series.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public ISeries LookupSeries(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("series", mbid.ToString("D"), Query.BuildExtraText(inc));
      return JsonConvert.DeserializeObject<Series>(json, Query.SerializerSettings);
    }

    /// <summary>Looks up the specified URL.</summary>
    /// <param name="mbid">The MBID for the URL to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested URL.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IUrl LookupUrl(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("url", mbid.ToString("D"), Query.BuildExtraText(inc));
      return JsonConvert.DeserializeObject<Url>(json, Query.SerializerSettings);
    }

    /// <summary>Looks up the specified URL.</summary>
    /// <param name="resource">The resource to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested URL.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IUrl LookupUrl(Uri resource, Include inc = Include.None) {
      if (resource == null) throw new ArgumentNullException(nameof(resource));
      var json = this.PerformRequest("url", null, Query.BuildExtraText(inc, resource: resource));
      return JsonConvert.DeserializeObject<Url>(json, Query.SerializerSettings);
    }

    /// <summary>Looks up the specified work.</summary>
    /// <param name="mbid">The MBID for the work to look up.</param>
    /// <param name="inc">Additional information to include in the result.</param>
    /// <returns>The requested work.</returns>
    /// <exception cref="QueryException">When the web service reports an error.</exception>
    /// <exception cref="WebException">When something goes wrong with the web request.</exception>
    public IWork LookupWork(Guid mbid, Include inc = Include.None) {
      var json = this.PerformRequest("work", mbid.ToString("D"), Query.BuildExtraText(inc));
      return JsonConvert.DeserializeObject<Work>(json, Query.SerializerSettings);
    }

  }

}
