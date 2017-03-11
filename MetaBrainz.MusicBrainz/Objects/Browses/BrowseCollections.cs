using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

#if NETFX_GE_4_5
using System.Threading.Tasks;
#endif

using MetaBrainz.MusicBrainz.Entities.Objects;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Browses {

  using Interface = IBrowseResults<ICollection>;
  #if NETFX_LT_4_5
  using Results = IEnumerable<ICollection>;
  #else
  using Results = IReadOnlyList<ICollection>;
  #endif

  internal sealed class BrowseCollections : BrowseResults<ICollection> {

    public BrowseCollections(Query query, string extra, int? limit = null, int? offset = null) : base(query, "collection", null, extra, limit, offset) { }

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

#if NETFX_GE_4_5

    public override async Task<Interface> NextAsync() {
      var json = await base.NextResponseAsync(this._currentResult?.results.Length ?? 0).ConfigureAwait(false);
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

    public override async Task<Interface> PreviousAsync() {
      var json = await base.PreviousResponseAsync().ConfigureAwait(false);
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

#endif

    #pragma warning disable 169
    #pragma warning disable 649

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private sealed class JSON {
      [JsonProperty("collections",       Required = Required.Always)] public Collection[] results;
      [JsonProperty("collection-count",  Required = Required.Always)] public int          count;
      [JsonProperty("collection-offset", Required = Required.Always)] public int          offset;
    }

    #pragma warning restore 169
    #pragma warning restore 649

    private JSON _currentResult;

  }

}
