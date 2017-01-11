using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
#if NETFX_GE_4_5
using System.Threading.Tasks;
#endif

using MetaBrainz.MusicBrainz.Entities.Objects;

using Newtonsoft.Json;

namespace MetaBrainz.MusicBrainz.Entities.Browses {

  using Interface = IBrowseEntities<IWork>;
  #if NETFX_LT_4_5
  using Results = IEnumerable<IWork>;
  #else
  using Results = IReadOnlyList<IWork>;
  #endif

  internal abstract class BrowseWorksBase : BrowseEntities<IWork> {

    protected BrowseWorksBase(Query query, string endpoint, string value, string extra, int? limit = null, int? offset = null) : base(query, endpoint, value, extra, limit, offset) { }

    public sealed override Results Results => this._currentResult?.results;

    public sealed override int TotalResults => this._currentResult?.count ?? 0;

    public sealed override Interface Next() {
      var json = base.NextResponse();
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

    public sealed override Interface Previous() {
      var json = base.PreviousResponse();
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

#if NETFX_GE_4_5

    public sealed override async Task<Interface> NextAsync() {
      var json = await base.NextResponseAsync().ConfigureAwait(false);
      this._currentResult = JsonConvert.DeserializeObject<JSON>(json);
      return this;
    }

    public sealed override async Task<Interface> PreviousAsync() {
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
      [JsonProperty("works",       Required = Required.Always)] public Work[] results;
      [JsonProperty("work-count",  Required = Required.Always)] public int    count;
      [JsonProperty("work-offset", Required = Required.Always)] public int    offset;
    }

    #pragma warning restore 169
    #pragma warning restore 649

    private JSON _currentResult;

  }

}
