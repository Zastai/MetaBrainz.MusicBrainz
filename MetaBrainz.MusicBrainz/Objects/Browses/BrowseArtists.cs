using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using MetaBrainz.MusicBrainz.Interfaces.Browses;
using MetaBrainz.MusicBrainz.Interfaces.Entities;
using MetaBrainz.MusicBrainz.Objects.Entities;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Objects.Browses {

  using Interface = IBrowseResults<IArtist>;
  #if NETFX_LT_4_5
  using Results   = IEnumerable<IArtist>;
  #else
  using Results   = IReadOnlyList<IArtist>;
  #endif

  internal sealed partial class BrowseArtists : BrowseResults<IArtist> {

    public BrowseArtists(Query query, string extra, int? limit = null, int? offset = null) : base(query, "artist", null, extra, limit, offset) { }

    public override Results Results => this._currentResult?.results;

    public override int TotalResults => this._currentResult?.count ?? 0;

    public override Interface Next() {
      var json = base.NextResponse(this._currentResult?.results.Length ?? 0);
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

    public override Interface Previous() {
      var json = base.PreviousResponse();
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

    #pragma warning disable 169
    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private sealed class JSON {
      [JsonProperty("artists",       Required = Required.Always)] public Artist[] results;
      [JsonProperty("artist-count",  Required = Required.Always)] public int      count;
      [JsonProperty("artist-offset", Required = Required.Always)] public int      offset;
    }

    #pragma warning restore 169
    #pragma warning restore 649

    private JSON _currentResult;

  }

}
